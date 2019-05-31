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
        protected bool isInCombat = true;
        protected float attackSpeed;
        protected bool alive = true;
        protected int currentHealth;
        protected int maxHealth;
        protected int damage;
        protected int waterDamage;
        protected int darkDamage;
        protected int fireDamage;
        protected int airDamage;
        protected int earthDamage;
        protected int metalDamage;
        protected int defense;
        protected float waterResistance;
        protected float darkResistance;
        protected float fireResistance;
        protected float airResistance;
        protected float earthResistance;
        protected float metalResistance;
        protected List<int> damageTypes = new List<int>();
        protected List<float> resistanceTypes = new List<float>();

        public virtual int CurrentHealth
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
                else if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }
            }
        }

        public int Damage { get => damage; set => damage = value; }
        public int Defense { get => defense; set => defense = value; }
        public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
        public bool IsInCombat { get => isInCombat; set => isInCombat = value; }
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        public bool Alive { get => alive; set => alive = value; }
        public List<int> DamageTypes { get => damageTypes; set => damageTypes = value; }
        public List<float> ResistanceTypes { get => resistanceTypes; set => resistanceTypes = value; }
        public float WaterResistance { get => waterResistance; set => waterResistance = value; }
        public float DarkResistance { get => darkResistance; set => darkResistance = value; }
        public float FireResistance { get => fireResistance; set => fireResistance = value; }
        public float AirResistance { get => airResistance; set => airResistance = value; }
        public float EarthResistance { get => earthResistance; set => earthResistance = value; }
        public float MetalResistance { get => metalResistance; set => metalResistance = value; }

        public CharacterCombat()
        {
        }
    }
}
