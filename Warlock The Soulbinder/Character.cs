using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    class Character : GameObject
    {
        protected float movementSpeed;
        private string name;

        public Character(int index)
        {

        }

        protected string Name { get => name; }
    }
}
