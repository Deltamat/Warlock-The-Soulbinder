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
        protected int defense;
        protected float waterResistance;
        protected float darkResistance;
        protected float fireResistance;
        protected float airResistance;
        protected float earthResistance;
        protected float metalResistance;

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
