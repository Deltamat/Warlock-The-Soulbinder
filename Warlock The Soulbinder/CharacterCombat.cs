using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    /// <summary>
    /// Character with combat statistics
    /// </summary>
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

        /// <summary>
        /// Current Health that is inherited by player and enemy.
        /// </summary>
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
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public int Damage { get => damage; set => damage = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public int Defense { get => defense; set => defense = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public bool IsInCombat { get => isInCombat; set => isInCombat = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public bool Alive { get => alive; set => alive = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public List<int> DamageTypes { get => damageTypes; set => damageTypes = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public List<float> ResistanceTypes { get => resistanceTypes; set => resistanceTypes = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public float WaterResistance { get => waterResistance; set => waterResistance = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public float DarkResistance { get => darkResistance; set => darkResistance = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public float FireResistance { get => fireResistance; set => fireResistance = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public float AirResistance { get => airResistance; set => airResistance = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public float EarthResistance { get => earthResistance; set => earthResistance = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public float MetalResistance { get => metalResistance; set => metalResistance = value; }


        /// <summary>
        /// Empty
        /// </summary>
        public CharacterCombat()
        {
        }
    }
}