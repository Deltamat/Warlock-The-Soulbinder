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

        public FilledStone(string name, string monster, int level)
        {
            this.name = name;
            this.monster = monster;
            this.level = level;
            switch (monster) //switch case to determine the element based on the monster type
            {
                case "bear":
                case "sheep":
                case "wolf":                
                    element = "neutral";
                    break;
                case "bucketMan":
                case "defender":
                case "sentry":
                    element = "metal";
                    break;
                case "plantEater":
                case "insectSoldier":
                case "slimeSnake":
                    element = "earth";
                    break;
                case "falcon":
                case "bat":
                case "raven":
                    element = "air";
                    break;
                case "fireGolem":
                case "infernalGolem":
                case "ashZombie":
                    element = "fire";
                    break;
                case "mummy":
                case "vampire":
                case "banshee":
                    element = "dark";
                    break;
                case "tentacle":
                case "frog":
                case "fish":
                    element = "water";
                    break;
            }
        }
    }
}
