using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

namespace Warlock_The_Soulbinder
{
    class GeneralMenu : Menu
    {
        static GeneralMenu instance;
        private Texture2D book;
        private Texture2D weaponRing;
        private Texture2D skillRing;
        private Texture2D armorRing;
        private Texture2D emptyRing;
        private Texture2D skillPlank;
        private Texture2D levelCircle;
        private Texture2D expFull;
        private GameTime tempTime;
        private int logPage = 0;
        private List<String> menuList = new List<string>();
        private Texture2D arrow;
        private string inventoryState = "GeneralMenu";
        private bool equipping = false;
        private int equippingTo;
        private float delay = 0;
        private int currentPage = 0;
        private bool changingKey = false;

        public static GeneralMenu Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GeneralMenu();
                }
                return instance;
            }
        }

        public int CurrentPage { get => currentPage; set => currentPage = value; }
        public int EquippingTo { get => equippingTo; set => equippingTo = value; }
        public bool Equipping { get => equipping; set => equipping = value; }

        private GeneralMenu()
        {

        }
        
        public void LoadContent(ContentManager content)
        {
            book = content.Load<Texture2D>("Book");
            arrow = content.Load<Texture2D>("Arrow");
            weaponRing = content.Load<Texture2D>("buttons/weaponRing");
            armorRing = content.Load<Texture2D>("buttons/armorRing");
            skillRing = content.Load<Texture2D>("buttons/skillRing");
            emptyRing = content.Load<Texture2D>("buttons/emptyRing");
            skillPlank = content.Load<Texture2D>("buttons/skillPlank");
            levelCircle = content.Load<Texture2D>("buttons/levelCircle");
            expFull = content.Load<Texture2D>("buttons/expFull");
        }

        public override void Update(GameTime gameTime)
        {
            delay += gameTime.ElapsedGameTime.Milliseconds;

            switch (inventoryState)
            {
                case "GeneralMenu":
                    ChangeSelected(7);
                    break;
                case "Inventory":
                    ChangeSelected(1);
                    break;
                case "Equipment":
                    ChangeSelected(4);

                    //Code for unequipping stones
                    if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeyCancel) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonCancel)) && delay > 200)
                    {
                        switch (selectedInt)
                        {
                            case 0:
                                if (Equipment.Instance.Weapon != null)
                                {
                                    Equipment.Instance.Weapon.Equipped = false;
                                    Equipment.Instance.Weapon = null;
                                }
                                break;

                            case 1:
                                if (Equipment.Instance.Armor != null)
                                {
                                    Equipment.Instance.Armor.Equipped = false;
                                    Equipment.Instance.Armor = null;
                                }
                                break;

                            case 2:
                                if (Equipment.Instance.Skill1 != null)
                                {
                                    Equipment.Instance.Skill1.Equipped = false;
                                    Equipment.Instance.Skill1 = null;
                                }
                                break;

                            case 3:
                                if (Equipment.Instance.Skill2 != null)
                                {
                                    Equipment.Instance.Skill2.Equipped = false;
                                    Equipment.Instance.Skill2 = null;
                                }
                                break;

                            case 4:
                                if (Equipment.Instance.Skill3 != null)
                                {
                                    Equipment.Instance.Skill3.Equipped = false;
                                    Equipment.Instance.Skill3 = null;
                                }
                                break;
                        }
                    }
                    break;
                case "FilledStones":
                    if (CurrentPage < FilledStone.StoneListPages)
                    {
                        ChangeSelected(8);
                    }
                    else
                    {
                        ChangeSelected(FilledStone.StoneList.Count - (FilledStone.StoneListPages * 9) - 1);
                    }

                    if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeyRight) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonRight)) && delay > 200 && currentPage < FilledStone.StoneListPages)
                    {
                        currentPage++;
                        delay = 0;
                        selectedInt = 0;
                    }

                    if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeyLeft) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonLeft)) && delay > 200 && currentPage > 0)
                    {
                        currentPage--;
                        delay = 0;
                        selectedInt = 0;
                    }
                    break;
                case "Options":
                    ChangeSelected(2);

                    if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeyRight) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonRight)) && delay > 30)
                    {
                        if (GameWorld.Instance.SoundEffectVolume < 1 && selectedInt == 0)
                        {
                            GameWorld.Instance.SoundEffectVolume += 0.01f;

                        }

                        if (GameWorld.Instance.MusicVolume < 1 && selectedInt == 1)
                        {
                            GameWorld.Instance.MusicVolume += 0.01f;

                        }
                    }

                    if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeyLeft) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonLeft)) && delay > 30)
                    {
                        if (GameWorld.Instance.SoundEffectVolume > 0 && selectedInt == 0)
                        {
                            GameWorld.Instance.SoundEffectVolume -= 0.01f;

                            if (GameWorld.Instance.SoundEffectVolume < 0)
                            {
                                GameWorld.Instance.SoundEffectVolume = 0;
                            }
                        }

                        if (GameWorld.Instance.MusicVolume > 0 && selectedInt == 1)
                        {
                            GameWorld.Instance.MusicVolume -= 0.01f;

                            if (GameWorld.Instance.MusicVolume < 0)
                            {
                                GameWorld.Instance.MusicVolume = 0;
                            }
                        }
                    }

                    break;
                case "Keybinds":
                    ChangeSelected(7);

                    if (InputHandler.Instance.KeyPressed(Keys.R))
                    {
                        InputHandler.Instance.ResetKeybinds();
                    }
                    break;
                case "Log":
                    if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeyRight) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonRight)) && delay > 200 && logPage < 20)
                    {
                        logPage++;
                        delay = 0;
                    }

                    if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeyLeft) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonLeft)) && delay > 200 && logPage > 0)
                    {
                        logPage--;
                        delay = 0;
                    }
                    break;
            }   
            
            //Key to execute code dependent on the inventory state
            if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeySelect) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonSelect)) && delay > 200)
            {
                ChangeState();
                delay = 0;
                selectedInt = 0;
            }

            //Key for going back in menus
            if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeyReturn) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonReturn)) && delay > 150)
            {
                switch (inventoryState)
                {
                    case "Inventory":
                        inventoryState = "GeneralMenu";
                        selectedInt = 0;
                        break;
                    case "Equipment":
                        inventoryState = "GeneralMenu";
                        selectedInt = 0;
                        break;
                    case "Save":
                        inventoryState = "GeneralMenu";
                        selectedInt = 0;
                        break;
                    case "FilledStones":
                        if (equipping == false)
                        {
                            inventoryState = "Inventory";
                            selectedInt = 0;
                            break;
                        }

                        if (equipping == true)
                        {
                            inventoryState = "Equipment";
                            equipping = false;
                            selectedInt = 0;
                            break;
                        }
                        break;
                    case "Consumables":
                        inventoryState = "Inventory";
                        selectedInt = 0;
                        break;
                    case "Options":
                        inventoryState = "GeneralMenu";
                        selectedInt = 0;
                        break;
                    case "Keybinds":
                        inventoryState = "Options";
                        selectedInt = 0;
                        break;
                    case "Character":
                        inventoryState = "GeneralMenu";
                        selectedInt = 0;
                        break;
                    case "Log":
                        inventoryState = "GeneralMenu";
                        selectedInt = 0;
                        break;
                }

                delay = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(book, new Vector2(50, 20), Color.White);

            if (inventoryState == "GeneralMenu")
            { 
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Character", new Vector2(200, 120), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Equipment", new Vector2(200, 200), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Inventory", new Vector2(200, 280), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Quests", new Vector2(200, 360), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Log", new Vector2(200, 440), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Save", new Vector2(200, 520), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Options", new Vector2(200, 600), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Quit", new Vector2(200, 680), Color.White);
                
                switch (selectedInt)
                {

                    case 1:
                        if (Equipment.Instance.Weapon != null)
                        {
                            spriteBatch.Draw(Equipment.Instance.Weapon.Sprite, new Vector2(985, 120), null, Color.White, 0f, Vector2.Zero, 0.70f, new SpriteEffects(), 1);
                        }

                        if (Equipment.Instance.Armor != null)
                        {
                            spriteBatch.Draw(Equipment.Instance.Armor.Sprite, new Vector2(985, 280), null, Color.White, 0f, Vector2.Zero, 0.70f, new SpriteEffects(), 1);
                        }

                        if (Equipment.Instance.Skill1 != null)
                        {
                            spriteBatch.Draw(Equipment.Instance.Skill1.Sprite, new Vector2(985, 440), null, Color.White, 0f, Vector2.Zero, 0.70f, new SpriteEffects(), 1);
                        }

                        if (Equipment.Instance.Skill2 != null)
                        {
                            spriteBatch.Draw(Equipment.Instance.Skill2.Sprite, new Vector2(985, 600), null, Color.White, 0f, Vector2.Zero, 0.70f, new SpriteEffects(), 1);
                        }

                        if (Equipment.Instance.Skill3 != null)
                        {
                            spriteBatch.Draw(Equipment.Instance.Skill3.Sprite, new Vector2(985, 760), null, Color.White, 0f, Vector2.Zero, 0.70f, new SpriteEffects(), 1);
                        }

                        if (Equipment.Instance.Weapon != null)
                        {
                            spriteBatch.Draw(emptyRing, new Vector2(1000, 120), null, Color.White, 0f, Vector2.Zero, 0.6f, new SpriteEffects(), 1);
                        }
                        else
                        {
                            spriteBatch.Draw(weaponRing, new Vector2(1000, 120), null, Color.White, 0f, Vector2.Zero, 0.6f, new SpriteEffects(), 1);
                        }

                        if (Equipment.Instance.Armor != null)
                        {
                            spriteBatch.Draw(emptyRing, new Vector2(1000, 280), null, Color.White, 0f, Vector2.Zero, 0.6f, new SpriteEffects(), 1);
                        }
                        else
                        {
                            spriteBatch.Draw(armorRing, new Vector2(1000, 280), null, Color.White, 0f, Vector2.Zero, 0.6f, new SpriteEffects(), 1);
                        }

                        if (Equipment.Instance.Skill1 != null)
                        {
                            spriteBatch.Draw(emptyRing, new Vector2(1000, 440), null, Color.White, 0f, Vector2.Zero, 0.6f, new SpriteEffects(), 1);
                        }
                        else
                        {
                            spriteBatch.Draw(skillRing, new Vector2(1000, 440), null, Color.White, 0f, Vector2.Zero, 0.6f, new SpriteEffects(), 1);
                        }

                        if (Equipment.Instance.Skill2 != null)
                        {
                            spriteBatch.Draw(emptyRing, new Vector2(1000, 600), null, Color.White, 0f, Vector2.Zero, 0.6f, new SpriteEffects(), 1);
                        }
                        else
                        {
                            spriteBatch.Draw(skillRing, new Vector2(1000, 600), null, Color.White, 0f, Vector2.Zero, 0.6f, new SpriteEffects(), 1);
                        }

                        if (Equipment.Instance.Skill3 != null)
                        {
                            spriteBatch.Draw(emptyRing, new Vector2(1000, 760), null, Color.White, 0f, Vector2.Zero, 0.6f, new SpriteEffects(), 1);
                        }
                        else
                        {
                            spriteBatch.Draw(skillRing, new Vector2(1000, 760), null, Color.White, 0f, Vector2.Zero, 0.6f, new SpriteEffects(), 1);
                        }
                    
                        //Skillplanks
                        for (int i = 0; i < 5; i++)
                        {
                            spriteBatch.Draw(skillPlank, new Vector2(1170, 120 + 160 * i), Color.White);
                        }

                        if (Equipment.Instance.Weapon != null)
                        {
                            spriteBatch.DrawString(Combat.Instance.CombatFont, Equipment.Instance.Weapon.WeaponName, new Vector2(1200, 160), Color.White);
                        }

                        if (Equipment.Instance.Armor != null)
                        {
                            spriteBatch.DrawString(Combat.Instance.CombatFont, Equipment.Instance.Armor.ArmorName, new Vector2(1200, 320), Color.White);
                        }

                        if (Equipment.Instance.Skill1 != null)
                        {
                            spriteBatch.DrawString(Combat.Instance.CombatFont, Equipment.Instance.Skill1.SkillName, new Vector2(1200, 480), Color.White);
                        }

                        if (Equipment.Instance.Skill2 != null)
                        {
                            spriteBatch.DrawString(Combat.Instance.CombatFont, Equipment.Instance.Skill2.SkillName, new Vector2(1200, 640), Color.White);
                        }

                        if (Equipment.Instance.Skill3 != null)
                        {
                            spriteBatch.DrawString(Combat.Instance.CombatFont, Equipment.Instance.Skill3.SkillName, new Vector2(1200, 800), Color.White);
                        }
                        break;
                    case 2:
                    spriteBatch.DrawString(Combat.Instance.CombatFont, "Consumables", new Vector2(1100, 120), Color.White);
                    spriteBatch.DrawString(Combat.Instance.CombatFont, "Monster Stones", new Vector2(1100, 200), Color.White);
                        break;

                    case 4:
                        spriteBatch.DrawString(Combat.Instance.CombatFont, $"Complete Scans: {Log.Instance.FullScans()} / 21", new Vector2(1050, 120), Color.White);
                        spriteBatch.DrawString(Combat.Instance.CombatFont, $"Bonus VS Neutral: {Log.Instance.NeutralBonus * 100}%", new Vector2(1050, 200), Color.White);
                        spriteBatch.DrawString(Combat.Instance.CombatFont, $"Bonus VS Earth: {Log.Instance.EarthBonus * 100}%", new Vector2(1050, 280), Color.White);
                        spriteBatch.DrawString(Combat.Instance.CombatFont, $"Bonus VS Water: {Log.Instance.WaterBonus * 100}%", new Vector2(1050, 360), Color.White);
                        spriteBatch.DrawString(Combat.Instance.CombatFont, $"Bonus VS Dark: {Log.Instance.DarkBonus * 100}%", new Vector2(1050, 440), Color.White);
                        spriteBatch.DrawString(Combat.Instance.CombatFont, $"Bonus VS Metal: {Log.Instance.MetalBonus * 100}%", new Vector2(1050, 520), Color.White);
                        spriteBatch.DrawString(Combat.Instance.CombatFont, $"Bonus VS Fire: {Log.Instance.FireBonus * 100}%", new Vector2(1050, 600), Color.White);
                        spriteBatch.DrawString(Combat.Instance.CombatFont, $"Bonus VS Air: {Log.Instance.AirBonus * 100}%", new Vector2(1050, 680), Color.White);
                        break;
                }
            }

            if (inventoryState == "Equipment")
            {
                //SkillPlanks
                for (int i = 0; i < 5; i++)
                {
                    spriteBatch.Draw(skillPlank, new Vector2(320, 120 + 160 * i), Color.White);
                }

                if (Equipment.Instance.Weapon != null)
                {
                    spriteBatch.Draw(Equipment.Instance.Weapon.Sprite, new Vector2(135, 120), null, Color.White, 0f, Vector2.Zero, 0.70f, new SpriteEffects(), 1);
                    if (selectedInt == 0)
                    {
                        spriteBatch.Draw(levelCircle, new Vector2(1250, 100), Color.White);

                        if (Equipment.Instance.Weapon.Level < 10)
                        {
                            spriteBatch.DrawString(Combat.Instance.CombatFont, "0" + Equipment.Instance.Weapon.Level, new Vector2(1335, 185), Color.White);
                        }
                        else
                        {
                            spriteBatch.DrawString(Combat.Instance.CombatFont, $"{Equipment.Instance.Weapon.Level}" , new Vector2(1335, 185), Color.White);
                        }

                        spriteBatch.Draw(Combat.Instance.HealthEmpty, new Vector2(1075, 355), Color.White);
                        spriteBatch.Draw(expFull, new Vector2(1077, 357), new Rectangle(0, 0, Convert.ToInt32(Combat.Instance.PercentStat(Equipment.Instance.Weapon.Experience, Equipment.Instance.Weapon.ExperienceRequired) * 5.9), 70), Color.White);
                        spriteBatch.DrawString(Combat.Instance.CombatFont, "Exp left: " + $"{Equipment.Instance.Weapon.ExperienceRequired - Equipment.Instance.Weapon.Experience}", new Vector2(1200, 425), Color.White);
                        spriteBatch.DrawString(GameWorld.Instance.SmallFont,$"{Equipment.Instance.Weapon.SkillEffect.EffectString}", new Vector2(1100, 625), Color.White);
                    }

                    spriteBatch.DrawString(Combat.Instance.CombatFont, Equipment.Instance.Weapon.WeaponName, new Vector2(350, 160), Color.White);
                }

                if (Equipment.Instance.Armor != null)
                {
                    spriteBatch.Draw(Equipment.Instance.Armor.Sprite, new Vector2(135, 280), null, Color.White, 0f, Vector2.Zero, 0.70f, new SpriteEffects(), 1);
                    if (selectedInt == 1)
                    {
                        spriteBatch.Draw(levelCircle, new Vector2(1250, 100), Color.White);

                        if (Equipment.Instance.Armor.Level < 10)
                        {
                            spriteBatch.DrawString(Combat.Instance.CombatFont, "0" + Equipment.Instance.Armor.Level, new Vector2(1335, 185), Color.White);
                        }
                        else
                        {
                            spriteBatch.DrawString(Combat.Instance.CombatFont, $"{Equipment.Instance.Armor.Level}", new Vector2(1335, 185), Color.White);
                        }

                        spriteBatch.Draw(Combat.Instance.HealthEmpty, new Vector2(1075, 355), Color.White);
                        spriteBatch.Draw(expFull, new Vector2(1077, 357), new Rectangle(0, 0, Convert.ToInt32(Combat.Instance.PercentStat(Equipment.Instance.Armor.Experience, Equipment.Instance.Armor.ExperienceRequired) * 5.9), 70), Color.White);
                        spriteBatch.DrawString(Combat.Instance.CombatFont, "Exp left: " + $"{Equipment.Instance.Armor.ExperienceRequired - Equipment.Instance.Armor.Experience}", new Vector2(1200, 425), Color.White);
                        spriteBatch.DrawString(GameWorld.Instance.SmallFont, $"{Equipment.Instance.Armor.SkillEffect.EffectString}", new Vector2(1100, 625), Color.White);
                    }
                    spriteBatch.DrawString(Combat.Instance.CombatFont, Equipment.Instance.Armor.ArmorName, new Vector2(350, 320), Color.White);
                }

                if (Equipment.Instance.Skill1 != null)
                {
                    spriteBatch.Draw(Equipment.Instance.Skill1.Sprite, new Vector2(135, 440), null, Color.White, 0f, Vector2.Zero, 0.70f, new SpriteEffects(), 1);
                    if (selectedInt == 2)
                    {
                        spriteBatch.Draw(levelCircle, new Vector2(1250, 100), Color.White);
                        if (Equipment.Instance.Skill1.Level < 10)
                        {
                            spriteBatch.DrawString(Combat.Instance.CombatFont, "0" + Equipment.Instance.Skill1.Level, new Vector2(1335, 185), Color.White);
                        }
                        else
                        {
                            spriteBatch.DrawString(Combat.Instance.CombatFont, $"{Equipment.Instance.Skill1.Level}", new Vector2(1335, 185), Color.White);
                        }

                        spriteBatch.Draw(Combat.Instance.HealthEmpty, new Vector2(1075, 355), Color.White);
                        spriteBatch.Draw(expFull, new Vector2(1077, 357), new Rectangle(0, 0, Convert.ToInt32(Combat.Instance.PercentStat(Equipment.Instance.Skill1.Experience, Equipment.Instance.Skill1.ExperienceRequired) * 5.9), 70), Color.White);
                        spriteBatch.DrawString(Combat.Instance.CombatFont, "Exp left: " + $"{Equipment.Instance.Skill1.ExperienceRequired - Equipment.Instance.Skill1.Experience}", new Vector2(1200, 425), Color.White);
                        spriteBatch.DrawString(GameWorld.Instance.SmallFont, $"{Equipment.Instance.Skill1.SkillEffect.EffectString}", new Vector2(1100, 625), Color.White);
                    }

                    spriteBatch.DrawString(Combat.Instance.CombatFont, Equipment.Instance.Skill1.SkillName, new Vector2(350, 480), Color.White);
                }

                if (Equipment.Instance.Skill2 != null)
                {
                    spriteBatch.Draw(Equipment.Instance.Skill2.Sprite, new Vector2(135, 600), null, Color.White, 0f, Vector2.Zero, 0.70f, new SpriteEffects(), 1);
                    if (selectedInt == 3)
                    {
                        spriteBatch.Draw(levelCircle, new Vector2(1250, 100), Color.White);

                        if (Equipment.Instance.Skill2.Level < 10)
                        {
                            spriteBatch.DrawString(Combat.Instance.CombatFont, "0" + Equipment.Instance.Skill2.Level, new Vector2(1335, 185), Color.White);
                        }
                        else
                        {
                            spriteBatch.DrawString(Combat.Instance.CombatFont, $"{Equipment.Instance.Skill2.Level}", new Vector2(1335, 185), Color.White);
                        }

                        spriteBatch.Draw(Combat.Instance.HealthEmpty, new Vector2(1075, 355), Color.White);
                        spriteBatch.Draw(expFull, new Vector2(1077, 357), new Rectangle(0, 0, Convert.ToInt32(Combat.Instance.PercentStat(Equipment.Instance.Skill2.Experience, Equipment.Instance.Skill2.ExperienceRequired) * 5.9), 70), Color.White);
                        spriteBatch.DrawString(Combat.Instance.CombatFont, "Exp left: " + $"{Equipment.Instance.Skill2.ExperienceRequired - Equipment.Instance.Skill2.Experience}", new Vector2(1200, 425), Color.White);
                        spriteBatch.DrawString(GameWorld.Instance.SmallFont, $"{Equipment.Instance.Skill2.SkillEffect.EffectString}", new Vector2(1100, 625), Color.White);
                    }

                    spriteBatch.DrawString(Combat.Instance.CombatFont, Equipment.Instance.Skill2.SkillName, new Vector2(350, 640), Color.White);
                }

                if (Equipment.Instance.Skill3 != null)
                {
                    spriteBatch.Draw(Equipment.Instance.Skill3.Sprite, new Vector2(135, 760), null, Color.White, 0f, Vector2.Zero, 0.70f, new SpriteEffects(), 1);
                    if (selectedInt == 4)
                    {
                        spriteBatch.Draw(levelCircle, new Vector2(1250, 100), Color.White);
                        if (Equipment.Instance.Skill3.Level < 10)
                        {
                            spriteBatch.DrawString(Combat.Instance.CombatFont, "0" + Equipment.Instance.Skill3.Level, new Vector2(1335, 185), Color.White);
                        }
                        else
                        {
                            spriteBatch.DrawString(Combat.Instance.CombatFont, $"{Equipment.Instance.Skill3.Level}", new Vector2(1335, 185), Color.White);
                        }
                        spriteBatch.Draw(Combat.Instance.HealthEmpty, new Vector2(1075, 355), Color.White);
                        spriteBatch.Draw(expFull, new Vector2(1077, 357), new Rectangle(0, 0, Convert.ToInt32(Combat.Instance.PercentStat(Equipment.Instance.Skill3.Experience, Equipment.Instance.Skill3.ExperienceRequired) * 5.9), 70), Color.White);
                        spriteBatch.DrawString(Combat.Instance.CombatFont, "Exp left: " + $"{Equipment.Instance.Skill3.ExperienceRequired - Equipment.Instance.Skill3.Experience}", new Vector2(1200, 425), Color.White);
                        spriteBatch.DrawString(GameWorld.Instance.SmallFont, $"{Equipment.Instance.Skill3.SkillEffect.EffectString}", new Vector2(1100, 625), Color.White);
                    }
                    spriteBatch.DrawString(Combat.Instance.CombatFont, Equipment.Instance.Skill3.SkillName, new Vector2(350, 800), Color.White);
                }

                #region Rings
                if (Equipment.Instance.Weapon != null)
                {
                    spriteBatch.Draw(emptyRing, new Vector2(150, 120), null, Color.White, 0f, Vector2.Zero, 0.6f, new SpriteEffects(), 1);
                }
                else
                {
                    spriteBatch.Draw(weaponRing, new Vector2(150, 120), null, Color.White, 0f, Vector2.Zero, 0.6f, new SpriteEffects(), 1);
                }

                if (Equipment.Instance.Armor != null)
                {
                    spriteBatch.Draw(emptyRing, new Vector2(150, 280), null, Color.White, 0f, Vector2.Zero, 0.6f, new SpriteEffects(), 1);
                }
                else
                {
                    spriteBatch.Draw(armorRing, new Vector2(150, 280), null, Color.White, 0f, Vector2.Zero, 0.6f, new SpriteEffects(), 1);
                }

                if (Equipment.Instance.Skill1 != null)
                {
                    spriteBatch.Draw(emptyRing, new Vector2(150, 440), null, Color.White, 0f, Vector2.Zero, 0.6f, new SpriteEffects(), 1);
                }
                else
                {
                    spriteBatch.Draw(skillRing, new Vector2(150, 440), null, Color.White, 0f, Vector2.Zero, 0.6f, new SpriteEffects(), 1);
                }

                if (Equipment.Instance.Skill2 != null)
                {
                    spriteBatch.Draw(emptyRing, new Vector2(150, 600), null, Color.White, 0f, Vector2.Zero, 0.6f, new SpriteEffects(), 1);
                }
                else
                {
                    spriteBatch.Draw(skillRing, new Vector2(150, 600), null, Color.White, 0f, Vector2.Zero, 0.6f, new SpriteEffects(), 1);
                }

                if (Equipment.Instance.Skill3 != null)
                {
                    spriteBatch.Draw(emptyRing, new Vector2(150, 760), null, Color.White, 0f, Vector2.Zero, 0.6f, new SpriteEffects(), 1);
                }
                else
                {
                    spriteBatch.Draw(skillRing, new Vector2(150, 760), null, Color.White, 0f, Vector2.Zero, 0.6f, new SpriteEffects(), 1);
                }
                #endregion

                spriteBatch.Draw(emptyRing, new Vector2(150, 120 + selectedInt * 160), null, Color.Gold, 0f, Vector2.Zero, 0.6f, new SpriteEffects(), 1);
            }

            if (inventoryState == "Inventory")
            {
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Consumables", new Vector2(200, 120), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Monster Stones", new Vector2(200, 200), Color.White);

                switch (selectedInt)
                {
                    //Consumables
                    case 0:
                        spriteBatch.DrawString(Combat.Instance.CombatFont, $"Potions: {Consumable.Potion}", new Vector2(1100, 120), Color.White);
                        spriteBatch.DrawString(Combat.Instance.CombatFont, $"Soul Stones: {Consumable.SoulStone}", new Vector2(1100, 200), Color.White);
                        spriteBatch.DrawString(Combat.Instance.CombatFont, $"Bombs: {Consumable.Bomb}", new Vector2(1100, 280), Color.White);
                        break;
                    //Filled Stones
                    case 1:
                        if (CurrentPage < FilledStone.StoneListPages)
                        {
                            for (int i = 0; i < 9; i++)
                            {
                                spriteBatch.DrawString(Combat.Instance.CombatFont, FilledStone.StoneList[i + (currentPage * 9)].Name, new Vector2(1100, 120 + i * 80), Color.White);
                               
                                spriteBatch.DrawString(Combat.Instance.CombatFont, "lvl" + FilledStone.StoneList[i + (currentPage * 9)].Level, new Vector2(1600, 120 + i * 80), Color.White);

                                if (FilledStone.StoneList[i + (currentPage * 9)].Equipped == true)
                                {
                                    spriteBatch.DrawString(Combat.Instance.CombatFont, "E", new Vector2(1725, 120 + i * 80), Color.Green);
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < FilledStone.StoneList.Count - (FilledStone.StoneListPages * 9); i++)
                            {
                                spriteBatch.DrawString(Combat.Instance.CombatFont, FilledStone.StoneList[i + (currentPage * 9)].Name, new Vector2(1100, 120 + i * 80), Color.White);
                               
                                spriteBatch.DrawString(Combat.Instance.CombatFont, "lvl" + FilledStone.StoneList[i + (currentPage * 9)].Level, new Vector2(1600, 120 + i * 80), Color.White);

                                if (FilledStone.StoneList[i + (currentPage * 9)].Equipped == true)
                                {
                                    spriteBatch.DrawString(Combat.Instance.CombatFont, "E", new Vector2(1725, 120 + i * 80), Color.Green);
                                }
                            }
                        }

                        spriteBatch.DrawString(Combat.Instance.CombatFont, (currentPage + 1) + " / " + (FilledStone.StoneListPages + 1), new Vector2(1300, 870), Color.White);
                        break;
                }
            }

            if (inventoryState == "Log")
            {
                Log.Instance.Draw(spriteBatch, logPage);
                spriteBatch.DrawString(Combat.Instance.CombatFont, $"{logPage +1}", new Vector2(125, 885), Color.White);
            }

            if (inventoryState == "FilledStones")
            {
                if (CurrentPage < FilledStone.StoneListPages)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        spriteBatch.DrawString(Combat.Instance.CombatFont, FilledStone.StoneList[i + (currentPage * 9)].Name, new Vector2(200, 120 + i * 80), Color.White);
                        spriteBatch.DrawString(Combat.Instance.CombatFont, "lvl" + FilledStone.StoneList[i + (currentPage * 9)].Level, new Vector2(700, 120 + i * 80), Color.White);

                        if (FilledStone.StoneList[i + (currentPage * 9)].Equipped == true)
                        {
                            spriteBatch.DrawString(Combat.Instance.CombatFont, "E", new Vector2(850, 120 + i * 80), Color.Green);
                        }
                    }
                }

                else
                {
                    for (int i = 0; i < FilledStone.StoneList.Count - (FilledStone.StoneListPages * 9); i++)
                    {
                        spriteBatch.DrawString(Combat.Instance.CombatFont, FilledStone.StoneList[i + (currentPage * 9)].Name, new Vector2(200, 120 + i * 80), Color.White);
                        spriteBatch.DrawString(Combat.Instance.CombatFont, "lvl" + FilledStone.StoneList[i + (currentPage * 9)].Level, new Vector2(700, 120 + i * 80), Color.White);

                        if (FilledStone.StoneList[i + (currentPage * 9)].Equipped == true)
                        {
                            spriteBatch.DrawString(Combat.Instance.CombatFont, "E", new Vector2(850, 120 + i * 80), Color.Green);
                        }
                    }
                }

                if (FilledStone.StoneList.Count != 0)
                {
                    spriteBatch.Draw(FilledStone.StoneList[selectedInt + CurrentPage * 9].Sprite, new Vector2(1250, 100), Color.White);
                    spriteBatch.DrawString(Combat.Instance.CombatFont, $"{char.ToUpper(FilledStone.StoneList[selectedInt + CurrentPage * 9].Element[0]) + FilledStone.StoneList[selectedInt + CurrentPage * 9].Element.Substring(1)}", new Vector2(1375 - (Combat.Instance.CombatFont.MeasureString(FilledStone.StoneList[selectedInt + CurrentPage * 9].Element).X * 0.5f), 310), Color.White);
                    spriteBatch.Draw(skillPlank, new Vector2(1175, 400), Color.White);
                    spriteBatch.Draw(skillPlank, new Vector2(1175, 600), Color.White);
                    spriteBatch.Draw(skillPlank, new Vector2(1175, 800), Color.White);
                    spriteBatch.Draw(weaponRing, new Vector2(1000, 400), null, Color.White, 0f, Vector2.Zero, 0.6f, new SpriteEffects(), 1);
                    spriteBatch.Draw(armorRing, new Vector2(1000, 600), null, Color.White, 0f, Vector2.Zero, 0.6f, new SpriteEffects(), 1);
                    spriteBatch.Draw(skillRing, new Vector2(1000, 800), null, Color.White, 0f, Vector2.Zero, 0.6f, new SpriteEffects(), 1);
                    spriteBatch.DrawString(GameWorld.Instance.SmallFont, $"{FilledStone.StoneList[selectedInt + CurrentPage * 9].WeaponEffect.EffectString}", new Vector2(1190, 410), Color.White);
                    spriteBatch.DrawString(GameWorld.Instance.SmallFont, $"{FilledStone.StoneList[selectedInt + CurrentPage * 9].ArmorEffect.EffectString}", new Vector2(1190, 610), Color.White);
                    spriteBatch.DrawString(GameWorld.Instance.SmallFont, $"{FilledStone.StoneList[selectedInt + CurrentPage * 9].SkillEffect.EffectString}", new Vector2(1190, 810), Color.White);
                    spriteBatch.DrawString(Combat.Instance.CombatFont, $"DMG: +{FilledStone.StoneList[selectedInt + CurrentPage * 9].Damage}", new Vector2(1260, 80), Color.White);
                    spriteBatch.DrawString(Combat.Instance.CombatFont, $"Max HP: +{FilledStone.StoneList[selectedInt + CurrentPage * 9].MaxHealth}", new Vector2(1260, 160), Color.White);
                    spriteBatch.DrawString(Combat.Instance.CombatFont, $"Def: +{FilledStone.StoneList[selectedInt + CurrentPage * 9].Defense}", new Vector2(1260, 240), Color.White);
                    spriteBatch.DrawString(Combat.Instance.CombatFont,$"Att.Speed: +{FilledStone.StoneList[selectedInt + CurrentPage * 9].AttackSpeed}", new Vector2(1260, 320), Color.White);

                }

                spriteBatch.DrawString(Combat.Instance.CombatFont, (currentPage + 1 ) + " / " + (FilledStone.StoneListPages + 1), new Vector2(400, 900), Color.White);
            }

            if (inventoryState == "Options")
            {
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Sound:", new Vector2(200, 120), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Music:", new Vector2(200, 200), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Keybinds", new Vector2(200, 280), Color.White);

                spriteBatch.DrawString(Combat.Instance.CombatFont, $"{(int)(GameWorld.Instance.SoundEffectVolume * 100)}%", new Vector2(500, 120), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, $"{(int)(GameWorld.Instance.MusicVolume * 100)}%", new Vector2(500, 200), Color.White);
            }

            if (inventoryState == "Keybinds")
            {
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Up", new Vector2(200, 120), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Down", new Vector2(200, 200), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Left", new Vector2(200, 280), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Right", new Vector2(200, 360), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Select", new Vector2(200, 440), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Cancel", new Vector2(200, 520), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Return", new Vector2(200, 600), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Menu", new Vector2(200, 680), Color.White);

                spriteBatch.DrawString(Combat.Instance.CombatFont, Convert.ToString(InputHandler.Instance.KeyUp), new Vector2(600, 120), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, Convert.ToString(InputHandler.Instance.KeyDown), new Vector2(600, 200), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, Convert.ToString(InputHandler.Instance.KeyLeft), new Vector2(600, 280), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, Convert.ToString(InputHandler.Instance.KeyRight), new Vector2(600, 360), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, Convert.ToString(InputHandler.Instance.KeySelect), new Vector2(600, 440), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, Convert.ToString(InputHandler.Instance.KeyCancel), new Vector2(600, 520), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, Convert.ToString(InputHandler.Instance.KeyReturn), new Vector2(600, 600), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, Convert.ToString(InputHandler.Instance.KeyMenu), new Vector2(600, 680), Color.White);
            }

            //Draws a selection arrow to see what you are hovering over
            if (inventoryState != "Equipment" && inventoryState != "Log")
            {
                spriteBatch.Draw(arrow, new Vector2(155, 120 + 80 * selectedInt), Color.White);
            }
        }

        public void ChangeState()
        {
            if (inventoryState == "GeneralMenu")
            {
                switch (selectedInt)
                {
                    case 0:
                        inventoryState = "Character";
                        break;
                    case 1:
                        inventoryState = "Equipment";
                        break;
                    case 2:
                        inventoryState = "Inventory";
                        break;
                    case 3:
                        inventoryState = "Options";
                        break;
                    case 4:
                        inventoryState = "Log";
                        break;
                    case 5:
                        inventoryState = "Save";
                        break;
                    case 6:
                        inventoryState = "Options";
                        break;
                    case 7:
                        inventoryState = "Quit";
                        break;
                }
           }
            else if (inventoryState == "Inventory")
           {
                switch (selectedInt)
                {
                    case 1:
                        inventoryState = "FilledStones";
                        break;
                }
           }
            else if (inventoryState == "FilledStones")
            {
                if (equipping == true)
                {
                    if (FilledStone.StoneList.Count != 0)
                    {
                        //Unequips the current weapon from the slot that is picked
                        switch (equippingTo)
                        {
                            case 0:
                                if (Equipment.Instance.Weapon != null && Equipment.Instance.Weapon.Equipped == true)
                                {
                                    Equipment.Instance.Weapon.Equipped = false;
                                }
                                break;
                            case 1:
                                if (Equipment.Instance.Armor != null && Equipment.Instance.Armor.Equipped == true)
                                {
                                    Equipment.Instance.Armor.Equipped = false;
                                }
                                break;
                            case 2:
                                if (Equipment.Instance.Skill1 != null && Equipment.Instance.Skill1.Equipped == true)
                                {
                                    Equipment.Instance.Skill1.Equipped = false;
                                }
                                break;
                            case 3:
                                if (Equipment.Instance.Skill2 != null && Equipment.Instance.Skill2.Equipped == true)
                                {
                                    Equipment.Instance.Skill2.Equipped = false;
                                }
                                break;
                            case 4:
                                if (Equipment.Instance.Skill3 != null && Equipment.Instance.Skill3.Equipped == true)
                                {
                                    Equipment.Instance.Skill3.Equipped = false;
                                }
                                break;
                        }

                        //Unequips the stone you are trying to equip, if it is equipped in another equipment slot
                        if (FilledStone.StoneList[selectedInt + currentPage * 9].Equipped == true)
                        {
                            FilledStone.StoneList[selectedInt + currentPage * 9].Equipped = false;
                        }

                        //Checks all of the equipment slots and makes them null if the item is no longer equipped to them
                        if (Equipment.Instance.Weapon != null && Equipment.Instance.Weapon.Equipped == false)
                        {
                            Equipment.Instance.Weapon = null ;
                        }
                            
                        if (Equipment.Instance.Armor != null && Equipment.Instance.Armor.Equipped == false)
                        {
                            Equipment.Instance.Armor = null;
                        }
                            
                        if (Equipment.Instance.Skill1 != null && Equipment.Instance.Skill1.Equipped == false)
                        {
                            Equipment.Instance.Skill1 = null;
                        }
                            
                        if (Equipment.Instance.Skill2 != null && Equipment.Instance.Skill2.Equipped == false)
                        {
                            Equipment.Instance.Skill2 = null ;
                        }
                            
                        if (Equipment.Instance.Skill3 != null && Equipment.Instance.Skill3.Equipped == false)
                        {
                            Equipment.Instance.Skill3 = null;
                        }
                        
                        Equipment.Instance.EquipStone(EquippingTo, FilledStone.StoneList[selectedInt + currentPage * 9]);
                        FilledStone.StoneList[selectedInt + currentPage * 9].Equipped = true;
                        EquippingTo = 0;
                        inventoryState = "Equipment";
                    }
                }
            }
            else if (inventoryState == "Equipment")
            {
                Equipping = true;
                EquippingTo = selectedInt;
                inventoryState = "FilledStones";
            }
            else if (inventoryState == "Options")
            {
                if (selectedInt == 2)
                {
                    inventoryState = "Keybinds";
                }
            }
            else if (inventoryState == "Keybinds")
            {
                if (delay > 200)
                {
                    
                    delay = 0;
                    changingKey = true;
                    GameTime tempTime = new GameTime();
                }
                while (changingKey == true)
                {
                    if (Keyboard.GetState().GetPressedKeys() != null)
                    {
                        switch (selectedInt)
                        {
                            case 0:
                                InputHandler.Instance.KeyUp = InputHandler.Instance.ChangeKey(InputHandler.Instance.KeyUp);
                                break;

                            case 1:
                                InputHandler.Instance.KeyDown = InputHandler.Instance.ChangeKey(InputHandler.Instance.KeyDown);
                                break;

                            case 2:
                                InputHandler.Instance.KeyLeft = InputHandler.Instance.ChangeKey(InputHandler.Instance.KeyLeft);
                                break;

                            case 3:
                                InputHandler.Instance.KeyRight = InputHandler.Instance.ChangeKey(InputHandler.Instance.KeyRight);
                                break;

                            case 4:
                                InputHandler.Instance.KeySelect = InputHandler.Instance.ChangeKey(InputHandler.Instance.KeySelect);
                                break;

                            case 5:
                                InputHandler.Instance.KeyCancel = InputHandler.Instance.ChangeKey(InputHandler.Instance.KeyCancel);
                                break;

                            case 6:
                                InputHandler.Instance.KeyReturn = InputHandler.Instance.ChangeKey(InputHandler.Instance.KeyReturn);
                                break;

                            case 7:
                                InputHandler.Instance.KeyMenu = InputHandler.Instance.ChangeKey(InputHandler.Instance.KeyMenu);
                                break;

                        }

                        changingKey = false;
                    }
                    

                    
                }

            }
        }

        //Changes selectedInt which determines what item you are going to press
        public void ChangeSelected(int max)
        {
            if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeyUp) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonUp)) && delay > 150 && SelectedInt > 0)
            {
                SelectedInt--;
                delay = 0;
            }

            if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeyDown) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonDown)) && delay > 150 && SelectedInt < max)
            {
                SelectedInt++;
                delay = 0;
            }
        }
    }
}
