using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    class Consumable : Item
    {
        private int amount;
        private static List<Consumable> consumableList = new List<Consumable>();
       
        public int Amount { get => amount; set => amount = value; }
        internal static List<Consumable> ConsumableList { get => consumableList; set => consumableList = value; }

        /// <summary>
        /// For the game
        /// </summary>
        /// <param name="spriteName"></param>
        /// <param name="name"></param>
        /// <param name="goldCost"></param>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        /// <param name="position"></param>
        /// <param name="content"></param>
        public Consumable(string spriteName, string name, int goldCost, string type, int amount, Vector2 position, ContentManager content):base (spriteName, position, name, goldCost, type, content)
        {
            this.amount = amount;
        }

        /// <summary>
        /// For the database
        /// </summary>
        /// <param name="spriteName"></param>
        /// <param name="name"></param>
        /// <param name="goldCost"></param>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        public Consumable(string spriteName, string name, int goldCost, string type, int amount) : base(spriteName, name, goldCost, type)
        {
            this.amount = amount;
        }
    }
}
