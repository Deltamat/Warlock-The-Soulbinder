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
       
        public int Amount { get => amount; set => amount = value; }

        public Consumable(string name)
        {
            this.name = name;
        }
    }
}
