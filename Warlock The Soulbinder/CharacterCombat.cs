using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    public class CharacterCombat : Character
    {
        protected Vector2 direction;
        protected bool isInCombat;
        protected float attackSpeed;
        protected int health;
        protected int damage;

        public CharacterCombat()
        {
        }

        public CharacterCombat(int index) : base(index)
        {
        }

        public virtual void Combat()
        {

        }
    }
}
