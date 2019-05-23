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
        private string name;
        protected float scale = 1;

        protected string Name { get => name; }

        public Character()
        {
        }

    }
}
