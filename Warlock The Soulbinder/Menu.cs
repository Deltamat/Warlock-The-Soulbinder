using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    public class Menu
    {
        protected int selectedInt = 0;
        public int SelectedInt { get => selectedInt; set => selectedInt = value; }
        
        /// <summary>
        /// Empty
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
        }

        /// <summary>
        /// Empty
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}