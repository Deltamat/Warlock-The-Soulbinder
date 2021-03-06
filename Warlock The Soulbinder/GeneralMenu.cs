﻿using System;
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
        private static GeneralMenu instance;
        private Texture2D book;
        private Texture2D weaponRing;
        private Texture2D skillRing;
        private Texture2D armorRing;
        private Texture2D emptyRing;
        private Texture2D skillPlank;
        private Texture2D levelCircle;
        private Texture2D expFull;
        private bool fullscreenState = false;
        private int logPage = 0;
        private List<String> menuList = new List<string>();
        private Texture2D arrow;
        private string inventoryState = "GeneralMenu";
        private bool equipping = false;
        private int equippingTo;
        private float delay = 0;
        private int currentPage = 0;
        private int helpPage = 0;
        private bool changingKey = false;

        /// <summary>
        /// Creates an instance for the singleton
        /// </summary>
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
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public int CurrentPage { get => currentPage; set => currentPage = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public int EquippingTo { get => equippingTo; set => equippingTo = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public bool Equipping { get => equipping; set => equipping = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public string InventoryState { get => inventoryState; set => inventoryState = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public bool FullscreenState { get => fullscreenState; set => fullscreenState = value; }

        private GeneralMenu()
        {

        }

        /// <summary>
        /// Loads assets
        /// </summary>
        /// <param name="content">connects to the content folder</param>
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

        /// <summary>
        /// Update method to be called in GameWorld
        /// </summary>
        /// <param name="gameTime"> allows the use of time based code</param>
        public override void Update(GameTime gameTime)
        {
            delay += gameTime.ElapsedGameTime.Milliseconds;

            //Code update depending on inventory state
            switch (InventoryState)
            {
                case "GeneralMenu":
                    ChangeSelected(8);
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
                                    Equipment.Instance.EquippedEquipment[0] = null;
                                }
                                break;

                            case 1:
                                if (Equipment.Instance.Armor != null)
                                {
                                    Equipment.Instance.Armor.Equipped = false;
                                    Equipment.Instance.Armor = null;
                                    Equipment.Instance.EquippedEquipment[1] = null;
                                }
                                break;

                            case 2:
                                if (Equipment.Instance.Skill1 != null)
                                {
                                    Equipment.Instance.Skill1.Equipped = false;
                                    Equipment.Instance.Skill1 = null;
                                    Equipment.Instance.EquippedEquipment[2] = null;
                                }
                                break;

                            case 3:
                                if (Equipment.Instance.Skill2 != null)
                                {
                                    Equipment.Instance.Skill2.Equipped = false;
                                    Equipment.Instance.Skill2 = null;
                                    Equipment.Instance.EquippedEquipment[3] = null;
                                }
                                break;

                            case 4:
                                if (Equipment.Instance.Skill3 != null)
                                {
                                    Equipment.Instance.Skill3.Equipped = false;
                                    Equipment.Instance.Skill3 = null;
                                    Equipment.Instance.EquippedEquipment[4] = null;
                                }
                                break;
                        }
                        Player.Instance.UpdateStats();
                    }
                    break;
                case "FilledStones":

                    //Sets selectedInt depending on the amount of monsters captured
                    if (CurrentPage < FilledStone.StoneListPages)
                    {
                        ChangeSelected(8);
                    }
                    else
                    {
                        ChangeSelected(FilledStone.StoneList.Count - (FilledStone.StoneListPages * 9) - 1);
                    }

                    //Goes to next page in case it has one
                    if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeyRight) || InputHandler.Instance.KeyPressed(Keys.D) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonRight)) && delay > 200 && currentPage < FilledStone.StoneListPages)
                    {
                        currentPage++;
                        delay = 0;
                        selectedInt = 0;
                    }

                    //Goes to last page unless you are on the first one
                    if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeyLeft) || InputHandler.Instance.KeyPressed(Keys.A) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonLeft)) && delay > 200 && currentPage > 0)
                    {
                        currentPage--;
                        delay = 0;
                        selectedInt = 0;
                    }
                    break;
                case "Options":
                    ChangeSelected(3);

                    //Changes sound and music volume for the game
                    if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeyRight) || InputHandler.Instance.KeyPressed(Keys.D) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonRight)) && delay > 30)
                    {
                       
                        if (GameWorld.Instance.SoundEffectVolume < 1 && selectedInt == 0)
                        {
                            GameWorld.Instance.SoundEffectVolume += 0.01f;
                            if (GameWorld.Instance.SoundEffectVolume > 1)
                            {
                                GameWorld.Instance.SoundEffectVolume = 1;
                            }

                        }

                        if (GameWorld.Instance.MusicVolume < 1 && selectedInt == 1)
                        {
                            GameWorld.Instance.MusicVolume += 0.01f;
                            if (GameWorld.Instance.SoundEffectVolume > 1)
                            {
                                GameWorld.Instance.SoundEffectVolume = 1;
                            }

                        }
                    }

                    if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeyLeft) || InputHandler.Instance.KeyPressed(Keys.A) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonLeft)) && delay > 30)
                    {
                        if (GameWorld.Instance.SoundEffectVolume > 0 && selectedInt == 0)
                        {
                            GameWorld.Instance.SoundEffectVolume -= 0.01f;

                            if (GameWorld.Instance.SoundEffectVolume > 1)
                            {
                                GameWorld.Instance.SoundEffectVolume = 1;
                            }
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

                    ////Resets keybinds
                    //if (InputHandler.Instance.KeyPressed(Keys.R))
                    //{
                    //    InputHandler.Instance.ResetKeybinds();
                    //}
                    break;
                case "Log":

                    //Changes pages in Log
                    if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeyRight) || ((InputHandler.Instance.KeyPressed(Keys.D))) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonRight)) && delay > 200 && logPage < 20)
                    {
                        logPage++;
                        delay = 0;
                    }

                    if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeyLeft) || ((InputHandler.Instance.KeyPressed(Keys.A))) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonLeft)) && delay > 200 && logPage > 0)
                    {
                        logPage--;
                        delay = 0;
                    }
                    break;

                case "Help":

                    //Changes pages
                    if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeyRight) || ((InputHandler.Instance.KeyPressed(Keys.D))) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonRight)) && delay > 200 && helpPage < 2)
                    {
                        helpPage+=2;
                        delay = 0;
                        selectedInt = 0;
                    }

                    if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeyLeft) || ((InputHandler.Instance.KeyPressed(Keys.A))) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonLeft)) && delay > 200 && helpPage > 0)
                    {
                        helpPage-=2;
                        delay = 0;
                        selectedInt = 0;
                    }
                    break;

            }   
            
            //Key to execute code dependent on the inventory state
            if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeySelect) || InputHandler.Instance.KeyPressed(Keys.E) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonSelect)) && delay > 200)
            {
                ChangeState();
                delay = 0;
                selectedInt = 0;
            }

            //Key for going back in menus
            if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeyReturn) || InputHandler.Instance.KeyPressed(Keys.R) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonReturn)) && delay > 150 && InventoryState != "GeneralMenu")
            {
                Sound.PlaySound("sound/menuSounds/turnPageBack");
                //case = current menu, inventorystate = go back to this menu
                switch (InventoryState)
                {
                    case "Equipment":
                        InventoryState = "GeneralMenu";
                        selectedInt = 0;
                        break;
                    case "Save":
                        InventoryState = "GeneralMenu";
                        selectedInt = 0;
                        break;
                    case "FilledStones":
                        if (equipping == false)
                        {
                            InventoryState = "GeneralMenu";
                            selectedInt = 0;
                            break;
                        }

                        if (equipping == true)
                        {
                            InventoryState = "Equipment";
                            equipping = false;
                            selectedInt = 0;
                            break;
                        }
                        break;
                    case "Consumables":
                        InventoryState = "Inventory";
                        selectedInt = 0;
                        break;

                    case "Options":
                        InventoryState = "GeneralMenu";
                        selectedInt = 0;
                        break;
                    case "Keybinds":
                        InventoryState = "Options";
                        selectedInt = 0;
                        break;
                    case "Character":
                        InventoryState = "GeneralMenu";
                        selectedInt = 0;
                        break;
                    case "Log":
                        InventoryState = "GeneralMenu";
                        selectedInt = 0;
                        break;
                    case "Help":
                        InventoryState = "GeneralMenu";
                        selectedInt = 0;
                        break;
                }

                //Removes equipment slots for unequipped stones
                foreach (FilledStone stone in FilledStone.StoneList)
                {
                    if (stone.Equipped == false)
                    {
                        stone.EquipmentSlot = null;
                    }
                }
                delay = 0;
            }
        }


        /// <summary>
        /// Draw method with the base color of whatever is being drawn
        /// </summary>
        /// <param name="spriteBatch"> spritebatch </param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GameWorld.Instance.Background, Vector2.Zero, Color.White);
            spriteBatch.Draw(book, new Vector2(50, 20), Color.White);

            if (InventoryState == "GeneralMenu")
            { 
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Character", new Vector2(200, 120), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Equipment", new Vector2(200, 200), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Soul Stones", new Vector2(200, 280), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Help", new Vector2(200, 360), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Log", new Vector2(200, 440), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Save", new Vector2(200, 520), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Options", new Vector2(200, 600), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Quit to Desktop", new Vector2(200, 680), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Quit to Main Menu", new Vector2(200, 760), Color.White);
                
                switch (selectedInt)
                {
                    //Draws character stats
                    case 0:
                        spriteBatch.DrawString(Combat.Instance.CombatFont, $"Health: {Player.Instance.CurrentHealth} / {Player.Instance.MaxHealth} ", new Vector2(1100, 120), Color.White);
                        if (Equipment.Instance.Weapon != null)
                        {
                            spriteBatch.DrawString(Combat.Instance.CombatFont, $"Damage: {Player.Instance.TotalDamage} {Equipment.Instance.Weapon.Element}", new Vector2(1100, 200), Color.White);
                        }
                        
                        else
                        {
                            spriteBatch.DrawString(Combat.Instance.CombatFont, $"Damage: {Player.Instance.TotalDamage} neutral", new Vector2(1100, 200), Color.White);
                        }

                        spriteBatch.DrawString(Combat.Instance.CombatFont, $"Resistance:", new Vector2(1100, 320), Color.White);

                        if (Equipment.Instance.Armor != null)
                        {
                            switch (Equipment.Instance.Armor.Element)
                            {
                                case "neutral":
                                    spriteBatch.DrawString(Combat.Instance.CombatFont, $"{(int)Player.Instance.EarthResistance}% all", new Vector2(1450, 320), Color.White);
                                    break;

                                case "earth":
                                    spriteBatch.DrawString(Combat.Instance.CombatFont, $"{(int)Player.Instance.EarthResistance}% earth", new Vector2(1450, 280), Color.White);
                                    spriteBatch.DrawString(Combat.Instance.CombatFont, $"{(int)Player.Instance.DarkResistance}% dark", new Vector2(1450, 360), Color.White);
                                    break;
                                case "air":
                                    spriteBatch.DrawString(Combat.Instance.CombatFont, $"{(int)Player.Instance.AirResistance}% air", new Vector2(1450, 280), Color.White);
                                    spriteBatch.DrawString(Combat.Instance.CombatFont, $"{(int)Player.Instance.EarthResistance}% earth", new Vector2(1450, 360), Color.White);
                                    break;
                                case "water":
                                    spriteBatch.DrawString(Combat.Instance.CombatFont, $"{(int)Player.Instance.WaterResistance}% water", new Vector2(1450, 280), Color.White);
                                    spriteBatch.DrawString(Combat.Instance.CombatFont, $"{(int)Player.Instance.AirResistance}% air", new Vector2(1450, 360), Color.White);
                                    break;
                                case "fire":
                                    spriteBatch.DrawString(Combat.Instance.CombatFont, $"{(int)Player.Instance.FireResistance}% fire", new Vector2(1450, 280), Color.White);
                                    spriteBatch.DrawString(Combat.Instance.CombatFont, $"{(int)Player.Instance.WaterResistance}% water", new Vector2(1450, 360), Color.White);
                                    break;
                                case "metal":
                                    spriteBatch.DrawString(Combat.Instance.CombatFont, $"{(int)Player.Instance.MetalResistance}% metal", new Vector2(1450, 280), Color.White);
                                    spriteBatch.DrawString(Combat.Instance.CombatFont, $"{(int)Player.Instance.FireResistance}% fire", new Vector2(1450, 360), Color.White);
                                    break;
                                case "dark":
                                    spriteBatch.DrawString(Combat.Instance.CombatFont, $"{(int)Player.Instance.DarkResistance}% dark", new Vector2(1450, 280), Color.White);
                                    spriteBatch.DrawString(Combat.Instance.CombatFont, $"{(int)Player.Instance.MetalResistance}% metal", new Vector2(1450, 360), Color.White);
                                    break;


                            }
                        }

                        else
                        {
                            spriteBatch.DrawString(Combat.Instance.CombatFont, $"none", new Vector2(1450, 280), Color.White);
                            spriteBatch.DrawString(Combat.Instance.CombatFont, $"none", new Vector2(1450, 360), Color.White);
                        }

                        spriteBatch.DrawString(Combat.Instance.CombatFont, $"Defense: {Player.Instance.Defense}", new Vector2(1100, 440), Color.White);
                        spriteBatch.DrawString(Combat.Instance.CombatFont, $"Attack Speed: {Player.Instance.AttackSpeed}", new Vector2(1100, 520), Color.White);
                        break;

                    //Draws Equipment stone if it is equipped, if not it draws an empty ring of a specific type
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

                        //Draws the soulstone monster pages
                    case 2:
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

                        if (FilledStone.StoneList.Count > 0)
                        {
                            spriteBatch.DrawString(Combat.Instance.CombatFont, (currentPage + 1) + " / " + (FilledStone.StoneListPages + 1), new Vector2(1300, 870), Color.White);
                        }
                       
                        //Says to  capture monsters if you have none.
                        else
                        {
                            spriteBatch.DrawString(Combat.Instance.CombatFont, "Capture monsters to \nsee them here", new Vector2(1050, 150), Color.White);
                        }
                        break;

                    //Draws the log if any scans has been done, if not then it says to find a sentry
                    case 4:
                        if (Log.Instance.LogBegun == true)
                        {
                            spriteBatch.DrawString(Combat.Instance.CombatFont, $"Complete Scans: {Log.Instance.FullScans()} / 21", new Vector2(1050, 120), Color.White);
                            spriteBatch.DrawString(Combat.Instance.CombatFont, $"Bonus VS Neutral: {Log.Instance.NeutralBonus * 100}%", new Vector2(1050, 200), Color.White);
                            spriteBatch.DrawString(Combat.Instance.CombatFont, $"Bonus VS Earth: {Log.Instance.EarthBonus * 100}%", new Vector2(1050, 280), Color.White);
                            spriteBatch.DrawString(Combat.Instance.CombatFont, $"Bonus VS Water: {Log.Instance.WaterBonus * 100}%", new Vector2(1050, 360), Color.White);
                            spriteBatch.DrawString(Combat.Instance.CombatFont, $"Bonus VS Dark: {Log.Instance.DarkBonus * 100}%", new Vector2(1050, 440), Color.White);
                            spriteBatch.DrawString(Combat.Instance.CombatFont, $"Bonus VS Metal: {Log.Instance.MetalBonus * 100}%", new Vector2(1050, 520), Color.White);
                            spriteBatch.DrawString(Combat.Instance.CombatFont, $"Bonus VS Fire: {Log.Instance.FireBonus * 100}%", new Vector2(1050, 600), Color.White);
                            spriteBatch.DrawString(Combat.Instance.CombatFont, $"Bonus VS Air: {Log.Instance.AirBonus * 100}%", new Vector2(1050, 680), Color.White);
                        }
                                
                        else
                        {
                            spriteBatch.DrawString(Combat.Instance.CombatFont, $"Capture sentry to \nstart scanning enemies\nfor bonuses", new Vector2(1050, 150), Color.White);
                        }
                       

                        break;
                }
            }

            if (InventoryState == "Equipment")
            {
                //SkillPlanks
                for (int i = 0; i < 5; i++)
                {
                    spriteBatch.Draw(skillPlank, new Vector2(320, 120 + 160 * i), Color.White);
                }

                //Draws everything for the weapon, sprite, level, EXP, last EXP, and effect
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
                        spriteBatch.DrawString(Combat.Instance.CombatFont, "Exp last fight:" + $"{Equipment.Instance.Weapon.ExperienceLastEncounter}", new Vector2(1200, 500), Color.Gold);
                        spriteBatch.DrawString(GameWorld.Instance.SmallFont,$"{Equipment.Instance.Weapon.WeaponEffect.EffectString}", new Vector2(1100, 625), Color.White);
                    }

                    spriteBatch.DrawString(Combat.Instance.CombatFont, Equipment.Instance.Weapon.WeaponName, new Vector2(350, 160), Color.White);
                }

                //Draws everything for the armor, sprite, level, EXP, last EXP, and effect
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
                        spriteBatch.DrawString(GameWorld.Instance.SmallFont, $"{Equipment.Instance.Armor.ArmorEffect.EffectString}", new Vector2(1100, 625), Color.White);
                        spriteBatch.DrawString(Combat.Instance.CombatFont, "Exp last fight:" + $"{Equipment.Instance.Armor.ExperienceLastEncounter}", new Vector2(1200, 500), Color.Gold);
                    }
                    spriteBatch.DrawString(Combat.Instance.CombatFont, Equipment.Instance.Armor.ArmorName, new Vector2(350, 320), Color.White);
                }

                //Draws everything for the skill, sprite, level, EXP, last EXP, and effect
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
                        spriteBatch.DrawString(Combat.Instance.CombatFont, "Exp last fight:" + $"{Equipment.Instance.Skill1.ExperienceLastEncounter}", new Vector2(1200, 500), Color.Gold);
                    }

                    spriteBatch.DrawString(Combat.Instance.CombatFont, Equipment.Instance.Skill1.SkillName, new Vector2(350, 480), Color.White);
                }

                //Draws everything for the skill, sprite, level, EXP, last EXP, and effect
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
                        spriteBatch.DrawString(Combat.Instance.CombatFont, "Exp last fight:" + $"{Equipment.Instance.Skill2.ExperienceLastEncounter}", new Vector2(1200, 500), Color.Gold);
                    }

                    spriteBatch.DrawString(Combat.Instance.CombatFont, Equipment.Instance.Skill2.SkillName, new Vector2(350, 640), Color.White);
                }

                //Draws everything for the skill, sprite, level, EXP, last EXP, and effect
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
                        spriteBatch.DrawString(Combat.Instance.CombatFont, "Exp last fight:" + $"{Equipment.Instance.Skill3.ExperienceLastEncounter}", new Vector2(1200, 500), Color.Gold);
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

            //Draws the appropiate log page
            if (InventoryState == "Log")
            {
                Log.Instance.Draw(spriteBatch, logPage);
                spriteBatch.DrawString(Combat.Instance.CombatFont, $"{logPage +1}", new Vector2(125, 885), Color.White);
            }


            //Draws the monster stone pages
            if (InventoryState == "FilledStones")
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

                //Draws all of the skills that the stone has alongside slot and skillplank
                if (FilledStone.StoneList.Count != 0)
                {
                    spriteBatch.Draw(FilledStone.StoneList[selectedInt + CurrentPage * 9].Sprite, new Vector2(1000, 100), Color.White);
                    spriteBatch.DrawString(Combat.Instance.CombatFont, $"{char.ToUpper(FilledStone.StoneList[selectedInt + CurrentPage * 9].Element[0]) + FilledStone.StoneList[selectedInt + CurrentPage * 9].Element.Substring(1)}", new Vector2(1125 - (Combat.Instance.CombatFont.MeasureString(FilledStone.StoneList[selectedInt + CurrentPage * 9].Element).X * 0.5f), 310), Color.White);
                    spriteBatch.Draw(skillPlank, new Vector2(1175, 400), Color.White);
                    spriteBatch.Draw(skillPlank, new Vector2(1175, 600), Color.White);
                    spriteBatch.Draw(skillPlank, new Vector2(1175, 800), Color.White);
                    spriteBatch.Draw(weaponRing, new Vector2(1000, 400), null, Color.White, 0f, Vector2.Zero, 0.6f, new SpriteEffects(), 1);
                    spriteBatch.Draw(armorRing, new Vector2(1000, 600), null, Color.White, 0f, Vector2.Zero, 0.6f, new SpriteEffects(), 1);
                    spriteBatch.Draw(skillRing, new Vector2(1000, 800), null, Color.White, 0f, Vector2.Zero, 0.6f, new SpriteEffects(), 1);
                    spriteBatch.DrawString(GameWorld.Instance.SmallFont, $"{FilledStone.StoneList[selectedInt + CurrentPage * 9].WeaponEffect.EffectString}", new Vector2(1190, 410), Color.White);
                    spriteBatch.DrawString(GameWorld.Instance.SmallFont, $"{FilledStone.StoneList[selectedInt + CurrentPage * 9].ArmorEffect.EffectString}", new Vector2(1190, 610), Color.White);
                    spriteBatch.DrawString(GameWorld.Instance.SmallFont, $"{FilledStone.StoneList[selectedInt + CurrentPage * 9].SkillEffect.EffectString}", new Vector2(1190, 810), Color.White);
                    spriteBatch.DrawString(Combat.Instance.CombatFont, $"DMG: +{FilledStone.StoneList[selectedInt + CurrentPage * 9].TotalDamage}", new Vector2(1260, 80), Color.White);
                    spriteBatch.DrawString(Combat.Instance.CombatFont, $"Max HP: +{FilledStone.StoneList[selectedInt + CurrentPage * 9].MaxHealth}", new Vector2(1260, 160), Color.White);
                    spriteBatch.DrawString(Combat.Instance.CombatFont, $"Def: +{FilledStone.StoneList[selectedInt + CurrentPage * 9].Defense}", new Vector2(1260, 240), Color.White);
                    spriteBatch.DrawString(Combat.Instance.CombatFont,$"Att.Speed: +{FilledStone.StoneList[selectedInt + CurrentPage * 9].AttackSpeed}", new Vector2(1260, 320), Color.White);

                }

                spriteBatch.DrawString(Combat.Instance.CombatFont, (currentPage + 1 ) + " / " + (FilledStone.StoneListPages + 1), new Vector2(400, 900), Color.White);
            }

            //Draws options
            if (InventoryState == "Options")
            {
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Sound:", new Vector2(200, 120), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Music:", new Vector2(200, 200), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Keybinds", new Vector2(200, 280), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, $"Fullscreen: {fullscreenState}", new Vector2(200, 360), Color.White);

                spriteBatch.DrawString(Combat.Instance.CombatFont, $"{(int)(GameWorld.Instance.SoundEffectVolume * 100)}%", new Vector2(500, 120), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, $"{(int)(GameWorld.Instance.MusicVolume * 100)}%", new Vector2(500, 200), Color.White);
            }

            //Draws keybinds
            if (InventoryState == "Keybinds")
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


            if (InventoryState == "Help")
            {
                switch (helpPage)
                {
                    case 0:
                        spriteBatch.DrawString(Combat.Instance.CombatFont, "Introduction", new Vector2(545-(Combat.Instance.CombatFont.MeasureString("Introduction").X/2), 120), Color.White);
                        spriteBatch.DrawString(GameWorld.Instance.SmallFont, "Welcome to the path of the warlock, this book shall \nbe your guide on the path to becoming a true warlock.", new Vector2(130, 200), Color.White);
                        spriteBatch.DrawString(GameWorld.Instance.SmallFont, "Right now you are only a novice, to become a true \nwarlock you must slay the seven dragon aspects  \nat the dragon shrine, however these dragons are \nfearsome elemental forces that will kill anyone \nwho challenges them unprepared. ", new Vector2(130, 300), Color.White);
                        spriteBatch.DrawString(GameWorld.Instance.SmallFont, "But fear not! For even a novice warlock has power \nabove normal humans, you have the power to control \nsouls, allowing you to capture the souls of monsters \nto do your bidding, and even makes you unable \nto truly die forever! \n\nRead on to figure out just how \nyou can use this to your advantage.", new Vector2(130, 520), Color.White);

                        spriteBatch.DrawString(Combat.Instance.CombatFont, "Elements", new Vector2(1400 - (Combat.Instance.CombatFont.MeasureString("Elements").X / 2), 120), Color.White);
                        spriteBatch.DrawString(GameWorld.Instance.SmallFont, "Each monster in the world is created with an element \nthese elements affects their attacks and defense. \nAll monsters except neutral has protections against \ntheir own element and are strong against another one", new Vector2(990, 200), Color.White);
                        spriteBatch.DrawString(GameWorld.Instance.SmallFont, "The elemental ecosystem is: earth -> air -> water -> \nfire -> metal -> dark -> earth. Remembering this will \nbe crucial for succeeding.", new Vector2(990, 400), Color.White);
                        break;
                    case 1:
                        
                        break;
                    case 2:
                        spriteBatch.DrawString(Combat.Instance.CombatFont, "Capturing", new Vector2(545 - (Combat.Instance.CombatFont.MeasureString("Capturing").X / 2), 120), Color.White);
                        spriteBatch.DrawString(GameWorld.Instance.SmallFont, "Due to your powers you are able to capture monsters \nfor personal use, however since you are only a novice \nthey need to be weakened in combat first the weaker  \nthey are, the easier it is to capture.", new Vector2(130, 200), Color.White);
                        spriteBatch.DrawString(GameWorld.Instance.SmallFont, "When you capture a monster you convert their soul \ninto a stone which inherits the strengths, weaknesses \nand skills of each monster. Each monster shares the \nsame unique skills, but has different strengths and \nweaknesses, so make sure to capture plenty to find \nthe strongest monster, when you have \nyou are now ready to use it, and make it stronger.", new Vector2(130, 400), Color.White);

                        spriteBatch.DrawString(Combat.Instance.CombatFont, "Equipment", new Vector2(1400 - (Combat.Instance.CombatFont.MeasureString("Equipment").X / 2), 120), Color.White);
                        spriteBatch.DrawString(GameWorld.Instance.SmallFont, "Since you are only a novice warlock you cannot \ndirectly control the monsters, you need \nto equip them upon yourself to use their power. \nHowever when you have, you will gain the \ndamage, speed, health, defenses, and depending \non the thing you equip it to, different ways of \nusing their skills, the monsters element for \nyour weapon determines its damage element, and \nfor the armor determines its protection. So make \nsure to take into acount what you are fighting, \nwhen selecting armor and weapon stones.", new Vector2(990, 200), Color.White);
                        spriteBatch.DrawString(GameWorld.Instance.SmallFont, "In addition, all equipped soul stones \ngains experience whenever you kill a monster, the \nstronger the monster the higher the experience, this \nis how you will gain enough power to defeat \nthe dragons!", new Vector2(990, 700), Color.White);
                        break;

                    case 4:
                        spriteBatch.DrawString(Combat.Instance.CombatFont, "Ending", new Vector2(545 - (Combat.Instance.CombatFont.MeasureString("Introduction").X / 2), 120), Color.White);
                        break;

                }

                spriteBatch.DrawString(Combat.Instance.CombatFont, $"{helpPage + 1}", new Vector2(125, 885), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, $"{helpPage + 2}", new Vector2(1730, 885), Color.White);

            }

            //Draws a selection arrow to see what you are hovering over
            if (InventoryState != "Equipment" && InventoryState != "Log" && InventoryState != "Help")
            {
                spriteBatch.Draw(arrow, new Vector2(155, 120 + 80 * selectedInt), Color.White);
            }
        }

        /// <summary>
        /// Determines what happens when the "keySelect" is pressed depending on what inventory state is currently in
        /// </summary>
        public void ChangeState()
        { 
            if (InventoryState == "GeneralMenu")
            {
                if (selectedInt != 0)
                {
                    Sound.PlaySound("sound/menuSounds/turnPage");
                }

                switch (selectedInt)
                {
                    case 1:
                        InventoryState = "Equipment";
                        break;
                    case 2:
                        InventoryState = "FilledStones";
                        break;
                    case 3:
                        InventoryState = "Help";
                        break;
                    case 4:
                        if (Log.Instance.LogBegun == true)
                        {
                            InventoryState = "Log";
                        }
                        break;
                    case 5:
                        GameWorld.Instance.SaveToDBThreadMaker();
                        break;
                    case 6:
                        InventoryState = "Options";
                        break;
                    case 7:
                        GameWorld.Instance.Exit();
                        break;
                    case 8:
                        if (!GameWorld.Instance.Saving)
                        {
                            Combat.Instance.EarthDragonDead = false;
                            Combat.Instance.FireDragonDead = false;
                            Combat.Instance.DarkDragonDead = false;
                            Combat.Instance.MetalDragonDead = false;
                            Combat.Instance.WaterDragonDead = false;
                            Combat.Instance.AirDragonDead = false;
                            Combat.Instance.NeutralDragonDead = false;
                            FilledStone.StoneList.Clear();
                            Equipment.Instance.Weapon = null;
                            Equipment.Instance.Armor = null;
                            Equipment.Instance.Skill1 = null;
                            Equipment.Instance.Skill2 = null;
                            Equipment.Instance.Skill3 = null;
                            FilledStone.StoneListPages = 0;
                            Log.Instance.ResetForMainMenu();
                            Log.Instance.CalculateBonus();
                            GameWorld.Instance.currentZone = "Town";
                            Player.Instance.BaseStats();
                            Player.Instance.CurrentHealth = Player.Instance.MaxHealth;
                            Player.Instance.Position = new Vector2(1200);
                            MainMenu.Instance.MainMenuState = "Main";
                            GameWorld.Instance.GameState = "MainMenu";
                            MainMenu.Instance.Delay = 0;
                        }                        
                        break;
                }
            }
            else if (InventoryState == "FilledStones")
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
                            Equipment.Instance.Weapon.EquipmentSlot = null;
                            Equipment.Instance.EquippedEquipment[0].EquipmentSlot = null;
                            Equipment.Instance.Weapon = null;
                            Equipment.Instance.EquippedEquipment[0] = null;

                        }
                            
                        if (Equipment.Instance.Armor != null && Equipment.Instance.Armor.Equipped == false)
                        {
                            Equipment.Instance.Armor.EquipmentSlot = null;
                            Equipment.Instance.EquippedEquipment[1].EquipmentSlot = null;
                            Equipment.Instance.Armor = null;
                            Equipment.Instance.EquippedEquipment[1] = null;
                            
                        }
                            
                        if (Equipment.Instance.Skill1 != null && Equipment.Instance.Skill1.Equipped == false)
                        {
                            Equipment.Instance.Skill1.EquipmentSlot = null;
                            Equipment.Instance.EquippedEquipment[2].EquipmentSlot = null;
                            Equipment.Instance.Skill1 = null;
                            Equipment.Instance.EquippedEquipment[2] = null;

                        }
                            
                        if (Equipment.Instance.Skill2 != null && Equipment.Instance.Skill2.Equipped == false)
                        {
                            Equipment.Instance.Skill2.EquipmentSlot = null;
                            Equipment.Instance.EquippedEquipment[3].EquipmentSlot = null;
                            Equipment.Instance.Skill2 = null;
                            Equipment.Instance.EquippedEquipment[3] = null;

                        }
                            
                        if (Equipment.Instance.Skill3 != null && Equipment.Instance.Skill3.Equipped == false)
                        {
                            Equipment.Instance.Skill3.EquipmentSlot = null;
                            Equipment.Instance.EquippedEquipment[4].EquipmentSlot = null;
                            Equipment.Instance.Skill3 = null;
                            Equipment.Instance.EquippedEquipment[4] = null;

                        }
                        
                        Equipment.Instance.EquipStone(EquippingTo, FilledStone.StoneList[selectedInt + currentPage * 9]);
                        FilledStone.StoneList[selectedInt + currentPage * 9].Equipped = true;
                        EquippingTo = 0;
                        InventoryState = "Equipment";
                    }
                }
            }
            else if (InventoryState == "Equipment")
            {
                Sound.PlaySound("sound/menuSounds/turnPage");
                Equipping = true;
                EquippingTo = selectedInt;
                InventoryState = "FilledStones";
            }
            else if (InventoryState == "Options")
            {
                if (selectedInt == 2)
                {
                    InventoryState = "Keybinds";
                    Sound.PlaySound("sound/menuSounds/turnPage");
                }

                if (selectedInt == 3)
                {
                    if (fullscreenState == true)
                    {
                        fullscreenState = false;
                        GameWorld.Instance.Graphics.IsFullScreen = false;
                        GameWorld.Instance.Graphics.ApplyChanges();
                    }

                    else if (fullscreenState == false)
                    {
                        fullscreenState = true;
                        GameWorld.Instance.Graphics.IsFullScreen = true;
                        GameWorld.Instance.Graphics.ApplyChanges();
                    }
                }
            }
            else if (InventoryState == "Keybinds")
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

        /// <summary>
        /// Changes the selected menu item depending on the key pressed
        /// </summary>
        /// <param name="max">determines max selection to avoid crashing due to being out of index range</param>
        public void ChangeSelected(int max)
        {
            if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeyUp) || InputHandler.Instance.KeyPressed(Keys.W) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonUp)) && delay > 150 && SelectedInt > 0)
            {
                Sound.PlaySound("sound/menuSounds/button");
                SelectedInt--;
                delay = 0;
            }

            if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeyDown) || InputHandler.Instance.KeyPressed(Keys.S) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonDown)) && delay > 150 && SelectedInt < max)
            {
                Sound.PlaySound("sound/menuSounds/button");
                SelectedInt++;
                delay = 0;
            }
        }
    }
}