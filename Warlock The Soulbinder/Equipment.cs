using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    class Equipment : Menu
    {
        private static Equipment instance;
        private FilledStone weapon;
        private FilledStone armor;
        private FilledStone skill1;
        private FilledStone skill2;
        private FilledStone skill3;
        private List<FilledStone> equippedEquipment = new List<FilledStone>();

        public static Equipment Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Equipment();
                }
                return instance;
            }
        }

        public FilledStone Weapon { get => weapon; set => weapon = value; }
        public FilledStone Armor { get => armor; set => armor = value; }
        public FilledStone Skill1 { get => skill1; set => skill1 = value; }
        public FilledStone Skill2 { get => skill2; set => skill2 = value; }
        public FilledStone Skill3 { get => skill3; set => skill3 = value; }
        public List<FilledStone> EquippedEquipment
        {
            get => equippedEquipment;
            set
            {
                equippedEquipment = value;
            }
        }

        private Equipment()
        {
            EquippedEquipment.Add(Weapon);
            EquippedEquipment.Add(Armor);
            EquippedEquipment.Add(Skill1);
            EquippedEquipment.Add(Skill2);
            EquippedEquipment.Add(Skill3);
        }

        /// <summary>
        /// Equip a stone to a slot, 0: weapon, 1: armor, 2: skill1, 3: skill2, 4: skill5
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="selectedStone"></param>
        public void EquipStone(int slot, FilledStone selectedStone)
        {
            switch (slot)
            {
                case 0:
                    Weapon = selectedStone;
                    EquippedEquipment[0] = Weapon;
                    break;
                case 1:
                    Armor = selectedStone;
                    EquippedEquipment[1] = Armor;
                    break;
                case 2:
                    Skill1 = selectedStone;
                    EquippedEquipment[2] = Skill1;
                    break;
                case 3:
                    Skill2 = selectedStone;
                    EquippedEquipment[3] = Skill2;
                    break;
                case 4:
                    Skill3 = selectedStone;
                    EquippedEquipment[4] = Skill3;
                    break;
            }
            Player.Instance.UpdateStats();
        }
    }
}
