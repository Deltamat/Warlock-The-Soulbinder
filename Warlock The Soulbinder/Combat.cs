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
    class Combat : Menu
    {
        private Enemy target;

        static Combat instance;
        private Texture2D sheet;
        private Texture2D emptyButton;
        private Texture2D healthEmpty;
        private Texture2D healthFull;
        private SpriteFont CombatFont;
        private float combatDelay = 0;

        //For use when you have to change forexample in skills or items
        private string buttonType = "Normal";
        private List<GameObject> emptyButtonList = new List<GameObject>();


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
            healthEmpty = content.Load<Texture2D>("HealthEmpty");
            healthFull = content.Load<Texture2D>("HealthFull");
            target = new Enemy(1);
        }


        public override void Update(GameTime gameTime)
        {
            combatDelay += gameTime.ElapsedGameTime.Milliseconds;

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && combatDelay > 200)
            {
                CombatEvent();
                combatDelay = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sheet,new Vector2(800,300),Color.White);

            for (int i = 0; i < emptyButtonList.Count; i++)
            {
                if (i == SelectedInt)
                {
                    emptyButtonList[i].Draw(spriteBatch,Color.Gray);
                }

                else
                {
                    emptyButtonList[i].Draw(spriteBatch);
                }
               
                
            }

            //Draws the button text
            if (buttonType == "Normal")
            {
                spriteBatch.DrawString(CombatFont, "Attack", emptyButtonList[0].Position + new Vector2(50, 7), Color.White);
                spriteBatch.DrawString(CombatFont, "Skills", emptyButtonList[1].Position + new Vector2(50, 7), Color.White);
                spriteBatch.DrawString(CombatFont, "Items", emptyButtonList[2].Position + new Vector2(50, 7), Color.White);
                spriteBatch.DrawString(CombatFont, "Flee", emptyButtonList[3].Position + new Vector2(50, 7), Color.White);
            }
          
            if (buttonType == "Skills")
            {
                spriteBatch.DrawString(CombatFont, "Dam.E", emptyButtonList[0].Position + new Vector2(50, 7), Color.White);
                spriteBatch.DrawString(CombatFont, "Heal.E", emptyButtonList[1].Position + new Vector2(50, 7), Color.White);
                spriteBatch.DrawString(CombatFont, "Heal.P", emptyButtonList[2].Position + new Vector2(50, 7), Color.White);
                spriteBatch.DrawString(CombatFont, "Back", emptyButtonList[3].Position + new Vector2(50, 7), Color.White);
            }

            //Draws health and healthbars for both player and enemy
            if (target != null)
            {
                spriteBatch.Draw(healthEmpty, new Vector2(1200, 800), Color.White);
                spriteBatch.Draw(healthFull, new Vector2(1204, 803), new Rectangle(0, 0, Convert.ToInt32(PercentStat(target.CurrentHealth, target.MaxHealth) * 5.9), 70), Color.White);
                spriteBatch.DrawString(CombatFont, $"{target.CurrentHealth} / {target.MaxHealth}", new Vector2(1260, 880), Color.White);

            }

            spriteBatch.Draw(healthEmpty, new Vector2(100, 800), Color.White);
            spriteBatch.Draw(healthFull, new Vector2(104, 803), new Rectangle(0, 0,Convert.ToInt32(PercentStat(Player.Instance.CurrentHealth, Player.Instance.MaxHealth) *5.9), 70), Color.White);

            spriteBatch.DrawString(CombatFont, $"{Player.Instance.CurrentHealth} / {Player.Instance.MaxHealth}", new Vector2(160, 880), Color.White);
        }

        //Goes up and down on the button list
        public void ChangeSelected(int i)
        {
            if (Combat.Instance.SelectedInt >= 0 && Combat.Instance.SelectedInt <= 3 && combatDelay > 200)
            {
                Combat.Instance.SelectedInt += i;
                combatDelay = 0;
            }

            if (Combat.Instance.SelectedInt > 3)
            {
                Combat.Instance.SelectedInt = 3;
            }

            if (Combat.Instance.SelectedInt < 0)
            {
                Combat.Instance.SelectedInt = 0;
            }

            
        }

        //Determines what happens when a button is clicked
        public void CombatEvent()
        {
            if (buttonType == "Normal")
            {
                switch (selectedInt)
                {
                    case 0:
                        target.CurrentHealth -= Player.Instance.Damage;
                        break;

                    case 1:
                        buttonType = "Skills";
                        break;

                    case 2:
                        //buttonType = "Items";
                        break;

                    case 3:
                        //Escape
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

        //used to set a target on the enemey for effects
        public void SelectEnemy(Enemy combatEnemy)
        {
            target = combatEnemy;
        }

        //General code to give the percentage value of two numbers, going from 1 to 100
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



    }
}
