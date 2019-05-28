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
        private string name;
        private int goldCost;
        private string type;

        public string Name { get => name; set => name = value; }
        public int GoldCost { get => goldCost; set => goldCost = value; }
        public string Type { get => type; set => type = value; }



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
            this.Name = name;
            this.GoldCost = goldCost;
            this.Type = type;
        }

        public Item()
        {

        }
    }
}
