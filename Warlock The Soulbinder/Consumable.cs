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
        private static int potion = 5;
        private static int soulStone = 5;
        private static int bomb = 5;
       
        public int Amount { get => amount; set => amount = value; }
        //public static List<Consumable> ConsumableList { get => consumableList; set => consumableList = value; } // OBS! bliver den brugt
        public static int Potion { get => potion; set => potion = value; }
        public static int SoulStone { get => soulStone; set => soulStone = value; }
        public static int Bomb { get => bomb; set => bomb = value; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="amount"></param>
        public Consumable(string name, int amount) 
        {
            this.amount = amount;
        }
    }
}
