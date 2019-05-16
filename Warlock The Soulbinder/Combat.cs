using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Warlock_The_Soulbinder
{
    class Combat : Menu
    {
        private Enemy target;

        static Combat instance;

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

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(sprite, position, Color.White);
        }


        //used to set a target on the enemey for effects
        public void SelectEnemy(Enemy combatEnemy)
           {
            target = combatEnemy;
           }

        //Code to call in command pattern to change the selected button
        public void IncreaseCombatInt()
            {
                if (SelectedInt < 3)
                {
                SelectedInt++;
                }
                
            }
        //Code to call in command pattern to change the selected button
         public void DecreaseCombatInt()
            {
                if (SelectedInt != 0)
                {
                SelectedInt--;
                }
                
            }
    }
}
