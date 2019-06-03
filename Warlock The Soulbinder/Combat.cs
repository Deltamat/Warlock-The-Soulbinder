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
        private Texture2D turnFull;
        private SpriteFont combatFont;
        private float wolfBuff = 0;
        private float combatDelay = 0;
        private float playerAttackTimer;
        private float enemyAttackTimer;
        private float turnTimer = 1;
        private List<Effect> toBeRemovedEffects = new List<Effect>();
        private int enemyAttackAmount = 1;
        private int playerAttackAmount = 1;
        private float enemyDamageReduction = 1;
        private float playerDamageReduction = 1;
        private float playerSpeedMod = 1;
        private float enemySpeedMod = 1;

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
        public List<GameObject> PlayerText { get => playerText; set => playerText = value; }
        public List<GameObject> EnemyText { get => enemyText; set => enemyText = value; }
        public float WolfBuff { get => wolfBuff; set => wolfBuff = value; }
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

            if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeySelect) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonSelect)) && combatDelay > 200 && playerAttackTimer >= turnTimer)
            {
                CombatEvent();
                combatDelay = 0;
            }

            if (enemyAttackTimer >= turnTimer)
            {
                EnemyTurn();
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
                spriteBatch.DrawString(CombatFont, "Capture", emptyButtonList[2].Position + new Vector2(50, 7), buttonColor);
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
                spriteBatch.Draw(HealthFull, new Vector2(1202, 802), new Rectangle(0, 0, Convert.ToInt32(PercentStat(Target.CurrentHealth, Target.MaxHealth) * 5.9), 70), Color.White);
                spriteBatch.DrawString(CombatFont, $"{Target.CurrentHealth} / {Target.MaxHealth}", new Vector2(1260, 880), Color.White);
                spriteBatch.Draw(Target.Sprite, new Vector2(1250, 250), null, Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.FlipHorizontally, 1);
                spriteBatch.Draw(HealthEmpty, new Vector2(1200, 700), Color.White);
                spriteBatch.Draw(turnFull, new Vector2(1202, 702), new Rectangle(0, 0, Convert.ToInt32(PercentStat((int)enemyAttackTimer, (int)turnTimer) * 5.9), 70), Color.White);
            }

            //Draws health, healthbars and turn bar for player
            Player.Instance.ChooseAnimationFrame();
            spriteBatch.Draw(HealthEmpty, new Vector2(100, 800), Color.White);
            spriteBatch.Draw(HealthFull, new Vector2(102, 802), new Rectangle(0, 0, Convert.ToInt32(PercentStat(Player.Instance.CurrentHealth, Player.Instance.MaxHealth) * 5.9), 70), Color.White);
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
                        CountCooldown();
                        int tempChance = PercentStat(target.CurrentHealth, target.MaxHealth);
                        int tempInt = GameWorld.Instance.RandomInt(0, 100);

                        if ((tempChance)*2 < tempInt)
                        {
                            FilledStone.CatchMonster(Target);
                            Target.CurrentHealth = 0;
                        }

                        playerAttackTimer = 0;
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
                            if (Equipment.Instance.Skill1.SkillEffect.TargetsSelf)
                            {
                                playerEffects.Add(new Effect(Equipment.Instance.Skill1.SkillEffect.Index, Equipment.Instance.Skill1.SkillEffect.Type, Equipment.Instance.Skill1.SkillEffect.Stone, Player.Instance, 0));
                                playerDamageReduction *= playerEffects[playerEffects.Count - 1].DamageReduction;
                            }
                            else if (!Equipment.Instance.Skill1.SkillEffect.TargetsSelf)
                            {
                                enemyEffects.Add(new Effect(Equipment.Instance.Skill1.SkillEffect.Index, Equipment.Instance.Skill1.SkillEffect.Type, Equipment.Instance.Skill1.SkillEffect.Stone, target, 0));
                            }
                            playerAttackTimer = 0;
                            Equipment.Instance.Skill1.InternalCooldown = Equipment.Instance.Skill1.SkillEffect.Cooldown;
                        }
                        break;
                    case 1:
                        if (Equipment.Instance.Skill2 != null && Equipment.Instance.Skill2.InternalCooldown == 0)
                        {
                            CountCooldown();
                            if (Equipment.Instance.Skill2.SkillEffect.TargetsSelf)
                            {
                                playerEffects.Add(new Effect(Equipment.Instance.Skill2.SkillEffect.Index, Equipment.Instance.Skill2.SkillEffect.Type, Equipment.Instance.Skill2.SkillEffect.Stone, Player.Instance, 0));
                                playerDamageReduction *= playerEffects[playerEffects.Count - 1].DamageReduction;
                            }
                            else if (!Equipment.Instance.Skill2.SkillEffect.TargetsSelf)
                            {
                                enemyEffects.Add(new Effect(Equipment.Instance.Skill2.SkillEffect.Index, Equipment.Instance.Skill2.SkillEffect.Type, Equipment.Instance.Skill2.SkillEffect.Stone, target, 0));
                            }
                            playerAttackTimer = 0;
                            Equipment.Instance.Skill2.InternalCooldown = Equipment.Instance.Skill2.SkillEffect.Cooldown;
                        }
                        break;
                    case 2:
                        if (Equipment.Instance.Skill3 != null && Equipment.Instance.Skill3.InternalCooldown == 0)
                        {
                            CountCooldown();
                            if (Equipment.Instance.Skill3.SkillEffect.TargetsSelf)
                            {
                                playerEffects.Add(new Effect(Equipment.Instance.Skill3.SkillEffect.Index, Equipment.Instance.Skill3.SkillEffect.Type, Equipment.Instance.Skill3.SkillEffect.Stone, Player.Instance, 0));
                                playerDamageReduction *= playerEffects[playerEffects.Count - 1].DamageReduction;
                            }
                            else if (!Equipment.Instance.Skill3.SkillEffect.TargetsSelf)
                            {
                                enemyEffects.Add(new Effect(Equipment.Instance.Skill3.SkillEffect.Index, Equipment.Instance.Skill3.SkillEffect.Type, Equipment.Instance.Skill3.SkillEffect.Stone, target, 0));
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
                        if (effect.Damage != 0)
                        {
                            PlayerScrolling($"HP -{effect.Damage}", Color.Red); //adds damage to PlayerScrolling
                        }
                        confused = effect.Confuse; //applies confuse
                        stunned = effect.Stun; //applies stun
                        if (effect.AccuracyMod != 1f && effect.AccuracyMod < accuracyMod) //effects has a base AccuracyMod of 1, only overrides if the AccuracyMod is more effective
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

                if (Equipment.Instance.EquippedEquipment[1] != null && Equipment.Instance.EquippedEquipment[1].ArmorEffect.StunImmunity)
                {
                    if (Equipment.Instance.EquippedEquipment[1].ArmorEffect.StunImmunity) //checks if the player is immune to stuns
                    {
                        stunned = false;
                    }
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
                            damageToDeal.Add(Player.Instance.Damage - Target.Defense); //adds base damage to the damageToDeal list
                        }

                        //adds damage foreach damage type to the list damageToDeal
                        for (int i = 0; i < Player.Instance.DamageTypes.Count; i++)
                        {
                            damageToDeal.Add((int)(((Player.Instance.DamageTypes[i] * damageMod) - (Player.Instance.DamageTypes[i] * damageMod * 0.01 * Target.ResistanceTypes[i])) * enemyDamageReduction));
                        }

                        //adds all damage together into one variable
                        for (int i = 0; i < damageToDeal.Count; i++)
                        {
                            totalDamageToDeal += damageToDeal[i];
                        }

                        //Adds wolf damage buff
                        totalDamageToDeal = (int)(totalDamageToDeal + (totalDamageToDeal * WolfBuff));
                        switch (Target.EnemyStone.Element)
                        {
                            case "Neutral":
                                totalDamageToDeal = (int)Math.Round(totalDamageToDeal * (1 + Log.Instance.NeutralBonus));
                                break;
                            case "Earth":
                                totalDamageToDeal = (int)Math.Round(totalDamageToDeal * (1 + Log.Instance.EarthBonus));
                                break;
                            case "Water":
                                totalDamageToDeal = (int)Math.Round(totalDamageToDeal * (1 + Log.Instance.WaterBonus));
                                break;
                            case "Metal":
                                totalDamageToDeal = (int)Math.Round(totalDamageToDeal * (1 + Log.Instance.MetalBonus));
                                break;
                            case "Air":
                                totalDamageToDeal = (int)Math.Round(totalDamageToDeal * (1 + Log.Instance.AirBonus));
                                break;
                            case "Dark":
                                totalDamageToDeal = (int)Math.Round(totalDamageToDeal * (1 + Log.Instance.DarkBonus));
                                break;
                            case "Fire":
                                totalDamageToDeal = (int)Math.Round(totalDamageToDeal * (1 + Log.Instance.FireBonus));
                                break;
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

                            //armor effects of target
                            if (target.EnemyStone.ArmorEffect.TargetsSelf && GameWorld.Instance.RandomInt(0, target.EnemyStone.ArmorEffect.UpperChanceBounds) == 0) //has a chance to add positive effects to the enemy
                            {
                                enemyEffects.Add(new Effect(target.EnemyStone.ArmorEffect.Index, target.EnemyStone.ArmorEffect.Type, target.EnemyStone, target, totalDamageToDeal));
                            }
                            else if (target.EnemyStone.ArmorEffect.TargetsSelf && GameWorld.Instance.RandomInt(0, target.EnemyStone.ArmorEffect.UpperChanceBounds) == 0) //has a chance to add negative effects to the player
                            {
                                playerEffects.Add(new Effect(target.EnemyStone.ArmorEffect.Index, target.EnemyStone.ArmorEffect.Type, target.EnemyStone, target, totalDamageToDeal));
                            }
                            else if (target.EnemyStone.ArmorEffect.TargetsBoth && GameWorld.Instance.RandomInt(0, target.EnemyStone.ArmorEffect.UpperChanceBounds) == 0) //has a chance to damage player and heal target
                            {
                                Effect tempEffect = new Effect(target.EnemyStone.ArmorEffect.Index, target.EnemyStone.ArmorEffect.Type, target.EnemyStone, target, 0);
                                Player.Instance.CurrentHealth -= tempEffect.Damage; //damages player
                                PlayerScrolling($"HP -{tempEffect.Damage}", Color.Red); //adds damage to PlayerScrolling
                                target.CurrentHealth += tempEffect.Heal; //heals target
                                EnemyScrolling($"HP +{tempEffect.Heal}", Color.Green); //adds heal to EnemyScrolling
                            }
                        }

                        if (Equipment.Instance.Weapon != null)
                        {
                            if (!Equipment.Instance.Weapon.WeaponEffect.TargetsSelf && GameWorld.Instance.RandomInt(0, Equipment.Instance.Weapon.WeaponEffect.UpperChanceBounds) == 0) //has a chance to add negative effects to the enemy
                            {
                                enemyEffects.Add(new Effect(Equipment.Instance.Weapon.WeaponEffect.Index, Equipment.Instance.Weapon.WeaponEffect.Type, Equipment.Instance.Weapon, Player.Instance, totalDamageToDeal));
                            }
                            else if (Equipment.Instance.Weapon.WeaponEffect.TargetsSelf && GameWorld.Instance.RandomInt(0, Equipment.Instance.Weapon.WeaponEffect.UpperChanceBounds) == 0) //has a chance to add positive effects to the player
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
                                PlayerScrolling($"HP +{effect.Heal}", Color.Green);
                            }
                        }

                        EnemyScrolling($"HP -{totalDamageToDeal}", Color.Red);
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

            if (target.EnemyStone.WeaponEffect.DoubleAttack && GameWorld.Instance.RandomInt(0, target.EnemyStone.WeaponEffect.UpperChanceBounds) == 0) //checks for double attack 
            {
                playerAttackAmount++;
            }

            //goes through all active effects on the enemy with an EffectLength greater than 0
            foreach (Effect effect in enemyEffects)
            {
                if (effect.EffectLength > 0)
                {
                    target.CurrentHealth -= effect.Damage; //applies damage
                    if (effect.Damage != 0)
                    {
                        EnemyScrolling($"HP -{effect.Damage}", Color.Red); //adds damage to EnemyScrolling
                    }
                    confused = effect.Confuse; //applies confuse
                    stunned = effect.Stun; //applies stun
                    if (effect.AccuracyMod != 1f && effect.AccuracyMod < accuracyMod) //effects has a base AccuracyMod of 1, only overrides if the AccuracyMod is more effective
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

            enemyAttackTimer = 0;
            if (!stunned)
            {
                Player.Instance.HurtStart = true; //starts player's hurt animation

                List<int> damageToDeal = new List<int>();
                int totalDamageToDeal = 0;

                for (int j = 0; j < enemyAttackAmount; j++) //foreach time the enemy should attack
                {
                    //adds the damage to be dealt
                    if (Target.Damage - Player.Instance.Defense > 0)
                    {
                        damageToDeal.Add(Target.Damage - Player.Instance.Defense);
                    }

                    //goes through all damage types and resistance types and calculates damage to be dealt
                    for (int i = 0; i < Target.DamageTypes.Count; i++)
                    {
                        damageToDeal.Add((int)(((Target.DamageTypes[i] * damageMod) - (Target.DamageTypes[i] * damageMod * 0.01 * Player.Instance.ResistanceTypes[i])) * playerDamageReduction));
                    }

                    //adds all damage to a single variable
                    for (int i = 0; i < damageToDeal.Count; i++)
                    {
                        totalDamageToDeal += damageToDeal[i];
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

                        //armor effects of target
                        if (Equipment.Instance.Armor != null && Equipment.Instance.Armor.ArmorEffect.TargetsSelf && GameWorld.Instance.RandomInt(0, target.EnemyStone.ArmorEffect.UpperChanceBounds) == 0) //has a chance to add positive effects to the enemy
                        {
                            playerEffects.Add(new Effect(Equipment.Instance.Armor.ArmorEffect.Index, Equipment.Instance.Armor.ArmorEffect.Type, Equipment.Instance.Armor, Player.Instance, totalDamageToDeal));
                        }
                        else if (Equipment.Instance.Armor != null && Equipment.Instance.Armor.ArmorEffect.TargetsSelf && GameWorld.Instance.RandomInt(0, target.EnemyStone.ArmorEffect.UpperChanceBounds) == 0) //has a chance to add negative effects to the player
                        {
                            enemyEffects.Add(new Effect(Equipment.Instance.Armor.ArmorEffect.Index, Equipment.Instance.Armor.ArmorEffect.Type, Equipment.Instance.Armor, Player.Instance, totalDamageToDeal));
                        }
                        else if (Equipment.Instance.Armor != null && Equipment.Instance.Armor.ArmorEffect.TargetsBoth && GameWorld.Instance.RandomInt(0, target.EnemyStone.ArmorEffect.UpperChanceBounds) == 0) //has a chance to damage player and heal target
                        {
                            Effect tempEffect = new Effect(Equipment.Instance.Armor.ArmorEffect.Index, Equipment.Instance.Armor.ArmorEffect.Type, Equipment.Instance.Armor, Player.Instance, 0);
                            target.CurrentHealth -= tempEffect.Damage; //damages enemy
                            EnemyScrolling($"HP -{tempEffect.Damage}", Color.Red); //adds damage to EnemyScrolling
                            Player.Instance.CurrentHealth += tempEffect.Heal; //heals player
                            PlayerScrolling($"HP +{tempEffect.Heal}", Color.Green); //adds heal to PlayerScrolling
                        }
                    }

                    if (!Target.EnemyStone.WeaponEffect.TargetsSelf && GameWorld.Instance.RandomInt(0, Target.EnemyStone.WeaponEffect.UpperChanceBounds) == 0) //has a chance to add negative effects to the player
                    {
                        playerEffects.Add(new Effect(target.EnemyStone.WeaponEffect.Index, target.EnemyStone.WeaponEffect.Type, target.EnemyStone, target, totalDamageToDeal));
                    }
                    else if (Target.EnemyStone.WeaponEffect.TargetsSelf && GameWorld.Instance.RandomInt(0, Target.EnemyStone.WeaponEffect.UpperChanceBounds) == 0) //has a chance to add positive effects to the enemy
                    {
                        enemyEffects.Add(new Effect(target.EnemyStone.WeaponEffect.Index, target.EnemyStone.WeaponEffect.Type, target.EnemyStone, target, totalDamageToDeal));
                    }

                    //applies healing
                    foreach (Effect effect in enemyEffects)
                    {
                        if (effect.EffectLength > 0 && effect.Heal > 0)
                        {
                            Target.CurrentHealth += effect.Heal;
                            effect.EffectLength--;
                            EnemyScrolling($"HP +{effect.Heal}", Color.Green);
                        }
                    }

                    PlayerScrolling($"HP -{totalDamageToDeal}", Color.Red);
                }
                enemyAttackAmount = 1;
            }
        }

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="color"></param>
        public void PlayerScrolling(string Text, Color color)
        {
            playerText.Add(new GameObject(new Vector2(350, 400), Text, color));
        }

        /// <summary>
        /// 
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
            playerEffects.Clear();
            enemyEffects.Clear();
            playerText.Clear();
            enemyText.Clear();
            victorySound.Play();
            PlayerText.Clear();
            EnemyText.Clear();

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
            Player.Instance.GracePeriod = 0;
            Player.Instance.GraceStart = false;
            Player.Instance.Attacking = false;
            Player.Instance.AttackStart = false;
            Player.Instance.Hurt = false;
            Player.Instance.HurtStart = false;
        }
    }
}
