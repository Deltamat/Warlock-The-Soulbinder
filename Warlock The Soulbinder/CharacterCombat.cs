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
        protected int currentHealth;
        protected int maxHealth;
        protected int damage;
        protected int defense;
        protected float waterResistance;
        protected float darkResistance;
        protected float fireResistance;
        protected float airResistance;
        protected float earthResistance;
        protected float metalResistance;
        protected bool alive = true;

        public int CurrentHealth
        {
            get
            {
                return currentHealth;
            }
            set
            {
                currentHealth = value;
                if (currentHealth <= 0)
                {
                    Alive = false;
                }
                if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }
            }
        }

        public int Damage { get => damage; set => damage = value; }
        public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
        public bool IsInCombat { get => isInCombat; set => isInCombat = value; }
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        public bool Alive { get => alive; set => alive = value; }

        public CharacterCombat()
        {
        }

        public CharacterCombat(int index) : base(index)
        {
        }
    }
}
