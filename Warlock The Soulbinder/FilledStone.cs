using Microsoft.Xna.Framework.Graphics;
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
        #region VARIABLES
        private string monster;
        private string element;
        private int level;
        private int experience;
        private int experienceRequired = 13;
        private bool equipped;
        private string equipmentSlot;
        private static int stoneListPages;
        private string weaponName;
        private string armorName;
        private string skillName;
        private int internalCooldown;
        private int experienceLastEncounter;
        private Effect weaponEffect;
        private Effect dragonWeaponEffect1;
        private Effect dragonWeaponEffect2;
        private Effect dragonWeaponEffect3;
        private List<Effect> dragonWeaponEffects = new List<Effect>();
        private Effect armorEffect;
        private Effect dragonArmorEffect1;
        private Effect dragonArmorEffect2;
        private Effect dragonArmorEffect3;
        private List<Effect> dragonArmorEffects = new List<Effect>();
        private Effect skillEffect;

        private int maxHealth;
        protected float attackSpeed;
        private int totalDamage;
        protected int damage;
        protected int earthDamage;
        protected int waterDamage;
        protected int darkDamage;
        protected int metalDamage;
        protected int fireDamage;
        protected int airDamage;
        protected int defense;
        protected float earthResistance;
        protected float waterResistance;
        protected float darkResistance;
        protected float metalResistance;
        protected float fireResistance;
        protected float airResistance;
        protected List<int> damageTypes = new List<int>();
        protected List<float> resistanceTypes = new List<float>();
        #endregion
        #region PROPERTIES
        public string WeaponName { get => weaponName; set => weaponName = value; }
        public string ArmorName { get => armorName; set => armorName = value; }
        public string SkillName { get => skillName; set => skillName = value; }
        public int ExperienceRequired { get => experienceRequired; set => experienceRequired = value; }
        public Effect WeaponEffect { get => weaponEffect; set => weaponEffect = value; }
        public Effect ArmorEffect { get => armorEffect; set => armorEffect = value; }
        public Effect SkillEffect { get => skillEffect; set => skillEffect = value; }
        public int Damage { get => damage; set => damage = value; }
        public int Defense { get => defense; set => defense = value; }
        public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
        public List<int> DamageTypes { get => damageTypes; set => damageTypes = value; }
        public List<float> ResistanceTypes { get => resistanceTypes; set => resistanceTypes = value; }
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        public int InternalCooldown { get => internalCooldown; set => internalCooldown = value; }
        public string SpriteName { get => spriteName; set => spriteName = value; }
        public string Monster { get => monster; set => monster = value; }
        public string Element { get => element; set => element = value; }
        public int Experience { get => experience; set => experience = value; }
        public bool Equipped { get => equipped; set => equipped = value; }
        public string EquipmentSlot { get => equipmentSlot; set => equipmentSlot = value; }
        public static int StoneListPages { get => stoneListPages; set => stoneListPages = value; }
        public int ExperienceLastEncounter { get => experienceLastEncounter; set => experienceLastEncounter = value; }
        internal Effect DragonWeaponEffect1 { get => dragonWeaponEffect1; set => dragonWeaponEffect1 = value; }
        internal Effect DragonWeaponEffect2 { get => dragonWeaponEffect2; set => dragonWeaponEffect2 = value; }
        internal Effect DragonWeaponEffect3 { get => dragonWeaponEffect3; set => dragonWeaponEffect3 = value; }
        internal Effect DragonArmorEffect1 { get => dragonArmorEffect1; set => dragonArmorEffect1 = value; }
        internal Effect DragonArmorEffect2 { get => dragonArmorEffect2; set => dragonArmorEffect2 = value; }
        internal Effect DragonArmorEffect3 { get => dragonArmorEffect3; set => dragonArmorEffect3 = value; }
        internal List<Effect> DragonWeaponEffects { get => dragonWeaponEffects; set => dragonWeaponEffects = value; }
        internal List<Effect> DragonArmorEffects { get => dragonArmorEffects; set => dragonArmorEffects = value; }
        private static List<FilledStone> stoneList = new List<FilledStone>();
        #endregion
        public int Level
        {
            get => level;
            set 
            {
                level = value;
                BaseStats();
            }
        }

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

        public int TotalDamage { get => totalDamage; set => totalDamage = value; }


        /// <summary>
        /// Constructor for loading in from a saved game
        /// </summary>
        /// <param name="monster"></param>
        /// <param name="experience"></param>
        /// <param name="level"></param>
        public FilledStone(string monster, int experience, string equipmentSlot, int level, int damage, int maxHealth, float attackSpeed)
        {
            EquipmentSlot = equipmentSlot;
            
            Experience = experience;
            Monster = monster;
            spriteName = $"monsters/Orbs/{Monster}";
            sprite = GameWorld.ContentManager.Load<Texture2D>(spriteName);

            Damage = damage;
            MaxHealth = maxHealth;
            AttackSpeed = attackSpeed;

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
                    weaponName = "Confusing Maneuver";
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

            Level = level;

            WeaponSkill();
            ArmorSkill();
            Skill();
        }

        public FilledStone(Enemy enemy)
        {
            Monster = enemy.Monster;

            //switch case to determine the element, and name of abilities, based on the monster type, directly add effects to dragons
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
                    armorName = "Apoplastic Defense";
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
                //dragons
                case "neutralDragon":
                    Element = "neutral";
                    dragonWeaponEffect1 = new Effect(0, "Weapon", this, null, 0);
                    dragonWeaponEffect2 = new Effect(1, "Weapon", this, null, 0);
                    dragonWeaponEffect3 = new Effect(2, "Weapon", this, null, 0);
                    dragonArmorEffect1 = new Effect(0, "Armor", this, null, 0);
                    dragonArmorEffect2 = new Effect(1, "Armor", this, null, 0);
                    dragonArmorEffect3 = new Effect(2, "Armor", this, null, 0);
                    dragonWeaponEffects.Add(DragonWeaponEffect1);
                    dragonWeaponEffects.Add(DragonWeaponEffect2);
                    dragonWeaponEffects.Add(DragonWeaponEffect3);
                    dragonArmorEffects.Add(DragonArmorEffect1);
                    dragonArmorEffects.Add(DragonArmorEffect2);
                    dragonArmorEffects.Add(DragonArmorEffect3);
                    break;
                case "earthDragon":
                    Element = "earth";
                    dragonWeaponEffect1 = new Effect(3, "Weapon", this, null, 0);
                    dragonWeaponEffect2 = new Effect(4, "Weapon", this, null, 0);
                    dragonWeaponEffect3 = new Effect(5, "Weapon", this, null, 0);
                    dragonArmorEffect1 = new Effect(3, "Armor", this, null, 0);
                    dragonArmorEffect2 = new Effect(4, "Armor", this, null, 0);
                    dragonArmorEffect3 = new Effect(5, "Armor", this, null, 0);
                    dragonWeaponEffects.Add(DragonWeaponEffect1);
                    dragonWeaponEffects.Add(DragonWeaponEffect2);
                    dragonWeaponEffects.Add(DragonWeaponEffect3);
                    dragonArmorEffects.Add(DragonArmorEffect1);
                    dragonArmorEffects.Add(DragonArmorEffect2);
                    dragonArmorEffects.Add(DragonArmorEffect3);
                    break;
                case "waterDragon":
                    Element = "water";
                    dragonWeaponEffect1 = new Effect(6, "Weapon", this, null, 0);
                    dragonWeaponEffect2 = new Effect(7, "Weapon", this, null, 0);
                    dragonWeaponEffect3 = new Effect(8, "Weapon", this, null, 0);
                    dragonArmorEffect1 = new Effect(6, "Armor", this, null, 0);
                    dragonArmorEffect2 = new Effect(7, "Armor", this, null, 0);
                    dragonArmorEffect3 = new Effect(8, "Armor", this, null, 0);
                    dragonWeaponEffects.Add(DragonWeaponEffect1);
                    dragonWeaponEffects.Add(DragonWeaponEffect2);
                    dragonWeaponEffects.Add(DragonWeaponEffect3);
                    dragonArmorEffects.Add(DragonArmorEffect1);
                    dragonArmorEffects.Add(DragonArmorEffect2);
                    dragonArmorEffects.Add(DragonArmorEffect3);
                    break;
                case "darkDragon":
                    Element = "dark";
                    dragonWeaponEffect1 = new Effect(9, "Weapon", this, null, 0);
                    dragonWeaponEffect2 = new Effect(10, "Weapon", this, null, 0);
                    dragonWeaponEffect3 = new Effect(11, "Weapon", this, null, 0);
                    dragonArmorEffect1 = new Effect(9, "Armor", this, null, 0);
                    dragonArmorEffect2 = new Effect(10, "Armor", this, null, 0);
                    dragonArmorEffect3 = new Effect(11, "Armor", this, null, 0);
                    dragonWeaponEffects.Add(DragonWeaponEffect1);
                    dragonWeaponEffects.Add(DragonWeaponEffect2);
                    dragonWeaponEffects.Add(DragonWeaponEffect3);
                    dragonArmorEffects.Add(DragonArmorEffect1);
                    dragonArmorEffects.Add(DragonArmorEffect2);
                    dragonArmorEffects.Add(DragonArmorEffect3);
                    break;
                case "metalDragon":
                    Element = "metal";
                    dragonWeaponEffect1 = new Effect(12, "Weapon", this, null, 0);
                    dragonWeaponEffect2 = new Effect(13, "Weapon", this, null, 0);
                    dragonWeaponEffect3 = new Effect(14, "Weapon", this, null, 0);
                    dragonArmorEffect1 = new Effect(12, "Armor", this, null, 0);
                    dragonArmorEffect2 = new Effect(13, "Armor", this, null, 0);
                    dragonArmorEffect3 = new Effect(14, "Armor", this, null, 0);
                    dragonWeaponEffects.Add(DragonWeaponEffect1);
                    dragonWeaponEffects.Add(DragonWeaponEffect2);
                    dragonWeaponEffects.Add(DragonWeaponEffect3);
                    dragonArmorEffects.Add(DragonArmorEffect1);
                    dragonArmorEffects.Add(DragonArmorEffect2);
                    dragonArmorEffects.Add(DragonArmorEffect3);
                    break;
                case "fireDragon":
                    Element = "fire";
                    dragonWeaponEffect1 = new Effect(15, "Weapon", this, null, 0);
                    dragonWeaponEffect2 = new Effect(16, "Weapon", this, null, 0);
                    dragonWeaponEffect3 = new Effect(17, "Weapon", this, null, 0);
                    dragonArmorEffect1 = new Effect(15, "Armor", this, null, 0);
                    dragonArmorEffect2 = new Effect(16, "Armor", this, null, 0);
                    dragonArmorEffect3 = new Effect(17, "Armor", this, null, 0);
                    dragonWeaponEffects.Add(DragonWeaponEffect1);
                    dragonWeaponEffects.Add(DragonWeaponEffect2);
                    dragonWeaponEffects.Add(DragonWeaponEffect3);
                    dragonArmorEffects.Add(DragonArmorEffect1);
                    dragonArmorEffects.Add(DragonArmorEffect2);
                    dragonArmorEffects.Add(DragonArmorEffect3);
                    break;
                case "airDragon":
                    Element = "air";
                    dragonWeaponEffect1 = new Effect(18, "Weapon", this, null, 0);
                    dragonWeaponEffect2 = new Effect(19, "Weapon", this, null, 0);
                    dragonWeaponEffect3 = new Effect(20, "Weapon", this, null, 0);
                    dragonArmorEffect1 = new Effect(18, "Armor", this, null, 0);
                    dragonArmorEffect2 = new Effect(19, "Armor", this, null, 0);
                    dragonArmorEffect3 = new Effect(20, "Armor", this, null, 0);
                    dragonWeaponEffects.Add(DragonWeaponEffect1);
                    dragonWeaponEffects.Add(DragonWeaponEffect2);
                    dragonWeaponEffects.Add(DragonWeaponEffect3);
                    dragonArmorEffects.Add(DragonArmorEffect1);
                    dragonArmorEffects.Add(DragonArmorEffect2);
                    dragonArmorEffects.Add(DragonArmorEffect3);
                    break;
            }

            Level = (int)Math.Round(enemy.Level * 0.5);
            if (Level == 0)
            {
                Level = 1;
            }

            try
            {
                spriteName = $"monsters/Orbs/{Monster}";
            }
            catch
            {
                spriteName = "monsters/Orbs/blankSoulGem";
            }
            
            if (Enemy.ReturnMonsterIndex(enemy.Monster) <= 20)
            {
                sprite = GameWorld.ContentManager.Load<Texture2D>(spriteName);
            }
            
            if (!enemy.Dragon)
            {
                WeaponSkill();
                ArmorSkill();
                Skill();
            }
        }

        /// <summary>
        /// Sets base stats according to the stone's level
        /// </summary>
        public void BaseStats()
        {
            float modifier = 0.2f;

            //base stats
            Damage = (int)Math.Round((Level + 2.5) * 5 * modifier);
            maxHealth = (int)Math.Round((Level + 3) * 10 * modifier);
            attackSpeed = (float)((Level + 5.5) * 3 * modifier);
            Defense = (int)Math.Round((Level + 2.5f) * 0.5f * modifier);

            earthResistance = (float)Math.Log(10 * (Level * 0.3f) + 3.5);
            waterResistance = (float)Math.Log(10 * (Level * 0.3f) + 3.5);
            darkResistance = (float)Math.Log(10 * (Level * 0.3f) + 3.5);
            metalResistance = (float)Math.Log(10 * (Level * 0.3f) + 3.5);
            fireResistance = (float)Math.Log(10 * (Level * 0.3f) + 3.5);
            airResistance = (float)Math.Log(10 * (Level * 0.3f) + 3.5);

            int bounds = 15;

            switch (Element)
            {
                case "neutral":
                    Defense = (int)Math.Round(Defense * (Level * 0.75f) * modifier);
                    earthResistance *= 2;
                    waterResistance *= 2;
                    darkResistance *= 2;
                    metalResistance *= 2;
                    fireResistance *= 2;
                    airResistance *= 2;
                    break;
                case "earth":
                    earthResistance *= (float)(bounds / (1 + Math.Pow(Math.E, -(Level * 0.5f))));
                    darkResistance = (float)(darkResistance * (-bounds / (1 + Math.Pow(Math.E, -(Level * 0.5f)))) + Level * 0.5f);
                    earthDamage = (int)Math.Round(damage * 1.8f);
                    damage = (int)Math.Round(damage * 0.2f);
                    break;
                case "water":
                    waterResistance *= (float)(bounds / (1 + Math.Pow(Math.E, -(Level * 0.5f))));
                    airResistance = (float)(airResistance * (-bounds / (1 + Math.Pow(Math.E, -(Level * 0.5f)))) + Level * 0.5f);
                    waterDamage = (int)Math.Round(damage * 1.8f);
                    damage = (int)Math.Round(damage * 0.2f);
                    break;
                case "dark":
                    darkResistance *= (float)(bounds / (1 + Math.Pow(Math.E, -(Level * 0.5f))));
                    metalResistance = (float)(metalResistance * (-bounds / (1 + Math.Pow(Math.E, -(Level * 0.5f)))) + Level * 0.5f);
                    darkDamage = (int)Math.Round(damage * 1.8f);
                    damage = (int)Math.Round(damage * 0.2f);
                    break;
                case "metal":
                    metalResistance *= (float)(bounds / (1 + Math.Pow(Math.E, -(Level * 0.5f))));
                    fireResistance = (float)(fireResistance * (-bounds / (1 + Math.Pow(Math.E, -(Level * 0.5f)))) + Level * 0.5f);
                    earthDamage = (int)Math.Round(damage * 1.8f);
                    damage = (int)Math.Round(damage * 0.2f);
                    break;
                case "fire":
                    fireResistance *= (float)(bounds / (1 + Math.Pow(Math.E, -(Level * 0.5f))));
                    waterResistance = (float)(waterResistance * (-bounds / (1 + Math.Pow(Math.E, -(Level * 0.5f)))) + Level * 0.5f);
                    fireDamage = (int)Math.Round(damage * 1.8f);
                    damage = (int)Math.Round(damage * 0.2f);
                    break;
                case "air":
                    airResistance *= (float)(bounds / (1 + Math.Pow(Math.E, -(Level * 0.5f))));
                    earthResistance = (float)(earthResistance * (-bounds / (1 + Math.Pow(Math.E, -(Level * 0.5f)))) + Level * 0.5f);
                    airDamage = (int)Math.Round(damage * 1.8f);
                    damage = (int)Math.Round(damage * 0.2f);
                    break;
            }

            TotalDamage = damage + earthDamage + waterDamage + darkDamage + metalDamage + fireDamage + airDamage;

            ResistanceTypes.Clear();
            DamageTypes.Clear();

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

            if (Equipped)
            {
                Player.Instance.UpdateStats();
            }
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
            stoneList.Add(new FilledStone(target));
        }
    }
}