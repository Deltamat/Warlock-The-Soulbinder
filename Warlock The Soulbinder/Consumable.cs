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

        public Consumable(string name)
        {
            this.name = name;
        }
    }
}
