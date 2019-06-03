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
        public List<FilledStone> EquippedEquipment { get => equippedEquipment; set => equippedEquipment = value; }

        private Equipment()
        {
            EquippedEquipment.Add(Weapon);
            EquippedEquipment.Add(Armor);
            EquippedEquipment.Add(Skill1);
            EquippedEquipment.Add(Skill2);
            EquippedEquipment.Add(Skill3);
        }

        /// <summary>
        /// Equip a stone to a slot, 0: weapon, 1: armor, 2: skill1, 3: skill2, 4: skill3
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="selectedStone"></param>
        public void EquipStone(int slot, FilledStone selectedStone)
        {
            switch (slot)
            {
                case 0:
                    Weapon = selectedStone;
                    selectedStone.EquipmentSlot = "Weapon";                   
                    EquippedEquipment[slot] = Weapon;
                    break;
                case 1:
                    Armor = selectedStone;
                    selectedStone.EquipmentSlot = "Armor";
                    EquippedEquipment[slot] = Armor;
                    break;
                case 2:
                    Skill1 = selectedStone;
                    selectedStone.EquipmentSlot = "Skill1";
                    EquippedEquipment[slot] = Skill1;
                    break;
                case 3:
                    Skill2 = selectedStone;
                    selectedStone.EquipmentSlot = "Skill2";
                    EquippedEquipment[slot] = Skill2;
                    break;
                case 4:
                    Skill3 = selectedStone;
                    selectedStone.EquipmentSlot = "Skill3";
                    EquippedEquipment[slot] = Skill3;
                    break;
            }
            Player.Instance.UpdateStats();
        }

        /// <summary>
        /// Automatically divides the given experience with all equipped stones.
        /// </summary>
        /// <param name="experience"></param>
        public void ExperienceEquipment (int experience)
        {
            int stoneShare = 0;
            List<FilledStone> tempList = new List<FilledStone>();

            if (weapon != null)
            {
                stoneShare++;
                tempList.Add(weapon);
            }

            if (armor != null)
            {
                stoneShare++;
                tempList.Add(armor);
            }

            if (skill1 != null)
            {
                stoneShare++;
                tempList.Add(skill1);
            }

            if (skill2 != null)
            {
                stoneShare++;
                tempList.Add(skill2);
            }

            if (skill3 != null)
            {
                stoneShare++;
                tempList.Add(skill3);
            }

            if (stoneShare != 0)
            {
                for (int i = 0; i < tempList.Count; i++)
                {
                    tempList[i].Experience += (experience / stoneShare);

                    for (int t = 0; t < 20; t++)
                    {
                        if (tempList[i].ExperienceRequired - tempList[i].Experience <= 0)
                        {

                            tempList[i].Experience = tempList[i].Experience - tempList[i].ExperienceRequired;
                            tempList[i].Level++;
                            tempList[i].ExperienceRequired = (int)(10 * Math.Pow(1.3, tempList[i].Level));
                        }
                    }
                   
                }
            }

            tempList.Clear();
        }

        public void UpdateExperienceRequired()
        {
            foreach (FilledStone stone in FilledStone.StoneList)
            {
                stone.ExperienceRequired =  (int)(10 * Math.Pow(1.3, stone.Level));
            }
        }

        public void LoadEquipment()
        {
            foreach (FilledStone stone in FilledStone.StoneList)
            {
                if (stone.EquipmentSlot == "Weapon")
                {
                    EquipStone(0, stone);
                    stone.Equipped = true;
                }

                else if (stone.EquipmentSlot == "Armor")
                {
                    EquipStone(1, stone);
                    stone.Equipped = true;
                }

                else if (stone.EquipmentSlot == "Skill1")
                {
                    EquipStone(2, stone);
                    stone.Equipped = true;
                }

                else if (stone.EquipmentSlot == "Skill2")
                {
                    EquipStone(3, stone);
                    stone.Equipped = true;
                }

                else if (stone.EquipmentSlot == "Skill3")
                {
                    EquipStone(4, stone);
                    stone.Equipped = true;
                }

                else
                {
                    stone.Equipped = false;
                }
            }
        }
    }
}
