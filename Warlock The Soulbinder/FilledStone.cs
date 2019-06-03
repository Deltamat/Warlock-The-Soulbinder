﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    class FilledStone : Item
    {
        private string monster;
        private string element;
        private int level;
        private int experience = 0;
        private int experienceRequired = 13;
        private bool equipped;
        private string equipmentSlot;
        private static int stoneListPages = 0;
        private string weaponName;
        private string armorName;
        private string skillName;
        private int internalCooldown;
        private Effect weaponEffect;
        private Effect armorEffect;
        private Effect skillEffect;
        public string SpriteName { get => spriteName; set => spriteName = value; }
        public string Monster { get => monster; set => monster = value; }
        public string Element { get => element; set => element = value; }
        public int Level { get => level; set => level = value; }
        public int Experience { get => experience; set => experience = value; }
        public bool Equipped { get => equipped; set => equipped = value; }
        public string EquipmentSlot { get => equipmentSlot; set => equipmentSlot = value; }
        public static int StoneListPages { get => stoneListPages; set => stoneListPages = value; }
        private int id;

        private int maxHealth;
        protected int attackSpeed;
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

        public int Id { get => id; private set => id = value; }
        public string WeaponName { get => weaponName; set => weaponName = value; }
        public string ArmorName { get => armorName; set => armorName = value; }
        public string SkillName { get => skillName; set => skillName = value; }
        public int ExperienceRequired { get => experienceRequired; set => experienceRequired = value; }
        public Effect WeaponEffect { get => weaponEffect; set => weaponEffect = value; }
        public Effect ArmorEffect { get => armorEffect; set => armorEffect = value; }
        public Effect SkillEffect { get => skillEffect; set => skillEffect = value; }
        public int Damage { get => damage; set => damage = value; }
        public int Defense { get => defense; set => defense = value; }
        public int AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
        public List<int> DamageTypes { get => damageTypes; set => damageTypes = value; }
        public List<float> ResistanceTypes { get => resistanceTypes; set => resistanceTypes = value; }
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        public int InternalCooldown { get => internalCooldown; set => internalCooldown = value; }

        //public string Name { get => name; set => name = value; }

        public static List<FilledStone> StoneList
        {
            get
            {
                //Code to make pages for the filled stones
                StoneListPages = 0;
                int tempStoneList = stoneList.Count;
                for (int i = 0; i < 99; i++)
                {
                    if (tempStoneList - 9 > 0)
                    {
                        StoneListPages++;
                        tempStoneList -= 9;
                    }
                }
                return stoneList;
            }
            set
            {
                stoneList = value;
                
            }
        }

        private static List<FilledStone> stoneList = new List<FilledStone>();

        /// <summary>
        /// Constructor for loading in from a saved game
        /// </summary>
        /// <param name="monster"></param>
        /// <param name="experience"></param>
        /// <param name="level"></param>
        public FilledStone(string monster, int experience, string equipmentSlot, int level, int damage, int maxHealth, int attackSpeed)
        {
            EquipmentSlot = equipmentSlot;
            
            Experience = experience;
            Monster = monster;
            Level = level;
            spriteName = $"monsters/Orbs/{Monster}";
            sprite = GameWorld.ContentManager.Load<Texture2D>(spriteName);

            Damage = damage;
            this.maxHealth = maxHealth;
            this.attackSpeed = attackSpeed;

            //switch case to determine the element, and name of abilities, based on the monster type
            switch (Monster)
            {
                case "sheep":
                    Name = "Sheep";
                    Element = "neutral";
                    weaponName = "Headbutt";
                    armorName = "Woolen Armor";
                    skillName = "Headbutt";
                    break;
                case "bear":
                    Name = "Bear";
                    Element = "neutral";
                    weaponName = "Maul";
                    armorName = "Ursadaen Fortitude";
                    skillName = "Maul";
                    break;
                case "wolf":
                    Name = "Wolf";
                    Element = "neutral";
                    weaponName = "Wolf Bite";
                    armorName = "Wolf Revenge";
                    skillName = "Wolf Frenzy";
                    break;
                case "plantEater":
                    Name = "Plant Eater";
                    Element = "earth";
                    weaponName = "Symplastic Strike";
                    armorName = "Apoplastic Defence";
                    skillName = "Drain Life";
                    break;
                case "insectSoldier":
                    Name = "Insect Soldier";
                    Element = "earth";
                    weaponName = "Toxic Barbs";
                    armorName = "Poisonous Skin";
                    skillName = "Venomous Strike";
                    break;
                case "slimeSnake":
                    Name = "Slime Snake";
                    Element = "earth";
                    weaponName = "Corrosive Slime";
                    armorName = "Hardened Gel";
                    skillName = "Protective Goop";
                    break;
                case "tentacle":
                    Name = "Tentacle";
                    Element = "water";
                    weaponName = "Constraining Grapple";
                    armorName = "Retaliating Slap";
                    skillName = "Tentacle Grap";
                    break;
                case "frog":
                    Name = "Frog";
                    Element = "water";
                    weaponName = "Slipping Splash";
                    armorName = "Slimy Skin";
                    skillName = "Mucus Shot";
                    break;
                case "fish":
                    Name = "Fish";
                    Element = "water";
                    weaponName = "Regenerative Strike";
                    armorName = "Sudden Mending";
                    skillName = "Healing Rain";
                    break;
                case "mummy":
                    Name = "Mummy";
                    Element = "dark";
                    weaponName = "Cursing Strike";
                    armorName = "Curse Immunity";
                    skillName = "Curse of the Mummy";
                    break;
                case "vampire":
                    Name = "Vampire";
                    Element = "dark";
                    weaponName = "Vampiric Touch";
                    armorName = "Bloodied Shield";
                    skillName = "Blood Shield";
                    break;
                case "banshee":
                    Name = "Banshee";
                    Element = "dark";
                    weaponName = "Paralysing Touch";
                    armorName = "Paralyse Immunity";
                    skillName = "Banshee's Song";
                    break;
                case "bucketMan":
                    Name = "Bucket Man";
                    Element = "metal";
                    weaponName = "Critical Bucket";
                    armorName = "Bucket Shield";
                    skillName = "ULTIMATE BUCKET DESTRUCTION!";
                    break;
                case "defender":
                    Name = "Defender";
                    Element = "metal";
                    weaponName = "Sunder Armor";
                    armorName = "Thick Plates";
                    skillName = "Defensive Stance";
                    break;
                case "sentry":
                    Name = "Sentry";
                    Element = "metal";
                    weaponName = "Accurate Strikes";
                    armorName = "Predicting Algorithm";
                    skillName = "Scan";
                    break;
                case "fireGolem":
                    Name = "Fire Golem";
                    Element = "fire";
                    weaponName = "Heavy Strikes";
                    armorName = "Obsidian Skin";
                    skillName = "Momentous Slam";
                    break;
                case "infernalDemon":
                    Name = "Infernal Demon";
                    Element = "fire";
                    weaponName = "Red-hot Smite";
                    armorName = "Immolating Presence";
                    skillName = "Incenerate";
                    break;
                case "ashZombie":
                    Name = "Ash Zombie";
                    Element = "fire";
                    weaponName = "Fiery Double-tap";
                    armorName = "Blazing Assault";
                    skillName = "Flaring Assailment";
                    break;
                case "falcon":
                    Name = "Falcon";
                    Element = "air";
                    weaponName = "Speedy Swoop";
                    armorName = "Evasive Wing-work";
                    skillName = "Quickened Agility";
                    break;
                case "bat":
                    Name = "Bat";
                    Element = "air";
                    weaponName = "Confusing Menoeuvre";
                    armorName = "Perplexing Retaliation";
                    skillName = "Sonic Scream";
                    break;
                case "raven":
                    Name = "Raven";
                    Element = "air";
                    weaponName = "Blinding Assault";
                    armorName = "Retaliative Amaurotic";
                    skillName = "Blind";
                    break;
            }

            //switch case to determine stats
            switch (Element)
            {
                case "neutral":
                    Defense = (int)(Level * 0.75f);
                    break;
                case "earth":
                    earthResistance = (float)(20 / (1 + Math.Pow(Math.E, -(Level * 0.5f))));
                    darkResistance = (float)(-20 / (1 + Math.Pow(Math.E, -(Level * 0.5f))) + Level * 0.5f);
                    earthDamage = (int)(damage * 4f);
                    break;
                case "water":
                    waterResistance = (float)(20 / (1 + Math.Pow(Math.E, -(Level * 0.5f))));
                    airResistance = (float)(-20 / (1 + Math.Pow(Math.E, -(Level * 0.5f))) + Level * 0.5f);
                    waterDamage = (int)(damage * 4f);
                    break;
                case "dark":
                    darkResistance = (float)(20 / (1 + Math.Pow(Math.E, -(Level * 0.5f))));
                    metalResistance = (float)(-20 / (1 + Math.Pow(Math.E, -(Level * 0.5f))) + Level * 0.5f);
                    darkDamage = (int)(damage * 4f);
                    break;
                case "metal":
                    metalResistance = (float)(20 / (1 + Math.Pow(Math.E, -(Level * 0.5f))));
                    fireResistance = (float)(-20 / (1 + Math.Pow(Math.E, -(Level * 0.5f))) + Level * 0.5f);
                    metalDamage = (int)(damage * 4f);
                    break;
                case "fire":
                    fireResistance = (float)(20 / (1 + Math.Pow(Math.E, -(Level * 0.5f))));
                    waterResistance = (float)(-20 / (1 + Math.Pow(Math.E, -(Level * 0.5f))) + Level * 0.5f);
                    fireDamage = (int)(damage * 4f);
                    break;
                case "air":
                    airResistance = (float)(20 / (1 + Math.Pow(Math.E, -(Level * 0.5f))));
                    earthResistance = (float)(-20 / (1 + Math.Pow(Math.E, -(Level * 0.5f))) + Level * 0.5f);
                    airDamage = (int)(damage * 4f);
                    break;
            }

            //adds damage and resistances to lists for ease of use
            #region
            ResistanceTypes.Add(earthResistance);
            ResistanceTypes.Add(waterResistance);
            ResistanceTypes.Add(darkResistance);
            ResistanceTypes.Add(metalResistance);
            ResistanceTypes.Add(fireResistance);
            ResistanceTypes.Add(airResistance);
            DamageTypes.Add(earthDamage);
            DamageTypes.Add(waterDamage);
            DamageTypes.Add(darkDamage);
            DamageTypes.Add(metalDamage);
            DamageTypes.Add(fireDamage);
            DamageTypes.Add(airDamage);
            #endregion

            WeaponSkill();
            ArmorSkill();
            Skill();
        }

        public FilledStone(Enemy enemy)
        {
            Monster = enemy.Monster;
            Level = enemy.Level;
            spriteName = $"monsters/Orbs/{Monster}";
            if (Enemy.ReturnMonsterIndex(enemy.Monster) <= 20)
            {
                sprite = GameWorld.ContentManager.Load<Texture2D>(spriteName);
            }

            //base stats
            Damage = (int)(enemy.Damage * 0.15);
            maxHealth = (int)(enemy.MaxHealth * 0.15);
            attackSpeed = (int)(enemy.AttackSpeed * 0.15f);
            Defense = (int)(enemy.Defense * 0.15f);

            //switch case to determine the element, and name of abilities, based on the monster type
            switch (Monster)
            {
                case "sheep":
                    Name = "Sheep";
                    Element = "neutral";
                    weaponName = "Headbutt";
                    armorName = "Woolen Armor";
                    skillName = "Headbutt";
                    break;
                case "bear":
                    Name = "Bear";
                    Element = "neutral";
                    weaponName = "Maul";
                    armorName = "Ursadaen Fortitude";
                    skillName = "Maul";
                    break;
                case "wolf":
                    Name = "Wolf";
                    Element = "neutral";
                    weaponName = "Wolf Bite";
                    armorName = "Wolf Revenge";
                    skillName = "Wolf Frenzy";
                    break;
                case "plantEater":
                    Name = "Plant Eater";
                    Element = "earth";
                    weaponName = "Symplastic Strike";
                    armorName = "Apoplastic Defence";
                    skillName = "Drain Life";
                    break;
                case "insectSoldier":
                    Name = "Insect Soldier";
                    Element = "earth";
                    weaponName = "Toxic Barbs";
                    armorName = "Poisonous Skin";
                    skillName = "Venomous Strike";
                    break;
                case "slimeSnake":
                    Name = "Slime Snake";
                    Element = "earth";
                    weaponName = "Corrosive Slime";
                    armorName = "Hardened Gel";
                    skillName = "Protective Goop";
                    break;
                case "tentacle":
                    Name = "Tentacle";
                    Element = "water";
                    weaponName = "Constraining Grapple";
                    armorName = "Retaliating Slap";
                    skillName = "Tentacle Grap";
                    break;
                case "frog":
                    Name = "Frog";
                    Element = "water";
                    weaponName = "Slipping Splash";
                    armorName = "Slimy Skin";
                    skillName = "Mucus Shot";
                    break;
                case "fish":
                    Name = "Fish";
                    Element = "water";
                    weaponName = "Regenerative Strike";
                    armorName = "Sudden Mending";
                    skillName = "Healing Rain";
                    break;
                case "mummy":
                    Name = "Mummy";
                    Element = "dark";
                    weaponName = "Cursing Strike";
                    armorName = "Curse Immunity";
                    skillName = "Curse of the Mummy";
                    break;
                case "vampire":
                    Name = "Vampire";
                    Element = "dark";
                    weaponName = "Vampiric Touch";
                    armorName = "Bloodied Shield";
                    skillName = "Blood Shield";
                    break;
                case "banshee":
                    Name = "Banshee";
                    Element = "dark";
                    weaponName = "Paralyzing Touch";
                    armorName = "Paralyze Immunity";
                    skillName = "Banshee's Song";
                    break;
                case "bucketMan":
                    Name = "Bucket Man";
                    Element = "metal";
                    weaponName = "Critical Bucket";
                    armorName = "Bucket Shield";
                    skillName = "ULTIMATE BUCKET DESTRUCTION!";
                    break;
                case "defender":
                    Name = "Defender";
                    Element = "metal";
                    weaponName = "Sunder Armor";
                    armorName = "Thick Plates";
                    skillName = "Defensive Stance";
                    break;
                case "sentry":
                    Name = "Sentry";
                    Element = "metal";
                    weaponName = "Accurate Strikes";
                    armorName = "Predicting Algorithm";
                    skillName = "Scan";
                    break;
                case "fireGolem":
                    Name = "Fire Golem";
                    Element = "fire";
                    weaponName = "Heavy Strikes";
                    armorName = "Obsidian Skin";
                    skillName = "Momentous Slam";
                    break;
                case "infernalDemon":
                    Name = "Infernal Demon";
                    Element = "fire";
                    weaponName = "Red-hot Smite";
                    armorName = "Immolating Presence";
                    skillName = "Incenerate";
                    break;
                case "ashZombie":
                    Name = "Ash Zombie";
                    Element = "fire";
                    weaponName = "Fiery Double-tap";
                    armorName = "Blazing Assault";
                    skillName = "Flaring Assailment";
                    break;
                case "falcon":
                    Name = "Falcon";
                    Element = "air";
                    weaponName = "Speedy Swoop";
                    armorName = "Evasive Wing-work";
                    skillName = "Quickened Agility";
                    break;
                case "bat":
                    Name = "Bat";
                    Element = "air";
                    weaponName = "Confusing Menoeuvre";
                    armorName = "Perplexing Retaliation";
                    skillName = "Sonic Scream";
                    break;
                case "raven":
                    Name = "Raven";
                    Element = "air";
                    weaponName = "Blinding Assault";
                    armorName = "Retaliative Amaurotic";
                    skillName = "Blind";
                    break;
            }

            //adds damage and resistances to lists for ease of use
            for (int i = 0; i < enemy.ResistanceTypes.Count; i++)
            {
                ResistanceTypes.Add(enemy.ResistanceTypes[i] * 0.15f);
                DamageTypes.Add((int)(enemy.DamageTypes[i] * 0.15f));
            }

            WeaponSkill();
            ArmorSkill();
            Skill();
        }

        public void WeaponSkill()
        {
            WeaponEffect = new Effect(Enemy.ReturnMonsterIndex(Monster), "Weapon", this, null, 0);
        }

        public void ArmorSkill()
        {
            ArmorEffect = new Effect(Enemy.ReturnMonsterIndex(Monster), "Armor", this, null, 0);
        }

        public void Skill()
        {
            SkillEffect = new Effect(Enemy.ReturnMonsterIndex(Monster), "Skill", this, null, 0);
        }

        public static void CatchMonster(Enemy target)
        {
            int tempLevel = (int)(target.Level * 0.5);
            if (tempLevel == 0)
            {
                tempLevel = 1;
            }
            stoneList.Add(new FilledStone(target));
        }
    }
}
