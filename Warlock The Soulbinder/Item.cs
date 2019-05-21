using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    class Item : GameObject
    {
        protected string name;
        protected int goldCost;
        protected string type;
        
        /// <summary>
        /// for the game
        /// </summary>
        /// <param name="spriteName"></param>
        /// <param name="position"></param>
        /// <param name="name"></param>
        /// <param name="goldCost"></param>
        /// <param name="type"></param>
        /// <param name="content"></param>
        public Item(string spriteName, Vector2 position, string name, int goldCost, string type, ContentManager content) :base (position, spriteName, content)
        {
            this.name = name;
            this.goldCost = goldCost;
            this.type = type;
        }

        /// <summary>
        /// For the database
        /// </summary>
        /// <param name="spriteName"></param>
        /// <param name="name"></param>
        /// <param name="goldCost"></param>
        /// <param name="type"></param>
        public Item(string spriteName, string name, int goldCost, string type) :base()
        {
            this.spriteName = spriteName;
            this.name = name;
            this.goldCost = goldCost;
            this.type = type;
        }
    }
}
