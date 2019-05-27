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
        private Texture2D emptyButton;
        private Texture2D healthEmpty;
        private Texture2D healthFull;
        private Texture2D turnFull;
        private SpriteFont combatFont;
        private float combatDelay = 0;
        private float playerAttackTimer;
        private float enemyAttackTimer;
        private float turnTimer = 1;
        private Color buttonColor = Color.White;
        Sound victorySound = new Sound("battleVictory");

        //For use when you have to change forexample in skills or items
        private string buttonType = "Normal";
        private List<GameObject> emptyButtonList = new List<GameObject>();

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
                playerAttackTimer += Player.Instance.AttackSpeed;
                enemyAttackTimer += target.AttackSpeed;
            }
            else if (playerAttackTimer >= turnTimer)
            {
                buttonColor = Color.White;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && combatDelay > 200 && playerAttackTimer >= turnTimer)
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
                    victorySound.Play();
                    target.Alive = false;
                    GameWorld.Instance.enemies.Remove(target);
                    ExitCombat();
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
                spriteBatch.DrawString(CombatFont, "Items", emptyButtonList[2].Position + new Vector2(50, 7), buttonColor);
                spriteBatch.DrawString(CombatFont, "Flee", emptyButtonList[3].Position + new Vector2(50, 7), buttonColor);
            }
            else if (buttonType == "Skills")
            {
                spriteBatch.DrawString(CombatFont, "Dam.E", emptyButtonList[0].Position + new Vector2(50, 7), Color.White);
                spriteBatch.DrawString(CombatFont, "Heal.E", emptyButtonList[1].Position + new Vector2(50, 7), Color.White);
                spriteBatch.DrawString(CombatFont, "Heal.P", emptyButtonList[2].Position + new Vector2(50, 7), Color.White);
                spriteBatch.DrawString(CombatFont, "Back", emptyButtonList[3].Position + new Vector2(50, 7), Color.White);
            }

            //Draws health, healthbars and turn bar for enemy
            if (target != null)
            {
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
                        PlayerAttack();
                        break;
                    case 1: //skill
                        buttonType = "Skills";
                        break;
                    case 2: //item
                        //buttonType = "Items";
                        break;
                    case 3: //flee
                        ExitCombat();
                        break;
                }
            }
            else if (buttonType == "Skills")
            {
                switch (selectedInt)
                {
                    case 0:
                        target.CurrentHealth -= 3;
                        break;
                    case 1:
                        target.CurrentHealth += 3;
                        break;
                    case 2:
                        Player.Instance.CurrentHealth += 2;
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
            turnTimer = (target.AttackSpeed + Player.Instance.AttackSpeed) * 200.5f;
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
        public void PlayerAttack()
        {
            buttonColor = Color.Gray;
            if (target != null)
            {
                playerAttackTimer = 0;
                Player.Instance.AttackStart = true;

                List<int> damageToDeal = new List<int>();
                int totalDamageToDeal = 0;

                damageToDeal.Add(Player.Instance.Damage - target.Defense);
                for (int i = 0; i < target.ResistanceTypes.Count; i++)
                {
                    damageToDeal.Add((int)(Player.Instance.DamageTypes[i] - (Player.Instance.DamageTypes[i] * 0.01 * target.ResistanceTypes[i])));
                }
                for (int i = 0; i < damageToDeal.Count; i++)
                {
                    totalDamageToDeal += damageToDeal[i];
                }

                target.CurrentHealth -= totalDamageToDeal;
                combatDelay = 0;
            }
        }

        /// <summary>
        /// Controls enemy's turn logic
        /// </summary>
        public void EnemyTurn()
        {
            enemyAttackTimer = 0;
            Player.Instance.HurtStart = true;

            List<int> damageToDeal = new List<int>();
            int totalDamageToDeal = 0;

            if (target.Damage - Player.Instance.Defense > 0)
            {
                damageToDeal.Add(target.Damage - Player.Instance.Defense);
            }
            
            for (int i = 0; i < target.ResistanceTypes.Count; i++)
            {
                damageToDeal.Add((int)(target.DamageTypes[i] - (target.DamageTypes[i] * 0.01 * Player.Instance.ResistanceTypes[i])));
            }
            for (int i = 0; i < damageToDeal.Count; i++)
            {
                totalDamageToDeal += damageToDeal[i];
            }

            Player.Instance.CurrentHealth -= totalDamageToDeal;
        }

        /// <summary>
        /// Resets relevant variables when leaving combat
        /// </summary>
        public void ExitCombat()
        {
            GameWorld.Instance.GameState = "Overworld";
            selectedInt = 0;
            playerAttackTimer = 0;
            enemyAttackTimer = 0;
            buttonType = "Normal";
            Player.Instance.GracePeriod = 0;
            Player.Instance.GraceStart = false;
            target = null;
            Player.Instance.Attacking = false;
            Player.Instance.AttackStart = false;
            Player.Instance.Hurt = false;
            Player.Instance.HurtStart = false;
        }
    }
}
