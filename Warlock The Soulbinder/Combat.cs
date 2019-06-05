using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Warlock_The_Soulbinder
{
    public class Combat : Menu
    {
        private Enemy target;

        static Combat instance;
        private Texture2D sheet;
        private List<GameObject> playerText = new List<GameObject>();
        private List<GameObject> enemyText = new List<GameObject>();
        private List<GameObject> toBeRemovedPlayerText = new List<GameObject>();
        private List<GameObject> toBeRemovedEnemyText = new List<GameObject>();
        private Texture2D emptyButton;
        private Texture2D healthEmpty;
        private Texture2D healthFull;
        private Texture2D blankFull;
        private Texture2D turnFull;
        private SpriteFont combatFont;
        private float combatDelay = 0;
        private float playerAttackTimer;
        private float enemyAttackTimer;
        private float turnTimer = 1;
        private List<Effect> toBeRemovedEffects = new List<Effect>();
        private int playerAttackAmount = 1;
        private int enemyAttackAmount = 1;
        private float playerDamageReduction = 1;
        private float enemyDamageReduction = 1;
        private int playerDamageAbs;
        private int enemyDamageAbs;
        private float playerSpeedMod = 1;
        private float enemySpeedMod = 1;
        private int playerShield;
        private int enemyShield;

        private Color buttonColor = Color.White;
        Sound victorySound = new Sound("battleVictory");

        private List<Effect> enemyEffects = new List<Effect>();
        private List<Effect> playerEffects = new List<Effect>();

        //For use when you have to change forexample in skills or items
        private string buttonType = "Normal";
        private List<GameObject> emptyButtonList = new List<GameObject>();

        public bool fireDragonDead { get; set; } = false;
        public bool waterDragonDead { get; set; } = false;
        public bool earthDragonDead { get; set; } = false;
        public bool metalDragonDead { get; set; } = false;
        public bool neutralDragonDead { get; set; } = false;
        public bool airDragonDead { get; set; } = false;
        public bool darkDragonDead { get; set; } = false;

        public SpriteFont CombatFont { get => combatFont; private set => combatFont = value; }

        public static Combat Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Combat();
                }
                return instance;
            }
        }

        public Texture2D HealthEmpty { get => healthEmpty; set => healthEmpty = value; }
        public Texture2D HealthFull { get => healthFull; set => healthFull = value; }
        public Texture2D BlankFull { get => blankFull; set => blankFull = value; }
        public List<GameObject> PlayerText { get => playerText; set => playerText = value; }
        public List<GameObject> EnemyText { get => enemyText; set => enemyText = value; }
        public Enemy Target { get => target; set => target = value; }

        private Combat()
        {
            
        }

        //Loads the assets and list of buttons
        public void LoadContent(ContentManager content)
        {
            sheet = content.Load<Texture2D>("Sheet");
            emptyButton = content.Load<Texture2D>("buttons/emptyButton");
            emptyButtonList.Add(new GameObject(new Vector2(812, 310),"buttons/emptyButton", content));
            emptyButtonList.Add(new GameObject(new Vector2(812, 415), "buttons/emptyButton", content));
            emptyButtonList.Add(new GameObject(new Vector2(812, 520), "buttons/emptyButton", content));
            emptyButtonList.Add(new GameObject(new Vector2(812, 625), "buttons/emptyButton", content));
            CombatFont = content.Load<SpriteFont>("combatFont");
            HealthEmpty = content.Load<Texture2D>("HealthEmpty");
            HealthFull = content.Load<Texture2D>("HealthFull");
            BlankFull = content.Load<Texture2D>("BlankFull");
            turnFull = content.Load<Texture2D>("TurnFull");
        }

        public override void Update(GameTime gameTime)
        {
            combatDelay += gameTime.ElapsedGameTime.Milliseconds;

            if (Target != null && playerAttackTimer < turnTimer && enemyAttackTimer < turnTimer && !Player.Instance.Attacking && !Player.Instance.Hurt && !Player.Instance.AttackStart && !Player.Instance.HurtStart)
            {
                buttonColor = Color.Gray;
                playerAttackTimer += Player.Instance.AttackSpeed * playerSpeedMod;
                enemyAttackTimer += Target.AttackSpeed * enemySpeedMod;
            }


            else if (playerAttackTimer >= turnTimer)
            {
                buttonColor = Color.White;
            }

            if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeySelect) || InputHandler.Instance.KeyPressed(Keys.E) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonSelect)) && combatDelay > 200 && playerAttackTimer >= turnTimer)
            {
                CombatEvent();
                combatDelay = 0;
            }

            if (enemyAttackTimer >= turnTimer)
            {
                if (target.Dragon)
                {
                    DragonTurn();
                }
                else
                {
                    EnemyTurn();
                }
            }

            if (Target != null)
            {
                if (Target.CurrentHealth <= 0) //if the target dies, remove target
                {
                    Equipment.Instance.ExperienceEquipment((int)(20 * Math.Pow(1.2, Target.Level)));

                    if (Target.Monster.Contains("Dragon")) // if enemy is a dragon mark the dragon as dead
                    {
                        switch (Target.Monster)
                        {
                            case "fireDragon":
                                fireDragonDead = true;
                                break;
                            case "waterDragon":
                                waterDragonDead = true;
                                break;
                            case "metalDragon":
                                metalDragonDead = true;
                                break;
                            case "earthDragon":
                                earthDragonDead = true;
                                break;
                            case "airDragon":
                                airDragonDead = true;
                                break;
                            case "neutralDragon":
                                neutralDragonDead = true;
                                break;
                            case "darkDragon":
                                darkDragonDead = true;
                                break;
                        }
                        GameWorld.Instance.currentZone = "Dragon";
                        GameWorld.Instance.CurrentZone().ChangeDragonPillarSprite();
                        GameWorld.Instance.currentZone = "DragonRealm";

                    }

                    Target.Alive = false;
                    GameWorld.Instance.CurrentZone().Enemies.Remove(Target);
                    ExitCombat();
                }

                //Scrolls playerText
                foreach (GameObject stringObject in  playerText)
                {
                    stringObject.StringPosition += new Vector2(0, -1);

                    if (stringObject.Position.X < 0)
                    {
                        toBeRemovedPlayerText.Add(stringObject);
                    }
                }

                //Scrolls enemyText
                foreach (GameObject stringObject in enemyText)
                {
                    stringObject.StringPosition += new Vector2(0, -1);

                    if (stringObject.Position.X < 0)
                    {
                        toBeRemovedEnemyText.Add(stringObject);
                    }
                }

                foreach (GameObject stringObject in toBeRemovedPlayerText)
                {
                    PlayerText.Remove(stringObject);
                }

                foreach (GameObject stringObject in toBeRemovedEnemyText)
                {
                    EnemyText.Remove(stringObject);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sheet, new Vector2(800, 300), Color.White);

            for (int i = 0; i < emptyButtonList.Count; i++)
            {
                if (i == SelectedInt)
                {
                    emptyButtonList[i].Draw(spriteBatch, Color.Gray);
                }
                else
                {
                    emptyButtonList[i].Draw(spriteBatch);
                }
            }

            //Draws the button text
            if (buttonType == "Normal")
            {
                spriteBatch.DrawString(CombatFont, "Attack", emptyButtonList[0].Position + new Vector2(50, 7), buttonColor);
                spriteBatch.DrawString(CombatFont, "Skills", emptyButtonList[1].Position + new Vector2(50, 7), buttonColor);

                if (target.Dragon == true)
                {
                    spriteBatch.DrawString(CombatFont, "Capture", emptyButtonList[2].Position + new Vector2(50, 7), Color.Black);
                }

                else
                {
                    spriteBatch.DrawString(CombatFont, "Capture", emptyButtonList[2].Position + new Vector2(50, 7), buttonColor);
                }
               
                spriteBatch.DrawString(CombatFont, "Flee", emptyButtonList[3].Position + new Vector2(50, 7), buttonColor);
            }
            else if (buttonType == "Skills")
            {
                if (Equipment.Instance.Skill1 != null)
                {
                    if (Equipment.Instance.Skill1.InternalCooldown == 0)
                    {
                        spriteBatch.DrawString(CombatFont, Equipment.Instance.Skill1.SkillName, emptyButtonList[0].Position + new Vector2(50, 7), Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(CombatFont, Equipment.Instance.Skill1.SkillName, emptyButtonList[0].Position + new Vector2(50, 7), Color.Gray);
                        spriteBatch.DrawString(CombatFont, $"{Equipment.Instance.Skill1.InternalCooldown}", emptyButtonList[0].Position + new Vector2(-100, 7), Color.Gray);
                    }
                }

                if (Equipment.Instance.Skill2 != null)
                {
                    if (Equipment.Instance.Skill2.InternalCooldown == 0)
                    {
                        spriteBatch.DrawString(CombatFont, Equipment.Instance.Skill2.SkillName, emptyButtonList[1].Position + new Vector2(50, 7), Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(CombatFont, Equipment.Instance.Skill2.SkillName, emptyButtonList[1].Position + new Vector2(50, 7), Color.Gray);
                        spriteBatch.DrawString(CombatFont, $"{Equipment.Instance.Skill2.InternalCooldown}", emptyButtonList[1].Position + new Vector2(-100, 7), Color.Gray);
                    }
                }

                if (Equipment.Instance.Skill3 != null)
                {
                    if (Equipment.Instance.Skill3.InternalCooldown == 0)
                    {
                        spriteBatch.DrawString(CombatFont, Equipment.Instance.Skill3.SkillName, emptyButtonList[2].Position + new Vector2(50, 7), Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(CombatFont, Equipment.Instance.Skill3.SkillName, emptyButtonList[2].Position + new Vector2(50, 7), Color.Gray);
                        spriteBatch.DrawString(CombatFont, $"{Equipment.Instance.Skill3.InternalCooldown}", emptyButtonList[2].Position + new Vector2(-100, 7), Color.Gray);
                    }
                }
                
                spriteBatch.DrawString(CombatFont, "Back", emptyButtonList[3].Position + new Vector2(50, 7), Color.White);
            }

            //Draws health, healthbars and turn bar for enemy
            if (Target != null)
            {
                spriteBatch.DrawString(combatFont, $"Level {Target.Level}", new Vector2(1350, 150), Color.White);
                spriteBatch.Draw(HealthEmpty, new Vector2(1200, 800), Color.White);
                spriteBatch.Draw(HealthFull, new Vector2(1202, 802), new Rectangle(0, 0, Convert.ToInt32(PercentStat(target.CurrentHealth, target.MaxHealth) * 5.9), 70), Color.White);
                spriteBatch.Draw(BlankFull, new Vector2(1202, 802), new Rectangle(0, 0, Convert.ToInt32(PercentStat(enemyShield, target.MaxHealth) * 5.9), 70), Color.DarkGray * 0.9f);
                spriteBatch.DrawString(CombatFont, $"{target.CurrentHealth} / {target.MaxHealth}", new Vector2(1260, 880), Color.White);
                spriteBatch.Draw(target.Sprite, new Vector2(1250, 250), null, Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.FlipHorizontally, 1);
                spriteBatch.Draw(HealthEmpty, new Vector2(1200, 700), Color.White);
                spriteBatch.Draw(turnFull, new Vector2(1202, 702), new Rectangle(0, 0, Convert.ToInt32(PercentStat((int)enemyAttackTimer, (int)turnTimer) * 5.9), 70), Color.White);
            }

            //Draws health, healthbars and turn bar for player
            Player.Instance.ChooseAnimationFrame();
            spriteBatch.Draw(HealthEmpty, new Vector2(100, 800), Color.White);
            spriteBatch.Draw(HealthFull, new Vector2(102, 802), new Rectangle(0, 0, Convert.ToInt32(PercentStat(Player.Instance.CurrentHealth, Player.Instance.MaxHealth) * 5.9), 70), Color.White);
            spriteBatch.Draw(BlankFull, new Vector2(102, 802), new Rectangle(0, 0, Convert.ToInt32(PercentStat(playerShield, Player.Instance.MaxHealth) * 5.9), 70), Color.DarkGray * 0.9f);
            spriteBatch.Draw(Player.Instance.Sprite, new Vector2(150, 200), null, Color.White, 0f, Vector2.Zero, 1.5f, new SpriteEffects(), 1);
            spriteBatch.Draw(HealthEmpty, new Vector2(100, 700), Color.White);
            spriteBatch.Draw(turnFull, new Vector2(102, 702), new Rectangle(0, 0, Convert.ToInt32(PercentStat((int)playerAttackTimer, (int)turnTimer) * 5.9), 70), Color.White);
            
            spriteBatch.DrawString(CombatFont, $"{Player.Instance.CurrentHealth} / {Player.Instance.MaxHealth}", new Vector2(160, 880), Color.White);
            
            for (int i = 0; i < playerText.Count; i++)
            {
                spriteBatch.DrawString(CombatFont, playerText[i].StringText, playerText[i].StringPosition, playerText[i].StringColor);
            }

            for (int i = 0; i < enemyText.Count; i++)
            {
                spriteBatch.DrawString(CombatFont, enemyText[i].StringText, enemyText[i].StringPosition, enemyText[i].StringColor);
            }

        }

        //Goes up and down on the button list
        public void ChangeSelected(int i)
        {
            if (SelectedInt >= 0 && SelectedInt <= 3 && combatDelay > 200)
            {
                SelectedInt += i;
                combatDelay = 0;
            }

            if (SelectedInt > 3)
            {
                SelectedInt = 3;
            }

            if (SelectedInt < 0)
            {
                SelectedInt = 0;
            }
        }
        
        /// <summary>
        /// Determines what happens when a button is clicked
        /// </summary>
        public void CombatEvent()
        {
            if (buttonType == "Normal")
            {
                switch (selectedInt)
                {
                    case 0: //attack
                        PlayerTurn();
                        CountCooldown();
                        break;
                    case 1: //skill
                        buttonType = "Skills";
                        break;
                    case 2: //Capture
                        if (target.Dragon == false)
                        {
                            CountCooldown();
                            int tempChance = PercentStat(target.CurrentHealth, target.MaxHealth);
                            int tempInt = GameWorld.Instance.RandomInt(0, 100);

                            if ((tempChance) * 2 < tempInt)
                            {
                                FilledStone.CatchMonster(Target);
                                Target.CurrentHealth = 0;
                            }

                            playerAttackTimer = 0;
                        }
                        break;

                    case 3: //flee
                        playerAttackTimer = 0;
                        if (GameWorld.Instance.RandomInt(0, 4) != 0)
                        {
                            ExitCombat();
                        }
                        break;
                }
            }
            else if (buttonType == "Skills")
            {
                switch (selectedInt)
                {
                    case 0:
                        if (Equipment.Instance.Skill1 != null && Equipment.Instance.Skill1.InternalCooldown == 0)
                        {
                            CountCooldown();
                            
                           
                            Effect tempEffect = new Effect(Equipment.Instance.Skill1.SkillEffect.Index, Equipment.Instance.Skill1.SkillEffect.Type, Equipment.Instance.Skill1.SkillEffect.Stone, Player.Instance, 0);
                            if (Equipment.Instance.Skill1.Monster == "sentry")
                            {
                                Log.Instance.ScanCreature(target);
                                Equipment.Instance.Skill1.InternalCooldown = 99;
                            }

                            else if (Equipment.Instance.Skill1.SkillEffect.TargetsSelf)
                            {
                                playerEffects.Add(new Effect(Equipment.Instance.Skill1.SkillEffect.Index, Equipment.Instance.Skill1.SkillEffect.Type, Equipment.Instance.Skill1.SkillEffect.Stone, Player.Instance, 0));
                                playerDamageReduction *= playerEffects[playerEffects.Count - 1].DamageReduction; //immediately applies damageReduction
                                playerShield += playerEffects[playerEffects.Count - 1].Shield; //immediately applies shield
                                if (playerEffects[playerEffects.Count - 1].Shield != 0) //adds shield to PlayerScrolling and removes a turn off of EffectLength
                                {
                                    PlayerScrolling($"Shield +{playerEffects[playerEffects.Count - 1].Shield}", Color.Gray);
                                    playerEffects[playerEffects.Count - 1].EffectLength--;
                                }
                            }
                            else if (!Equipment.Instance.Skill1.SkillEffect.TargetsSelf && !Equipment.Instance.Skill1.SkillEffect.TargetsBoth)
                            {
                                if (Equipment.Instance.Skill1.SkillEffect.EffectLength == 1 && Equipment.Instance.Skill1.SkillEffect.Damage > 0 && GameWorld.Instance.RandomInt(0, Equipment.Instance.Skill1.SkillEffect.UpperChanceBounds) == 0)
                                {
                                    target.CurrentHealth -= tempEffect.Damage;
                                }
                                else
                                {
                                    enemyEffects.Add(new Effect(Equipment.Instance.Skill1.SkillEffect.Index, Equipment.Instance.Skill1.SkillEffect.Type, Equipment.Instance.Skill1.SkillEffect.Stone, target, 0));
                                }                                
                            }
                            else if (Equipment.Instance.Skill1.SkillEffect.TargetsBoth)
                            {                                
                                if (tempEffect.UpperChanceBounds != 1) //ultimate bucket attack exception
                                {
                                    if (GameWorld.Instance.RandomInt(0, tempEffect.UpperChanceBounds) == 0)
                                    {
                                        target.CurrentHealth -= tempEffect.Damage - target.Defense;
                                    }
                                }
                                else //drain life exception
                                {
                                    target.CurrentHealth -= tempEffect.Damage; //damages target
                                    Player.Instance.CurrentHealth += tempEffect.Heal; //heals player
                                }
                            }
                            playerAttackTimer = 0;
                            Equipment.Instance.Skill1.InternalCooldown = Equipment.Instance.Skill1.SkillEffect.Cooldown;
                        }
                        break;
                    case 1:
                        if (Equipment.Instance.Skill2 != null && Equipment.Instance.Skill2.InternalCooldown == 0)
                        {
                            CountCooldown();

                            if (Equipment.Instance.Skill2.Monster == "sentry")
                            {
                                Log.Instance.ScanCreature(target);
                                Equipment.Instance.Skill2.InternalCooldown = 99;
                            }

                            Effect tempEffect = new Effect(Equipment.Instance.Skill2.SkillEffect.Index, Equipment.Instance.Skill2.SkillEffect.Type, Equipment.Instance.Skill2.SkillEffect.Stone, Player.Instance, 0);
                            if (Equipment.Instance.Skill2.SkillEffect.TargetsSelf)
                            {
                                playerEffects.Add(new Effect(Equipment.Instance.Skill2.SkillEffect.Index, Equipment.Instance.Skill2.SkillEffect.Type, Equipment.Instance.Skill2.SkillEffect.Stone, Player.Instance, 0));
                                playerDamageReduction *= playerEffects[playerEffects.Count - 1].DamageReduction; //immediately applies damageReduction
                                playerShield += playerEffects[playerEffects.Count - 1].Shield; //immediately applies shield
                                if (playerEffects[playerEffects.Count - 1].Shield != 0) //adds shield to PlayerScrolling and removes a turn off of EffectLength
                                {
                                    PlayerScrolling($"Shield +{playerEffects[playerEffects.Count - 1].Shield}", Color.Gray);
                                    playerEffects[playerEffects.Count - 1].EffectLength--;
                                }
                            }
                            else if (!Equipment.Instance.Skill2.SkillEffect.TargetsSelf && !Equipment.Instance.Skill1.SkillEffect.TargetsBoth)
                            {
                                if (Equipment.Instance.Skill2.SkillEffect.EffectLength == 1 && Equipment.Instance.Skill2.SkillEffect.Damage > 0 && GameWorld.Instance.RandomInt(0, Equipment.Instance.Skill2.SkillEffect.UpperChanceBounds) == 0)
                                {                                    
                                    target.CurrentHealth -= tempEffect.Damage;
                                }
                                else
                                {
                                    enemyEffects.Add(new Effect(Equipment.Instance.Skill2.SkillEffect.Index, Equipment.Instance.Skill2.SkillEffect.Type, Equipment.Instance.Skill2.SkillEffect.Stone, target, 0));
                                }
                            }
                            else if (Equipment.Instance.Skill2.SkillEffect.TargetsBoth)
                            {
                                if (tempEffect.UpperChanceBounds != 1) //ultimate bucket attack exception
                                {
                                    if (GameWorld.Instance.RandomInt(0, tempEffect.UpperChanceBounds) == 0)
                                    {
                                        target.CurrentHealth -= tempEffect.Damage - target.Defense;
                                    }
                                }
                                else //drain life exception
                                {
                                    target.CurrentHealth -= tempEffect.Damage; //damages target
                                    Player.Instance.CurrentHealth += tempEffect.Heal; //heals player
                                }
                            }
                            playerAttackTimer = 0;
                            Equipment.Instance.Skill2.InternalCooldown = Equipment.Instance.Skill2.SkillEffect.Cooldown;
                        }
                        break;
                    case 2:
                        if (Equipment.Instance.Skill3 != null && Equipment.Instance.Skill3.InternalCooldown == 0)
                        {
                            CountCooldown();
                            if (Equipment.Instance.Skill3.Monster == "sentry")
                            {
                                Log.Instance.ScanCreature(target);
                                Equipment.Instance.Skill3.InternalCooldown = 99;
                            }

                            Effect tempEffect = new Effect(Equipment.Instance.Skill3.SkillEffect.Index, Equipment.Instance.Skill3.SkillEffect.Type, Equipment.Instance.Skill3.SkillEffect.Stone, Player.Instance, 0);
                            if (Equipment.Instance.Skill3.SkillEffect.TargetsSelf)
                            {
                                playerEffects.Add(new Effect(Equipment.Instance.Skill3.SkillEffect.Index, Equipment.Instance.Skill3.SkillEffect.Type, Equipment.Instance.Skill3.SkillEffect.Stone, Player.Instance, 0));
                                playerDamageReduction *= playerEffects[playerEffects.Count - 1].DamageReduction; //immediately applies damageReduction
                                playerShield += playerEffects[playerEffects.Count - 1].Shield; //immediately applies shield
                                if (playerEffects[playerEffects.Count - 1].Shield != 0) //adds shield to PlayerScrolling and removes a turn off of EffectLength
                                {
                                    PlayerScrolling($"Shield +{playerEffects[playerEffects.Count - 1].Shield}", Color.Gray);
                                    playerEffects[playerEffects.Count - 1].EffectLength--;
                                }
                            }
                            else if (!Equipment.Instance.Skill3.SkillEffect.TargetsSelf && !Equipment.Instance.Skill1.SkillEffect.TargetsBoth)
                            {
                                if (Equipment.Instance.Skill3.SkillEffect.EffectLength == 1 && Equipment.Instance.Skill3.SkillEffect.Damage > 0 && GameWorld.Instance.RandomInt(0, Equipment.Instance.Skill3.SkillEffect.UpperChanceBounds) == 0)
                                {
                                    target.CurrentHealth -= tempEffect.Damage;
                                }
                                else
                                {
                                    enemyEffects.Add(new Effect(Equipment.Instance.Skill3.SkillEffect.Index, Equipment.Instance.Skill3.SkillEffect.Type, Equipment.Instance.Skill3.SkillEffect.Stone, target, 0));
                                }
                            }
                            else if (Equipment.Instance.Skill3.SkillEffect.TargetsBoth)
                            {
                                if (tempEffect.UpperChanceBounds != 1) //ultimate bucket attack exception
                                {
                                    if (GameWorld.Instance.RandomInt(0, tempEffect.UpperChanceBounds) == 0)
                                    {
                                        target.CurrentHealth -= tempEffect.Damage - target.Defense;
                                    }
                                }
                                else //drain life exception
                                {
                                    target.CurrentHealth -= tempEffect.Damage; //damages target
                                    Player.Instance.CurrentHealth += tempEffect.Heal; //heals player
                                }
                            }
                            playerAttackTimer = 0;
                            Equipment.Instance.Skill3.InternalCooldown = Equipment.Instance.Skill3.SkillEffect.Cooldown;
                        }
                        break;
                    case 3:
                        buttonType = "Normal";
                        break;
                }
                buttonType = "Normal";
            }
        }
        
        /// <summary>
        /// Used to set a target on the enemey for effects
        /// </summary>
        /// <param name="combatEnemy"></param>
        public void SelectEnemy(Enemy combatEnemy)
        {
            Target = combatEnemy;

            //Alternate take on turnTimer
            if (Target.AttackSpeed > Player.Instance.AttackSpeed)
            {
                turnTimer = Target.AttackSpeed * 100.5f;
            }
            else
            {
                turnTimer = Player.Instance.AttackSpeed * 100.5f;
            }
        }
        
        /// <summary>
        /// General code to give the percentage value of two numbers, going from 1 to 100
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public int PercentStat(int currentValue, int maxValue)
        {
            float floatCurrentValue = currentValue;
            float floatMaxValue = maxValue;
            int Value = Convert.ToInt32(Math.Round(floatCurrentValue * 100 / floatMaxValue));

            if (Value > 100)
            {
                Value = 100;
            }
            return Value;
        }

        /// <summary>
        /// Controls player's attack
        /// </summary>
        public void PlayerTurn()
        {
            buttonColor = Color.Gray;
            if (Target != null)
            {
                //local values to apply effects
                bool stunned = new bool();
                bool confused = new bool();
                float accuracyMod = 1f;
                float damageMod = 1f;
                playerSpeedMod = 1f;
                playerDamageReduction = 1;
                playerDamageAbs = 0;

                if (Equipment.Instance.Weapon != null && Equipment.Instance.Weapon.WeaponEffect.DoubleAttack && GameWorld.Instance.RandomInt(0, Equipment.Instance.Weapon.WeaponEffect.UpperChanceBounds) == 0) //checks for double attack 
                {
                    playerAttackAmount++;
                }

                //goes through all active effects on the enemy with an EffectLength greater than 0 and applies their effect
                foreach (Effect effect in playerEffects)
                {
                    if (effect.EffectLength > 0)
                    {
                        Player.Instance.CurrentHealth -= effect.Damage; //applies damage
                        playerShield += effect.Shield;
                        if (effect.Shield != 0)
                        {
                            PlayerScrolling($"Shield +{effect.Shield}", Color.Gray);
                        }
                        playerDamageAbs += effect.DamageAbs;
                        confused = effect.Confuse; //applies confuse
                        stunned = effect.Stun; //applies stun
                        if (effect.AccuracyMod != 1f) //effects has a base AccuracyMod of 1
                        {
                            accuracyMod *= effect.AccuracyMod;
                        }
                        if (effect.DamageMod != 1f) //effects has a base DamageMod of 1
                        {
                            damageMod *= effect.DamageMod;
                        }
                        if (effect.SpeedMod != 1f) //effects has a base SpeedMod of 1
                        {
                            playerSpeedMod *= effect.SpeedMod;
                        }
                        if (effect.DamageReduction != 1) //effects has a base DamageReduction of 1
                        {
                            playerDamageReduction *= effect.DamageReduction;
                        }
                        if (effect.DoubleAttack) //checks for double attack
                        {
                            playerAttackAmount++;
                        }
                    }
                    effect.EffectLength--; //decreases how many rounds the effect is still in effect

                    //adds any effects with an effectLength of 0 or less to a list so they can be removed
                    if (effect.EffectLength <= 0)
                    {
                        toBeRemovedEffects.Add(effect);
                    }
                }

                //removes any effects in the toBeRemovedEffects list
                foreach (Effect effect in toBeRemovedEffects)
                {
                    enemyEffects.Remove(effect);
                }
                toBeRemovedEffects.Clear();

                if (Equipment.Instance.EquippedEquipment[1] != null && Equipment.Instance.EquippedEquipment[1].ArmorEffect.StunImmunity) //checks if the player is immune to stuns
                {
                    stunned = false;
                }

                playerAttackTimer = 0; //resets attack timer

                if (!stunned)
                {
                    Player.Instance.AttackStart = true; //starts attack animation

                    List<int> damageToDeal = new List<int>();
                    int totalDamageToDeal = 0;

                    for (int j = 0; j < playerAttackAmount; j++) //foreach time the player should attack
                    {
                        if (Player.Instance.Damage - Target.Defense > 0) //if the base damage the player should deal, after defense reduction, is greater than 0
                        {
                            damageToDeal.Add((int)Math.Round(((Player.Instance.Damage * damageMod) - target.Defense) * enemyDamageReduction - (enemyDamageAbs * 0.2))); //adds base damage to the damageToDeal list
                        }

                        //adds damage foreach damage type to the list damageToDeal
                        for (int i = 0; i < Player.Instance.DamageTypes.Count; i++)
                        {
                            damageToDeal.Add((int)(((Player.Instance.DamageTypes[i] * damageMod) - (Player.Instance.DamageTypes[i] * damageMod * 0.01 * target.ResistanceTypes[i])) * enemyDamageReduction - (enemyDamageAbs * 0.8)));
                        }

                        //adds all damage together into one variable
                        for (int i = 0; i < damageToDeal.Count; i++)
                        {
                            if (damageToDeal[i] < 0)
                            {
                                damageToDeal[i] = 0;
                            }
                            totalDamageToDeal += damageToDeal[i];
                        }

                        //adds damage bonus from log
                        if (target.Dragon)
                        {
                            switch (target.DragonStone.Element)
                            {
                                case "neutral":
                                    totalDamageToDeal = (int)Math.Round(totalDamageToDeal * (1 + Log.Instance.NeutralBonus));
                                    break;
                                case "earth":
                                    totalDamageToDeal = (int)Math.Round(totalDamageToDeal * (1 + Log.Instance.EarthBonus));
                                    break;
                                case "water":
                                    totalDamageToDeal = (int)Math.Round(totalDamageToDeal * (1 + Log.Instance.WaterBonus));
                                    break;
                                case "metal":
                                    totalDamageToDeal = (int)Math.Round(totalDamageToDeal * (1 + Log.Instance.MetalBonus));
                                    break;
                                case "air":
                                    totalDamageToDeal = (int)Math.Round(totalDamageToDeal * (1 + Log.Instance.AirBonus));
                                    break;
                                case "dark":
                                    totalDamageToDeal = (int)Math.Round(totalDamageToDeal * (1 + Log.Instance.DarkBonus));
                                    break;
                                case "fire":
                                    totalDamageToDeal = (int)Math.Round(totalDamageToDeal * (1 + Log.Instance.FireBonus));
                                    break;
                            }
                        }
                        else
                        {
                            switch (target.EnemyStone.Element)
                            {
                                case "neutral":
                                    totalDamageToDeal = (int)Math.Round(totalDamageToDeal * (1 + Log.Instance.NeutralBonus));
                                    break;
                                case "earth":
                                    totalDamageToDeal = (int)Math.Round(totalDamageToDeal * (1 + Log.Instance.EarthBonus));
                                    break;
                                case "water":
                                    totalDamageToDeal = (int)Math.Round(totalDamageToDeal * (1 + Log.Instance.WaterBonus));
                                    break;
                                case "metal":
                                    totalDamageToDeal = (int)Math.Round(totalDamageToDeal * (1 + Log.Instance.MetalBonus));
                                    break;
                                case "air":
                                    totalDamageToDeal = (int)Math.Round(totalDamageToDeal * (1 + Log.Instance.AirBonus));
                                    break;
                                case "dark":
                                    totalDamageToDeal = (int)Math.Round(totalDamageToDeal * (1 + Log.Instance.DarkBonus));
                                    break;
                                case "fire":
                                    totalDamageToDeal = (int)Math.Round(totalDamageToDeal * (1 + Log.Instance.FireBonus));
                                    break;
                            }
                        }
                        
                        //shield
                        if (enemyShield - totalDamageToDeal <= 0)
                        {
                            totalDamageToDeal -= enemyShield;
                            enemyShield = 0;
                        }
                        else
                        {
                            enemyShield -= totalDamageToDeal;
                            totalDamageToDeal = 0;
                        }

                        if (confused && GameWorld.Instance.RandomInt(0, 100) < 50) //if the player is confused, has a chance to damage themselves
                        {
                            Player.Instance.CurrentHealth -= (int)(totalDamageToDeal * 0.5);
                        }
                        else if (confused && GameWorld.Instance.RandomInt(0, 50) < 25) //if the enemy is confused, has a chance to miss
                        {
                            //does nothing
                        }
                        else if (GameWorld.Instance.RandomInt((int)(100 * accuracyMod), (int)(200 - (100 - (100 * accuracyMod)))) >= 100) //calculates hit chance
                        {
                            target.CurrentHealth -= totalDamageToDeal;

                            if (target.Dragon) //dragons
                            {
                                foreach (Effect itemEffect in target.DragonStone.DragonArmorEffects)
                                {
                                    //armor effects of target
                                    if (itemEffect.TargetsSelf && GameWorld.Instance.RandomInt(0, itemEffect.UpperChanceBounds) == 0) //has a chance to add positive effects to the enemy
                                    {
                                        enemyEffects.Add(new Effect(itemEffect.Index, itemEffect.Type, target.DragonStone, target, totalDamageToDeal));
                                    }
                                    else if ((itemEffect.Shield != 0 || itemEffect.DamageReduction != 1) && GameWorld.Instance.RandomInt(0, itemEffect.UpperChanceBounds) == 0)
                                    {
                                        Effect tempEffect = new Effect(itemEffect.Index, itemEffect.Type, target.DragonStone, target, totalDamageToDeal);
                                        enemyShield += tempEffect.Shield;
                                        if (tempEffect.Shield != 0)
                                        {
                                            EnemyScrolling($"Shield +{tempEffect.Shield}", Color.Gray);
                                        }
                                        enemyDamageReduction *= tempEffect.DamageReduction;
                                    }
                                    else if (!itemEffect.TargetsSelf && GameWorld.Instance.RandomInt(0, itemEffect.UpperChanceBounds) == 0 && !itemEffect.StatBuff) //has a chance to add negative effects to the player
                                    {
                                        if (itemEffect.Retaliate)
                                        {
                                            Effect tempEffect = new Effect(itemEffect.Index, itemEffect.Type, target.DragonStone, target, totalDamageToDeal);
                                            Player.Instance.CurrentHealth -= tempEffect.Damage;
                                        }
                                        else
                                        {
                                            playerEffects.Add(new Effect(itemEffect.Index, itemEffect.Type, target.DragonStone, target, totalDamageToDeal));
                                        }
                                    }
                                    else if (itemEffect.TargetsBoth && GameWorld.Instance.RandomInt(0, itemEffect.UpperChanceBounds) == 0) //has a chance to damage player and heal target
                                    {
                                        Effect tempEffect = new Effect(itemEffect.Index, itemEffect.Type, target.DragonStone, target, 0);
                                        Player.Instance.CurrentHealth -= tempEffect.Damage; //damages player
                                        target.CurrentHealth += tempEffect.Heal; //heals target
                                    }
                                }
                            }
                            else //normal enemies
                            {
                                //armor effects of target
                                if (target.EnemyStone.ArmorEffect.TargetsSelf && GameWorld.Instance.RandomInt(0, target.EnemyStone.ArmorEffect.UpperChanceBounds) == 0) //has a chance to add positive effects to the enemy
                                {
                                    enemyEffects.Add(new Effect(target.EnemyStone.ArmorEffect.Index, target.EnemyStone.ArmorEffect.Type, target.EnemyStone, target, totalDamageToDeal));
                                }
                                else if ((target.EnemyStone.ArmorEffect.Shield != 0 || target.EnemyStone.ArmorEffect.DamageReduction != 1) && GameWorld.Instance.RandomInt(0, target.EnemyStone.ArmorEffect.UpperChanceBounds) == 0)
                                {
                                    Effect tempEffect = new Effect(target.EnemyStone.ArmorEffect.Index, target.EnemyStone.ArmorEffect.Type, target.EnemyStone, target, totalDamageToDeal);
                                    enemyShield += tempEffect.Shield;
                                    if (tempEffect.Shield != 0)
                                    {
                                        EnemyScrolling($"Shield +{tempEffect.Shield}", Color.Gray);
                                    }
                                    enemyDamageReduction *= tempEffect.DamageReduction;
                                }
                                else if (!target.EnemyStone.ArmorEffect.TargetsSelf && GameWorld.Instance.RandomInt(0, target.EnemyStone.ArmorEffect.UpperChanceBounds) == 0 && !target.EnemyStone.ArmorEffect.StatBuff) //has a chance to add negative effects to the player
                                {
                                    if (target.EnemyStone.ArmorEffect.Retaliate)
                                    {
                                        Effect tempEffect = new Effect(target.EnemyStone.ArmorEffect.Index, target.EnemyStone.ArmorEffect.Type, target.EnemyStone, target, totalDamageToDeal);
                                        Player.Instance.CurrentHealth -= tempEffect.Damage;
                                    }
                                    else
                                    {
                                        playerEffects.Add(new Effect(target.EnemyStone.ArmorEffect.Index, target.EnemyStone.ArmorEffect.Type, target.EnemyStone, target, totalDamageToDeal));
                                    }
                                }
                                else if (target.EnemyStone.ArmorEffect.TargetsBoth && GameWorld.Instance.RandomInt(0, target.EnemyStone.ArmorEffect.UpperChanceBounds) == 0) //has a chance to damage player and heal target
                                {
                                    Effect tempEffect = new Effect(target.EnemyStone.ArmorEffect.Index, target.EnemyStone.ArmorEffect.Type, target.EnemyStone, target, 0);
                                    Player.Instance.CurrentHealth -= tempEffect.Damage; //damages player
                                    target.CurrentHealth += tempEffect.Heal; //heals target
                                }
                            }
                            
                            //rolls chance for player weapon soul stone
                            if (Equipment.Instance.Weapon != null)
                            {
                                if (!Equipment.Instance.Weapon.WeaponEffect.TargetsSelf && GameWorld.Instance.RandomInt(0, Equipment.Instance.Weapon.WeaponEffect.UpperChanceBounds) == 0 && !Equipment.Instance.Weapon.WeaponEffect.StatBuff) //has a chance to add negative effects to the enemy
                                {
                                    enemyEffects.Add(new Effect(Equipment.Instance.Weapon.WeaponEffect.Index, Equipment.Instance.Weapon.WeaponEffect.Type, Equipment.Instance.Weapon, Player.Instance, totalDamageToDeal));
                                }
                                else if (Equipment.Instance.Weapon.WeaponEffect.TargetsSelf && GameWorld.Instance.RandomInt(0, Equipment.Instance.Weapon.WeaponEffect.UpperChanceBounds) == 0 && !Equipment.Instance.Weapon.WeaponEffect.StatBuff) //has a chance to add positive effects to the player
                                {
                                    playerEffects.Add(new Effect(Equipment.Instance.Weapon.WeaponEffect.Index, Equipment.Instance.Weapon.WeaponEffect.Type, Equipment.Instance.Weapon, Player.Instance, totalDamageToDeal));
                                }
                            }

                            //applies healing
                            foreach (Effect effect in playerEffects)
                            {
                                if (effect.EffectLength > 0 && effect.Heal > 0)
                                {
                                    Player.Instance.CurrentHealth += effect.Heal;
                                    effect.EffectLength--;
                                    if (effect.Heal <= 0)
                                    {
                                        PlayerScrolling("HP +0", Color.Green);
                                    }
                                }
                            }
                        }
                        else
                        {
                            PlayerScrolling("Miss", Color.White);
                        }
                    }
                    if (totalDamageToDeal <= 0)
                    {
                        EnemyScrolling("HP -0", Color.Red);
                    }
                }
                playerAttackAmount = 1; //resets how many times the player attacks
                combatDelay = 0; //resets combat delay
            }
        }

        /// <summary>
        /// Controls enemy's turn logic
        /// </summary>
        public void EnemyTurn()
        {
            //local values to apply effects
            bool stunned = new bool();
            bool confused = new bool();
            float accuracyMod = 1f;
            float damageMod = 1f;
            enemySpeedMod = 1f;
            enemyDamageReduction = 1;
            enemyDamageAbs = 0;

            Sound.PlaySound($"sound/monsterSounds/{target.Monster}");

            if (target.EnemyStone.WeaponEffect.DoubleAttack && GameWorld.Instance.RandomInt(0, target.EnemyStone.WeaponEffect.UpperChanceBounds) == 0) //checks for double attack 
            {
                enemyAttackAmount++;
            }

            //goes through all active effects on the enemy with an EffectLength greater than 0
            foreach (Effect effect in enemyEffects)
            {
                if (effect.EffectLength > 0)
                {
                    target.CurrentHealth -= effect.Damage; //applies damage
                    enemyShield += effect.Shield;
                    if (effect.Shield != 0)
                    {
                        EnemyScrolling($"Shield +{effect.Shield}", Color.Gray);
                    }
                    enemyDamageAbs += effect.DamageAbs;
                    confused = effect.Confuse; //applies confuse
                    stunned = effect.Stun; //applies stun
                    if (effect.AccuracyMod != 1f) //effects has a base AccuracyMod of 1
                    {
                        accuracyMod *= effect.AccuracyMod;
                    }
                    if (effect.DamageMod != 1f) //effects has a base DamageMod of 1
                    {
                        damageMod *= effect.DamageMod;
                    }
                    if (effect.SpeedMod != 1f) //effects has a base SpeedMod of 1
                    {
                        enemySpeedMod *= effect.SpeedMod;
                    }
                    if (effect.DamageReduction != 1) //effects has a base DamageReduction of 1
                    {
                        enemyDamageReduction *= effect.DamageReduction;
                    }
                    if (effect.DoubleAttack) //checks for double attack
                    {
                        enemyAttackAmount++;
                    }
                }
                effect.EffectLength--;
                
                //adds any effects with an effectLength of 0 or less to a list so they can be removed
                if (effect.EffectLength <= 0)
                {
                    toBeRemovedEffects.Add(effect);
                }
            }

            //removes any effects in the toBeRemovedEffects list
            foreach (Effect effect in toBeRemovedEffects)
            {
                enemyEffects.Remove(effect);
            }

            toBeRemovedEffects.Clear();

            if (target.EnemyStone.ArmorEffect.StunImmunity) //checks if the enemy is immune to stuns
            {
                stunned = false;
            }

            enemyAttackTimer = 0;
            if (!stunned)
            {
                List<int> damageToDeal = new List<int>();
                int totalDamageToDeal = 0;

                for (int j = 0; j < enemyAttackAmount; j++) //foreach time the enemy should attack
                {
                    //adds the damage to be dealt
                    if (Target.Damage - Player.Instance.Defense > 0)
                    {
                        damageToDeal.Add((int)Math.Round(((target.Damage * damageMod) - Player.Instance.Defense) * playerDamageReduction - (playerDamageAbs * 0.2)));
                    }

                    //goes through all damage types and resistance types and calculates damage to be dealt
                    for (int i = 0; i < Target.DamageTypes.Count; i++)
                    {
                        damageToDeal.Add((int)(((target.DamageTypes[i] * damageMod) - (target.DamageTypes[i] * damageMod * 0.01 * Player.Instance.ResistanceTypes[i])) * playerDamageReduction - (playerDamageAbs * 0.8)));
                    }

                    //adds all damage to a single variable
                    for (int i = 0; i < damageToDeal.Count; i++)
                    {
                        if (damageToDeal[i] < 0)
                        {
                            damageToDeal[i] = 0;
                        }
                        totalDamageToDeal += damageToDeal[i];
                    }

                    //shield
                    if (playerShield - totalDamageToDeal <= 0)
                    {
                        totalDamageToDeal -= playerShield;
                        playerShield = 0;
                    }
                    else
                    {
                        playerShield -= totalDamageToDeal;
                        totalDamageToDeal = 0;
                    }

                    if (confused && GameWorld.Instance.RandomInt(0, 100) < 50) //if the enemy is confused, has a chance to damage themselves
                    {
                        Target.CurrentHealth -= (int)(totalDamageToDeal * 0.5);
                    }
                    else if (confused && GameWorld.Instance.RandomInt(0, 50) < 25) //if the enemy is confused, has a chance to miss
                    {
                        //does nothing
                    }
                    else if (GameWorld.Instance.RandomInt((int)(100 * accuracyMod), (int)(200 - (100 - (100 * accuracyMod)))) >= 100) //calculates hit chance
                    {
                        Player.Instance.CurrentHealth -= totalDamageToDeal;

                        //armor effects of player
                        if (Equipment.Instance.Armor != null && Equipment.Instance.Armor.ArmorEffect.TargetsSelf && GameWorld.Instance.RandomInt(0, Equipment.Instance.Armor.ArmorEffect.UpperChanceBounds) == 0 && Equipment.Instance.Armor.ArmorEffect.Shield == 0) //has a chance to add positive effects to the enemy
                        {
                            playerEffects.Add(new Effect(Equipment.Instance.Armor.ArmorEffect.Index, Equipment.Instance.Armor.ArmorEffect.Type, Equipment.Instance.Armor, Player.Instance, totalDamageToDeal));
                        }
                        else if (Equipment.Instance.Armor != null && Equipment.Instance.Armor.ArmorEffect.Shield != 0 && GameWorld.Instance.RandomInt(0, Equipment.Instance.Armor.ArmorEffect.UpperChanceBounds) == 0) //has a chance to immediately apply shield from armor
                        {
                            Effect tempEffect = new Effect(Equipment.Instance.Armor.ArmorEffect.Index, Equipment.Instance.Armor.ArmorEffect.Type, Equipment.Instance.Armor, Player.Instance, totalDamageToDeal);
                            playerShield += tempEffect.Shield;
                            PlayerScrolling($"Shield +{tempEffect.Shield}", Color.Gray);
                            
                        }
                        else if (Equipment.Instance.Armor != null && !Equipment.Instance.Armor.ArmorEffect.TargetsSelf && GameWorld.Instance.RandomInt(0, Equipment.Instance.Armor.ArmorEffect.UpperChanceBounds) == 0 && !Equipment.Instance.Armor.ArmorEffect.StatBuff) //has a chance to add negative effects to the player
                        {
                            if (Equipment.Instance.Armor.ArmorEffect.Retaliate)
                            {
                                Effect tempEffect = new Effect(Equipment.Instance.Armor.ArmorEffect.Index, Equipment.Instance.Armor.ArmorEffect.Type, Equipment.Instance.Armor, Player.Instance, totalDamageToDeal);
                                target.CurrentHealth -= tempEffect.Damage;
                            }
                            else
                            {
                                enemyEffects.Add(new Effect(Equipment.Instance.Armor.ArmorEffect.Index, Equipment.Instance.Armor.ArmorEffect.Type, Equipment.Instance.Armor, Player.Instance, totalDamageToDeal));
                            }
                        }
                        else if (Equipment.Instance.Armor != null && Equipment.Instance.Armor.ArmorEffect.TargetsBoth && GameWorld.Instance.RandomInt(0, Equipment.Instance.Armor.ArmorEffect.UpperChanceBounds) == 0) //has a chance to damage player and heal target
                        {
                            Effect tempEffect = new Effect(Equipment.Instance.Armor.ArmorEffect.Index, Equipment.Instance.Armor.ArmorEffect.Type, Equipment.Instance.Armor, Player.Instance, 0);
                            target.CurrentHealth -= tempEffect.Damage; //damages enemy
                            Player.Instance.CurrentHealth += tempEffect.Heal; //heals player
                        }
                        
                        //rolls chance for target's weapon soul stone to apply effects
                        if (!target.EnemyStone.WeaponEffect.TargetsSelf && GameWorld.Instance.RandomInt(0, target.EnemyStone.WeaponEffect.UpperChanceBounds) == 0 && !target.EnemyStone.WeaponEffect.StatBuff) //has a chance to add negative effects to the player
                        {
                            playerEffects.Add(new Effect(target.EnemyStone.WeaponEffect.Index, target.EnemyStone.WeaponEffect.Type, target.EnemyStone, target, totalDamageToDeal));
                        }
                        else if (target.EnemyStone.WeaponEffect.TargetsSelf && GameWorld.Instance.RandomInt(0, target.EnemyStone.WeaponEffect.UpperChanceBounds) == 0 && !target.EnemyStone.WeaponEffect.StatBuff) //has a chance to add positive effects to the enemy
                        {
                            enemyEffects.Add(new Effect(target.EnemyStone.WeaponEffect.Index, target.EnemyStone.WeaponEffect.Type, target.EnemyStone, target, totalDamageToDeal));
                        }

                        //applies healing
                        foreach (Effect effect in enemyEffects)
                        {
                            if (effect.EffectLength > 0 && effect.Heal > 0)
                            {
                                target.CurrentHealth += effect.Heal;
                                effect.EffectLength--;
                                if (effect.Heal <= 0)
                                {
                                    EnemyScrolling("HP +0", Color.Green);
                                }
                            }
                        }
                    }
                    else
                    {
                        EnemyScrolling("Miss", Color.White);
                    }
                }
                if (totalDamageToDeal <= 0)
                {
                    PlayerScrolling($"HP -0", Color.Red);
                }

                enemyAttackAmount = 1;
            }
        }

        /// <summary>
        /// Controls enemy's combat logic if they are a dragon
        /// </summary>
        private void DragonTurn()
        {
            //local values to apply effects
            bool stunned = new bool();
            bool confused = new bool();
            float accuracyMod = 1f;
            float damageMod = 1f;
            enemySpeedMod = 1f;
            enemyDamageReduction = 1;
            enemyDamageAbs = 0;
            Sound.PlaySound($"sound/monsterSounds/dragon");

            foreach (Effect itemEffect in target.DragonStone.DragonWeaponEffects)
            {
                if (itemEffect.DoubleAttack && GameWorld.Instance.RandomInt(0, itemEffect.UpperChanceBounds) == 0) //checks for double attack 
                {
                    enemyAttackAmount++;
                }
            }

            //goes through all active effects on the enemy with an EffectLength greater than 0
            foreach (Effect effect in enemyEffects)
            {
                if (effect.EffectLength > 0)
                {
                    target.CurrentHealth -= effect.Damage; //applies damage
                    enemyShield += effect.Shield;
                    if (effect.Shield != 0)
                    {
                        EnemyScrolling($"Shield +{effect.Shield}", Color.Gray);
                    }
                    enemyDamageAbs += effect.DamageAbs;
                    confused = effect.Confuse; //applies confuse
                    stunned = effect.Stun; //applies stun
                    if (effect.AccuracyMod != 1f) //effects has a base AccuracyMod of 1
                    {
                        accuracyMod *= effect.AccuracyMod;
                    }
                    if (effect.DamageMod != 1f) //effects has a base DamageMod of 1
                    {
                        damageMod *= effect.DamageMod;
                    }
                    if (effect.SpeedMod != 1f) //effects has a base SpeedMod of 1
                    {
                        enemySpeedMod *= effect.SpeedMod;
                    }
                    if (effect.DamageReduction != 1) //effects has a base DamageReduction of 1
                    {
                        enemyDamageReduction *= effect.DamageReduction;
                    }
                    if (effect.DoubleAttack) //checks for double attack
                    {
                        enemyAttackAmount++;
                    }
                }
                effect.EffectLength--;

                //adds any effects with an effectLength of 0 or less to a list so they can be removed
                if (effect.EffectLength <= 0)
                {
                    toBeRemovedEffects.Add(effect);
                }
            }

            //removes any effects in the toBeRemovedEffects list
            foreach (Effect effect in toBeRemovedEffects)
            {
                enemyEffects.Remove(effect);
            }

            toBeRemovedEffects.Clear();

            foreach (Effect itemEffect in target.DragonStone.DragonWeaponEffects)
            {
                if (itemEffect.StunImmunity) //checks if the enemy is immune to stuns
                {
                    stunned = false;
                }
            }
                
            enemyAttackTimer = 0;
            if (!stunned)
            {
                List<int> damageToDeal = new List<int>();
                int totalDamageToDeal = 0;

                for (int j = 0; j < enemyAttackAmount; j++) //foreach time the enemy should attack
                {
                    //adds the damage to be dealt
                    if (Target.Damage - Player.Instance.Defense > 0)
                    {
                        damageToDeal.Add((int)Math.Round(((target.Damage * damageMod) - Player.Instance.Defense) * playerDamageReduction - (playerDamageAbs * 0.2)));
                    }

                    //goes through all damage types and resistance types and calculates damage to be dealt
                    for (int i = 0; i < Target.DamageTypes.Count; i++)
                    {
                        damageToDeal.Add((int)(((target.DamageTypes[i] * damageMod) - (target.DamageTypes[i] * damageMod * 0.01 * Player.Instance.ResistanceTypes[i])) * playerDamageReduction - (playerDamageAbs * 0.8)));
                    }

                    //adds all damage to a single variable
                    for (int i = 0; i < damageToDeal.Count; i++)
                    {
                        if (damageToDeal[i] < 0)
                        {
                            damageToDeal[i] = 0;
                        }
                        totalDamageToDeal += damageToDeal[i];
                    }

                    //shield
                    if (playerShield - totalDamageToDeal <= 0)
                    {
                        totalDamageToDeal -= playerShield;
                        playerShield = 0;
                    }
                    else
                    {
                        playerShield -= totalDamageToDeal;
                        totalDamageToDeal = 0;
                    }

                    if (confused && GameWorld.Instance.RandomInt(0, 100) < 50) //if the enemy is confused, has a chance to damage themselves
                    {
                        Target.CurrentHealth -= (int)(totalDamageToDeal * 0.5);
                    }
                    else if (confused && GameWorld.Instance.RandomInt(0, 50) < 25) //if the enemy is confused, has a chance to miss
                    {
                        //does nothing
                    }
                    else if (GameWorld.Instance.RandomInt((int)(100 * accuracyMod), (int)(200 - (100 - (100 * accuracyMod)))) >= 100) //calculates hit chance
                    {
                        Player.Instance.CurrentHealth -= totalDamageToDeal;

                        //armor effects of player
                        if (Equipment.Instance.Armor != null && Equipment.Instance.Armor.ArmorEffect.TargetsSelf && GameWorld.Instance.RandomInt(0, Equipment.Instance.Armor.ArmorEffect.UpperChanceBounds) == 0 && Equipment.Instance.Armor.ArmorEffect.Shield == 0) //has a chance to add positive effects to the enemy
                        {
                            playerEffects.Add(new Effect(Equipment.Instance.Armor.ArmorEffect.Index, Equipment.Instance.Armor.ArmorEffect.Type, Equipment.Instance.Armor, Player.Instance, totalDamageToDeal));
                        }
                        else if (Equipment.Instance.Armor != null && Equipment.Instance.Armor.ArmorEffect.Shield != 0 && GameWorld.Instance.RandomInt(0, Equipment.Instance.Armor.ArmorEffect.UpperChanceBounds) == 0) //has a chance to immediately apply shield from armor
                        {
                            Effect tempEffect = new Effect(Equipment.Instance.Armor.ArmorEffect.Index, Equipment.Instance.Armor.ArmorEffect.Type, Equipment.Instance.Armor, Player.Instance, totalDamageToDeal);
                            playerShield += tempEffect.Shield;
                            PlayerScrolling($"Shield +{tempEffect.Shield}", Color.Gray);

                        }
                        else if (Equipment.Instance.Armor != null && !Equipment.Instance.Armor.ArmorEffect.TargetsSelf && GameWorld.Instance.RandomInt(0, Equipment.Instance.Armor.ArmorEffect.UpperChanceBounds) == 0 && !Equipment.Instance.Armor.ArmorEffect.StatBuff) //has a chance to add negative effects to the player
                        {
                            if (Equipment.Instance.Armor.ArmorEffect.Retaliate)
                            {
                                Effect tempEffect = new Effect(Equipment.Instance.Armor.ArmorEffect.Index, Equipment.Instance.Armor.ArmorEffect.Type, Equipment.Instance.Armor, Player.Instance, totalDamageToDeal);
                                target.CurrentHealth -= tempEffect.Damage;
                            }
                            else
                            {
                                enemyEffects.Add(new Effect(Equipment.Instance.Armor.ArmorEffect.Index, Equipment.Instance.Armor.ArmorEffect.Type, Equipment.Instance.Armor, Player.Instance, totalDamageToDeal));
                            }
                        }
                        else if (Equipment.Instance.Armor != null && Equipment.Instance.Armor.ArmorEffect.TargetsBoth && GameWorld.Instance.RandomInt(0, Equipment.Instance.Armor.ArmorEffect.UpperChanceBounds) == 0) //has a chance to damage player and heal target
                        {
                            Effect tempEffect = new Effect(Equipment.Instance.Armor.ArmorEffect.Index, Equipment.Instance.Armor.ArmorEffect.Type, Equipment.Instance.Armor, Player.Instance, 0);
                            target.CurrentHealth -= tempEffect.Damage; //damages enemy
                            Player.Instance.CurrentHealth += tempEffect.Heal; //heals player
                        }

                        //rolls chance for target's weapon soul stone to apply effects
                        foreach (Effect itemEffect in target.DragonStone.DragonWeaponEffects)
                        {
                            if (!itemEffect.TargetsSelf && GameWorld.Instance.RandomInt(0, itemEffect.UpperChanceBounds) == 0 && !itemEffect.StatBuff) //has a chance to add negative effects to the player
                            {
                                playerEffects.Add(new Effect(itemEffect.Index, itemEffect.Type, target.DragonStone, target, totalDamageToDeal));
                            }
                            else if (itemEffect.TargetsSelf && GameWorld.Instance.RandomInt(0, itemEffect.UpperChanceBounds) == 0 && !itemEffect.StatBuff) //has a chance to add positive effects to the enemy
                            {
                                enemyEffects.Add(new Effect(itemEffect.Index, itemEffect.Type, target.DragonStone, target, totalDamageToDeal));
                            }
                        }
                        
                        //applies healing
                        foreach (Effect effect in enemyEffects)
                        {
                            if (effect.EffectLength > 0 && effect.Heal > 0)
                            {
                                target.CurrentHealth += effect.Heal;
                                effect.EffectLength--;
                                if (effect.Heal <= 0)
                                {
                                    EnemyScrolling("HP +0", Color.Green);
                                }
                            }
                        }
                    }
                    else
                    {
                        EnemyScrolling("Miss", Color.White);
                    }
                }
                if (totalDamageToDeal <= 0)
                {
                    PlayerScrolling($"HP -0", Color.Red);
                }

                enemyAttackAmount = 1;
            }
        }

        /// <summary>
        /// Reduces cooldown on all player skills
        /// </summary>
        private void CountCooldown()
        {
            if (Equipment.Instance.Skill1 != null && Equipment.Instance.Skill1.InternalCooldown > 0)
            {
                Equipment.Instance.Skill1.InternalCooldown--;
            }

            if (Equipment.Instance.Skill2 != null && Equipment.Instance.Skill2.InternalCooldown > 0)
            {
                Equipment.Instance.Skill2.InternalCooldown--;
            }

            if (Equipment.Instance.Skill3 != null && Equipment.Instance.Skill3.InternalCooldown > 0)
            {
                Equipment.Instance.Skill3.InternalCooldown--;
            }
        }

        /// <summary>
        /// Adds scrolling text to the player's side of the battlefield
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="color"></param>
        public void PlayerScrolling(string Text, Color color)
        {
            playerText.Add(new GameObject(new Vector2(350, 400), Text, color));
        }

        /// <summary>
        /// Adds scrolling text to the enemy's side of the battlefield
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="color"></param>
        public void EnemyScrolling(string Text, Color color)
        {
            enemyText.Add(new GameObject(new Vector2(1350, 400), Text, color));
        }
        
        /// <summary>
        /// Resets relevant variables when leaving combat
        /// </summary>
        public void ExitCombat()
        {
            for (int i = 0; i < 11; i++)
            {
                CountCooldown();
            }
            enemyDamageAbs = 0;
            enemyShield = 0;
            enemySpeedMod = 1;
            enemyDamageReduction = 1;
            playerDamageAbs = 0;
            playerShield = 0;
            playerSpeedMod = 1;
            playerDamageReduction = 1;
            playerEffects.Clear();
            enemyEffects.Clear();
            playerText.Clear();
            enemyText.Clear();
            victorySound.Play();
            PlayerText.Clear();
            EnemyText.Clear();
            enemyShield = 0;
            foreach (FilledStone stone in Equipment.Instance.EquippedEquipment)
            {
                if (stone != null)
                {
                    stone.InternalCooldown = 0;
                }
            }
            GameWorld.Instance.GameState = "Overworld";
            selectedInt = 0;
            playerAttackTimer = 0;
            enemyAttackTimer = 0;
            buttonType = "Normal";
            Target = null;
            Log.Instance.CalculateBonus();
            Player.Instance.GracePeriod = 0;
            Player.Instance.GraceStart = false;
            Player.Instance.Attacking = false;
            Player.Instance.AttackStart = false;
            Player.Instance.Hurt = false;
            Player.Instance.HurtStart = false;
        }
    }
}
