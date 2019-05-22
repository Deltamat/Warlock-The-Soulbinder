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
        private string element;
        private int level;
        private int experience;
        private bool equipped;
        private string equipmentSlot;
        private static int stoneListPages = 0;
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

        public int Id { get => id; private set => id = value; }

        private static List<FilledStone> stoneList = new List<FilledStone>();

        public FilledStone(string name, string monster, int level)
        {
            this.name = name;
            this.Monster = monster;
            this.Level = level;
            spriteName = $"monsters/Orbs/{monster}";
            sprite = GameWorld.ContentManager.Load<Texture2D>(spriteName);
            switch (monster) //switch case to determine the element based on the monster type
            {
                case "bear":
                    break;
                       
                case "sheep":
                case "wolf":                
                    Element = "neutral";
                    break;
                case "bucketMan":
                case "defender":
                case "sentry":
                    Element = "metal";
                    break;
                case "plantEater":
                case "insectSoldier":
                case "slimeSnake":
                    Element = "earth";
                    break;
                case "falcon":
                case "bat":
                case "raven":
                    Element = "air";
                    break;
                case "fireGolem":
                case "infernalGolem":
                case "ashZombie":
                    Element = "fire";
                    break;
                case "mummy":
                case "vampire":
                case "banshee":
                    Element = "dark";
                    break;
                case "tentacle":
                case "frog":
                case "fish":
                    Element = "water";
                    break;
            }
        }

        public void WeaponSkill()
        {

        }

        public void ArmorSkill()
        {

        }

        public void Skill()
        {

        }

    }
}
