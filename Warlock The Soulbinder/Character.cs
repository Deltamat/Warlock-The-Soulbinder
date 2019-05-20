using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Warlock_The_Soulbinder
{
    public class Character : GameObject
    {
        protected float movementSpeed;
        private string name = "Jones";

        public Character()
        {
        }

        public Character(int index)
        {

        }

        protected string Name { get => name; }        
    }
}
