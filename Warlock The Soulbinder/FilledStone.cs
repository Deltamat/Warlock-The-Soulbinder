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
        private string monster;
        private string name;
        private string element;
        private int level;
        private int experience = 10;
        private int experienceRequired = 23;
        private bool equipped;
        private string equipmentSlot;
        private static int stoneListPages = 0;
        private string weaponName;
        private string armorName;
        private string skillName;
        private Effect weaponEffect;
        private Effect armorEffect;
        private Effect skillEffect;
        public string SpriteName { get => spriteName; set => spriteName = value; }
        public static List<FilledStone> StoneList { get => stoneList; set => stoneList = value; }
        public string Monster { get => monster; set => monster = value; }
        public string Element { get => element; set => element = value; }
        public int Level { get => level; set => level = value; }
        public int Experience { get => experience; set => experience = value; }
        public bool Equipped { get => equipped; set => equipped = value; }
        public string EquipmentSlot { get => equipmentSlot; set => equipmentSlot = value; }
        public static int StoneListPages { get => stoneListPages; set => stoneListPages = value; }
        private int id;

        private int maxHealth;
        protected float attackSpeed;
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
        public int Experiencerequired { get => experienceRequired; set => experienceRequired = value; }
        public Effect WeaponEffect { get => weaponEffect; set => weaponEffect = value; }
        public Effect ArmorEffect { get => armorEffect; set => armorEffect = value; }
        public Effect SkillEffect { get => skillEffect; set => skillEffect = value; }
        public int Damage { get => damage; set => damage = value; }
        public int Defense { get => defense; set => defense = value; }
        public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
        public List<int> DamageTypes { get => damageTypes; set => damageTypes = value; }
        public List<float> ResistanceTypes { get => resistanceTypes; set => resistanceTypes = value; }
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }

        private static List<FilledStone> stoneList = new List<FilledStone>();

        public FilledStone(string name, string monster, int level)
        {
            this.name = name;
            this.Monster = monster;
            this.Level = level;
            spriteName = $"monsters/Orbs/{monster}";
            sprite = GameWorld.ContentManager.Load<Texture2D>(spriteName);
            //switch case to determine the element, and name of abilities, based on the monster type
            switch (monster) 
            {
                case "sheep":
                    Element = "neutral";
                    weaponName = "Headbutt";
                    armorName = "Woolen Armor";
                    skillName = "Headbutt";
                    break;
                case "bear":
                    Element = "neutral";
                    weaponName = "Maul";
                    armorName = "Ursadaen Fortitude";
                    skillName = "Maul";
                    break;
                case "wolf":
                    Element = "neutral";
                    weaponName = "Wolf Bite";
                    armorName = "Wolf Revenge";
                    skillName = "Wolf Frenzy";
                    break;
                case "plantEater":
                    Element = "earth";
                    weaponName = "Symplastic Strike";
                    armorName = "Apoplastic Defence";
                    skillName = "Drain Life";
                    break;
                case "insectSoldier":
                    Element = "earth";
                    weaponName = "Toxic Barbs";
                    armorName = "Poisonous Skin";
                    skillName = "Venomous Strike";
                    break;
                case "slimeSnake":
                    Element = "earth";
                    weaponName = "Corrosive Slime";
                    armorName = "Hardened Gel";
                    skillName = "Protective Goop";
                    break;
                case "tentacle":
                    Element = "water";
                    weaponName = "Constraining Grapple";
                    armorName = "Retaliating Slap";
                    skillName = "Tentacle Grap";
                    break;
                case "frog":
                    Element = "water";
                    weaponName = "Slipping Splash";
                    armorName = "Slimy Skin";
                    skillName = "Mucus Shot";
                    break;
                case "fish":
                    Element = "water";
                    weaponName = "Regenerative Strike";
                    armorName = "Sudden Mending";
                    skillName = "Healing Rain";
                    break;
                case "mummy":
                    Element = "dark";
                    weaponName = "Cursing Strike";
                    armorName = "Curse Immunity";
                    skillName = "Curse of the Mummy";
                    break;
                case "vampire":
                    Element = "dark";
                    weaponName = "Vampiric Touch";
                    armorName = "Bloodied Shield";
                    skillName = "Blood Shield";
                    break;
                case "banshee":
                    Element = "dark";
                    weaponName = "Paralyzing Touch";
                    armorName = "Paralyze Immunity";
                    skillName = "Banshee's Song";
                    break;
                case "bucketMan":
                    Element = "metal";
                    weaponName = "Critical Bucket";
                    armorName = "Bucket Shield";
                    skillName = "ULTIMATE BUCKET DESTRUCTION!";
                    break;
                case "defender":
                    Element = "metal";
                    weaponName = "Sunder Armor";
                    armorName = "Thick Plates";
                    skillName = "Defensive Stance";
                    break;
                case "sentry":
                    Element = "metal";
                    weaponName = "Accurate Strikes";
                    armorName = "Predicting Algorithm";
                    skillName = "Scan";
                    break;
                case "fireGolem":
                    Element = "fire";
                    weaponName = "Heavy Strikes";
                    armorName = "Obsidian Skin";
                    skillName = "Momentous Slam";
                    break;
                case "infernalGolem":
                    Element = "fire";
                    weaponName = "Red-hot Smite";
                    armorName = "Immolating Presence";
                    skillName = "Incenerate";
                    break;
                case "ashZombie":
                    Element = "fire";
                    weaponName = "Fiery Double-tap";
                    armorName = "Blazing Assault";
                    skillName = "Flaring Assailment";
                    break;
                case "falcon":
                    Element = "air";
                    weaponName = "Speedy Swoop";
                    armorName = "Evasive Wing-work";
                    skillName = "Quickened Agility";
                    break;
                case "bat":
                    Element = "air";
                    weaponName = "Confusing Menoeuvre";
                    armorName = "Perplexing Retaliation";
                    skillName = "Sonic Scream";
                    break;
                case "raven":
                    Element = "air";
                    weaponName = "Blinding Assault";
                    armorName = "Retaliative Amaurotic";
                    skillName = "Bilnd";
                    break;
            }
            WeaponSkill();
            ArmorSkill();
            Skill();
        }

        public FilledStone(string monster, int level)
        {
            Monster = monster.Monster;
            Level = level;
            spriteName = $"monsters/Orbs/{Monster}";
            sprite = GameWorld.ContentManager.Load<Texture2D>(spriteName);
            
            Damage = (int)(5 * ((Level * 0.2 + GameWorld.Instance.RandomInt(1, 5)) * 0.25f));
            maxHealth = (int)(5 * ((Level * 0.2 + GameWorld.Instance.RandomInt(1, 6)) * 1.15f));
            attackSpeed = (Level * 0.2f) + GameWorld.Instance.RandomInt(-1, 3);

            //switch case to determine the element, and name of abilities, based on the monster type
            switch (Monster) 
            {
                case "sheep":
                    name = "Sheep";
                    Element = "neutral";
                    weaponName = "Headbutt";
                    armorName = "Woolen Armor";
                    skillName = "Headbutt";
                    break;
                case "bear":
                    name = "Bear";
                    Element = "neutral";
                    weaponName = "Maul";
                    armorName = "Ursadaen Fortitude";
                    skillName = "Maul";
                    break;
                case "wolf":
                    name = "Wolf";
                    Element = "neutral";
                    weaponName = "Wolf Bite";
                    armorName = "Wolf Revenge";
                    skillName = "Wolf Frenzy";
                    break;
                case "plantEater":
                    name = "Plant Eater";
                    Element = "earth";
                    weaponName = "Symplastic Strike";
                    armorName = "Apoplastic Defence";
                    skillName = "Drain Life";
                    break;
                case "insectSoldier":
                    name = "Insect Soldier";
                    Element = "earth";
                    weaponName = "Toxic Barbs";
                    armorName = "Poisonous Skin";
                    skillName = "Venomous Strike";
                    break;
                case "slimeSnake":
                    name = "Slime Snake";
                    Element = "earth";
                    weaponName = "Corrosive Slime";
                    armorName = "Hardened Gel";
                    skillName = "Protective Goop";
                    break;
                case "tentacle":
                    name = "Tentacle";
                    Element = "water";
                    weaponName = "Constraining Grapple";
                    armorName = "Retaliating Slap";
                    skillName = "Tentacle Grap";
                    break;
                case "frog":
                    name = "Frog";
                    Element = "water";
                    weaponName = "Slipping Splash";
                    armorName = "Slimy Skin";
                    skillName = "Mucus Shot";
                    break;
                case "fish":
                    name = "Fish";
                    Element = "water";
                    weaponName = "Regenerative Strike";
                    armorName = "Sudden Mending";
                    skillName = "Healing Rain";
                    break;
                case "mummy":
                    name = "Mummy";
                    Element = "dark";
                    weaponName = "Cursing Strike";
                    armorName = "Curse Immunity";
                    skillName = "Curse of the Mummy";
                    break;
                case "vampire":
                    name = "Vampire";
                    Element = "dark";
                    weaponName = "Vampiric Touch";
                    armorName = "Bloodied Shield";
                    skillName = "Blood Shield";
                    break;
                case "banshee":
                    name = "Banshee";
                    Element = "dark";
                    weaponName = "Paralyzing Touch";
                    armorName = "Paralyze Immunity";
                    skillName = "Banshee's Song";
                    break;
                case "bucketMan":
                    name = "Bucket Man";
                    Element = "metal";
                    weaponName = "Critical Bucket";
                    armorName = "Bucket Shield";
                    skillName = "ULTIMATE BUCKET DESTRUCTION!";
                    break;
                case "defender":
                    name = "Defender";
                    Element = "metal";
                    weaponName = "Sunder Armor";
                    armorName = "Thick Plates";
                    skillName = "Defensive Stance";
                    break;
                case "sentry":
                    name = "Sentry";
                    Element = "metal";
                    weaponName = "Accurate Strikes";
                    armorName = "Predicting Algorithm";
                    skillName = "Scan";
                    break;
                case "fireGolem":
                    name = "Fire Golem";
                    Element = "fire";
                    weaponName = "Heavy Strikes";
                    armorName = "Obsidian Skin";
                    skillName = "Momentous Slam";
                    break;
                case "infernalDemon":
                    name = "Infernal Demon";
                    Element = "fire";
                    weaponName = "Red-hot Smite";
                    armorName = "Immolating Presence";
                    skillName = "Incenerate";
                    break;
                case "ashZombie":
                    name = "Ash Zombie";
                    Element = "fire";
                    weaponName = "Fiery Double-tap";
                    armorName = "Blazing Assault";
                    skillName = "Flaring Assailment";
                    break;
                case "falcon":
                    name = "Falcon";
                    Element = "air";
                    weaponName = "Speedy Swoop";
                    armorName = "Evasive Wing-work";
                    skillName = "Quickened Agility";
                    break;
                case "bat":
                    name = "Bat";
                    Element = "air";
                    weaponName = "Confusing Menoeuvre";
                    armorName = "Perplexing Retaliation";
                    skillName = "Sonic Scream";
                    break;
                case "raven":
                    name = "Raven";
                    Element = "air";
                    weaponName = "Blinding Assault";
                    armorName = "Retaliative Amaurotic";
                    skillName = "Bilnd";
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
                    earthDamage = (int)(damage * 0.8f);
                    damage = (int)(damage * 0.2f);
                    break;
                case "water":
                    waterResistance = (float)(20 / (1 + Math.Pow(Math.E, -(Level * 0.5f))));
                    airResistance = (float)(-20 / (1 + Math.Pow(Math.E, -(Level * 0.5f))) + Level * 0.5f);
                    waterDamage = (int)(damage * 0.8f);
                    damage = (int)(damage * 0.2f);
                    break;
                case "dark":
                    darkResistance = (float)(20 / (1 + Math.Pow(Math.E, -(Level * 0.5f))));
                    metalResistance = (float)(-20 / (1 + Math.Pow(Math.E, -(Level * 0.5f))) + Level * 0.5f);
                    darkDamage = (int)(damage * 0.8f);
                    damage = (int)(damage * 0.2f);
                    break;
                case "metal":
                    metalResistance = (float)(20 / (1 + Math.Pow(Math.E, -(Level * 0.5f))));
                    fireResistance = (float)(-20 / (1 + Math.Pow(Math.E, -(Level * 0.5f))) + Level * 0.5f);
                    metalDamage = (int)(damage * 0.8f);
                    damage = (int)(damage * 0.2f);
                    break;
                case "fire":
                    fireResistance = (float)(20 / (1 + Math.Pow(Math.E, -(Level * 0.5f))));
                    waterResistance = (float)(-20 / (1 + Math.Pow(Math.E, -(Level * 0.5f))) + Level * 0.5f);
                    fireDamage = (int)(damage * 0.8f);
                    damage = (int)(damage * 0.2f);
                    break;
                case "air":
                    airResistance = (float)(20 / (1 + Math.Pow(Math.E, -(Level * 0.5f))));
                    earthResistance = (float)(-20 / (1 + Math.Pow(Math.E, -(Level * 0.5f))) + Level * 0.5f);
                    airDamage = (int)(damage * 0.8f);
                    damage = (int)(damage * 0.2f);
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

        public void WeaponSkill()
        {
            WeaponEffect = new Effect(Enemy.ReturnMonsterIndex(Monster), "Weapon", this);
        }

        public void ArmorSkill()
        {
            ArmorEffect = new Effect(Enemy.ReturnMonsterIndex(Monster), "Armor", this);
        }

        public void Skill()
        {
            SkillEffect = new Effect(Enemy.ReturnMonsterIndex(Monster), "Skill", this);
        }
    }
}
