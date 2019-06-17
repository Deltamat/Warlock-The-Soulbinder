using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Warlock_The_Soulbinder
{
    /// <summary>
    /// Character class
    /// </summary>
    public class Character : GameObject
    {
        protected float movementSpeed;
        protected float scale = 1;
    
        /// <summary>
        /// Empty
        /// </summary>
        public Character()
        {
        }
    }
}