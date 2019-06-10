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
        public string Name { get => name; set => name = value; }
        
        /// <summary>
        /// Tumbleweed.
        /// </summary>
        public Item()
        {

        }
    }
}
