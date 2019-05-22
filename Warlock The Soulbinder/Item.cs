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
        /// For the database
        /// </summary>
        /// <param name="spriteName"></param>
        /// <param name="name"></param>
        /// <param name="goldCost"></param>
        /// <param name="type"></param>
        public Item(string spriteName, string name, int goldCost, string type)
        {
            this.spriteName = spriteName;
            this.name = name;
            this.goldCost = goldCost;
            this.type = type;
        }

        public Item()
        {

        }

        public Item()
        {

        }
    }
}
