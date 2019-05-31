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
        private float combatDelay = 0;
        private float playerAttackTimer;
        private float enemyAttackTimer;
        private float turnTimer = 1;
        private List<Effect> toBeRemovedEffects = new List<Effect>();
        private int enemyAttackAmount = 1;
        private int playerAttackAmount = 1;
        private float enemyDamageReduction;
        private float playerDamageReduction;
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

            if (target != null && playerAttackTimer < turnTimer && enemyAttackTimer < turnTimer && !Player.Instance.Attacking && !Player.Instance.Hurt && !Player.Instance.AttackStart && !Player.Instance.HurtStart)
            {
                buttonColor = Color.Gray;
                playerAttackTimer += Player.Instance.AttackSpeed * playerSpeedMod;
                enemyAttackTimer += target.AttackSpeed * enemySpeedMod;
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

            if (target != null)
            {
                if (target.CurrentHealth <= 0) //if the target dies, remove target
                {
                    Equipment.Instance.ExperienceEquipment((int)(20 * Math.Pow(1.2, target.Level)));

                    if (target.Monster.Contains("Dragon")) // if enemy is a dragon mark the dragon as dead
                    {
                        switch (target.Monster)
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

                    target.Alive = false;
                    GameWorld.Instance.CurrentZone().Enemies.Remove(target);
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
            if (target != null)
            {
                spriteBatch.DrawString(combatFont, $"Level {target.Level}", new Vector2(1350, 150), Color.White);
                spriteBatch.Draw(HealthEmpty, new Vector2(1200, 800), Color.White);
                spriteBatch.Draw(HealthFull, new Vector2(1202, 802), new Rectangle(0, 0, Convert.ToInt32(PercentStat(target.CurrentHealth, target.MaxHealth) * 5.9), 70), Color.White);
                spriteBatch.DrawString(CombatFont, $"{target.CurrentHealth} / {target.MaxHealth}", new Vector2(1260, 880), Color.White);
                spriteBatch.Draw(target.Sprite, new Vector2(1250, 250), null, Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.FlipHorizontally, 1);
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
                        int tempChance = Combat.Instance.PercentStat(target.CurrentHealth, target.MaxHealth);
                        int tempInt = GameWorld.Instance.RandomInt(0, 100);

                        if ((tempChance)*3 < tempInt)
                        {
                            FilledStone.CatchMonster(target);
                            target.CurrentHealth = 0;
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
                            enemyEffects.Add(new Effect(Equipment.Instance.Skill1.SkillEffect.Index, Equipment.Instance.Skill1.SkillEffect.Type, Equipment.Instance.Skill1.SkillEffect.Stone, null, 0));
                            playerAttackTimer = 0;
                            Equipment.Instance.Skill1.InternalCooldown = Equipment.Instance.Skill1.SkillEffect.Cooldown;
                        }
                        break;
                    case 1:
                        if (Equipment.Instance.Skill2 != null && Equipment.Instance.Skill2.InternalCooldown == 0)
                        {
                            CountCooldown();
                            enemyEffects.Add(new Effect(Equipment.Instance.Skill2.SkillEffect.Index, Equipment.Instance.Skill2.SkillEffect.Type, Equipment.Instance.Skill2.SkillEffect.Stone, null, 0));
                            playerAttackTimer = 0;
                            Equipment.Instance.Skill2.InternalCooldown = Equipment.Instance.Skill2.SkillEffect.Cooldown;
                        }
                        break;
                    case 2:
                        if (Equipment.Instance.Skill3 != null && Equipment.Instance.Skill3.InternalCooldown == 0)
                        {
                            CountCooldown();
                            enemyEffects.Add(new Effect(Equipment.Instance.Skill3.SkillEffect.Index, Equipment.Instance.Skill3.SkillEffect.Type, Equipment.Instance.Skill3.SkillEffect.Stone, null, 0));
                            playerAttackTimer = 0;
                            Equipment.Instance.Skill3.InternalCooldown = Equipment.Instance.Skill3.SkillEffect.Cooldown;
                        }
                        break;
                    case 3:
                        buttonType = "Normal";
                        break;
                }
            }

          
        
        }
        

        /// <summary>
        /// Used to set a target on the enemey for effects
        /// </summary>
        /// <param name="combatEnemy"></param>
        public void SelectEnemy(Enemy combatEnemy)
        {
            target = combatEnemy;

            //Alternate take on turnTimer
            if (target.AttackSpeed > Player.Instance.AttackSpeed)
            {
                turnTimer = target.AttackSpeed * 100.5f;
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
            if (target != null)
            {
                //local values to apply effects
                bool stunned = new bool();
                bool confused = new bool();
                float accuracyMod = 1f;
                float damageMod = 1f;
                playerSpeedMod = 1f;

                if (Equipment.Instance.EquippedEquipment[0] != null && Equipment.Instance.EquippedEquipment[0].WeaponEffect.DoubleAttack && GameWorld.Instance.RandomInt(0, Equipment.Instance.EquippedEquipment[0].WeaponEffect.UpperChanceBounds) == 0) //checks for double attack 
                {
                    playerAttackAmount++;
                }

                //goes through all active effects on the enemy with an EffectLength greater than 0 and applies their effect
                foreach (Effect effect in playerEffects)
                {
                    if (effect.EffectLength > 0)
                    {
                        Player.Instance.CurrentHealth -= effect.Damage; //applies damage
                        Player.Instance.CurrentHealth += effect.Heal; //applies healing
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
                        if (Player.Instance.Damage - target.Defense > 0) //if the base damage the player should deal, after defense reduction, is greater than 0
                        {
                            damageToDeal.Add(Player.Instance.Damage - target.Defense); //adds base damage to the damageToDeal list
                        }

                        //adds damage foreach damage type to the list damageToDeal
                        for (int i = 0; i < Player.Instance.DamageTypes.Count; i++)
                        {
                            damageToDeal.Add((int)(((Player.Instance.DamageTypes[i] * damageMod) - (Player.Instance.DamageTypes[i] * damageMod * 0.01 * target.ResistanceTypes[i])) * enemyDamageReduction));
                        }

                        //adds all damage together into one variable
                        for (int i = 0; i < damageToDeal.Count; i++)
                        {
                            totalDamageToDeal += damageToDeal[i];
                        }

                        switch (target.EnemyStone.Element)
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
                        }

                        if (Equipment.Instance.EquippedEquipment[0] != null)
                        {
                            if (!Equipment.Instance.EquippedEquipment[0].WeaponEffect.TargetsSelf && GameWorld.Instance.RandomInt(0, Equipment.Instance.EquippedEquipment[0].WeaponEffect.UpperChanceBounds) == 0) //has a chance to add negative effects to the enemy
                            {
                                enemyEffects.Add(new Effect(Equipment.Instance.EquippedEquipment[0].WeaponEffect.Index, Equipment.Instance.EquippedEquipment[0].WeaponEffect.Type, Equipment.Instance.EquippedEquipment[0].WeaponEffect.Stone, Player.Instance, totalDamageToDeal));
                            }
                            else if (Equipment.Instance.EquippedEquipment[0].WeaponEffect.TargetsSelf && GameWorld.Instance.RandomInt(0, Equipment.Instance.EquippedEquipment[0].WeaponEffect.UpperChanceBounds) == 0) //has a chance to add positive effects to the player
                            {
                                playerEffects.Add(new Effect(Equipment.Instance.EquippedEquipment[0].WeaponEffect.Index, Equipment.Instance.EquippedEquipment[0].WeaponEffect.Type, Equipment.Instance.EquippedEquipment[0].WeaponEffect.Stone, Player.Instance, totalDamageToDeal));
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

            //goes through all active effects on the enemy with an EffectLength greater than 0
            foreach (Effect effect in enemyEffects)
            {
                if (effect.EffectLength > 0)
                {
                    target.CurrentHealth -= effect.Damage; //applies damage
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
                    if (effect.SpeedMod != 1f)
                    {
                        enemySpeedMod *= effect.SpeedMod;
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
                    if (target.Damage - Player.Instance.Defense > 0)
                    {
                        damageToDeal.Add(target.Damage - Player.Instance.Defense);
                    }

                    //goes through all damage types and resistance types and calculates damage to be dealt
                    for (int i = 0; i < target.DamageTypes.Count; i++)
                    {
                        damageToDeal.Add((int)(((target.DamageTypes[i] * damageMod) - (target.DamageTypes[i] * damageMod * 0.01 * Player.Instance.ResistanceTypes[i])) * playerDamageReduction));
                    }

                    //adds all damage to a single variable
                    for (int i = 0; i < damageToDeal.Count; i++)
                    {
                        totalDamageToDeal += damageToDeal[i];
                    }

                    if (confused && GameWorld.Instance.RandomInt(0, 100) < 50) //if the enemy is confused, has a chance to damage themselves
                    {
                        target.CurrentHealth -= (int)(totalDamageToDeal * 0.5);
                    }
                    else if (confused && GameWorld.Instance.RandomInt(0, 50) < 25) //if the enemy is confused, has a chance to miss
                    {
                        //does nothing
                    }
                    else if (GameWorld.Instance.RandomInt((int)(100 * accuracyMod), (int)(200 - (100 - (100 * accuracyMod)))) >= 100) //calculates hit chance
                    {
                        Player.Instance.CurrentHealth -= totalDamageToDeal;
                    }

                    if (!target.EnemyStone.WeaponEffect.TargetsSelf && GameWorld.Instance.RandomInt(0, target.EnemyStone.WeaponEffect.UpperChanceBounds) == 0) //has a chance to add negative effects to the player
                    {
                        playerEffects.Add(new Effect(target.EnemyStone.WeaponEffect.Index, target.EnemyStone.WeaponEffect.Type, target.EnemyStone.WeaponEffect.Stone, target, totalDamageToDeal));
                    }
                    else if (target.EnemyStone.WeaponEffect.TargetsSelf && GameWorld.Instance.RandomInt(0, target.EnemyStone.WeaponEffect.UpperChanceBounds) == 0) //has a chance to add positive effects to the enemy
                    {
                        enemyEffects.Add(new Effect(target.EnemyStone.WeaponEffect.Index, target.EnemyStone.WeaponEffect.Type, target.EnemyStone.WeaponEffect.Stone, target, totalDamageToDeal));
                    }

                    //applies healing
                    foreach (Effect effect in enemyEffects)
                    {
                        if (effect.EffectLength > 0 && effect.Heal > 0)
                        {
                            target.CurrentHealth += effect.Heal;
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
            playerEffects.Clear();
            enemyEffects.Clear();
            playerText.Clear();
            enemyText.Clear();
            victorySound.Play();
            PlayerText.Clear();
            EnemyText.Clear();
            GameWorld.Instance.GameState = "Overworld";
            selectedInt = 0;
            playerAttackTimer = 0;
            enemyAttackTimer = 0;
            buttonType = "Normal";
            target = null;
            Player.Instance.GracePeriod = 0;
            Player.Instance.GraceStart = false;
            Player.Instance.Attacking = false;
            Player.Instance.AttackStart = false;
            Player.Instance.Hurt = false;
            Player.Instance.HurtStart = false;
        }
    }
}
