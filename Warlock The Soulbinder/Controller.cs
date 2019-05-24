using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    class Controller
    {
        ModelConsumable consumable;
        ModelSoulStone filledStone;
        ModelEnemy enemy;
        ModelNPC npc;
        ModelQuest quest;
        ModelPlayer player;
        ModelStatistic statistic;
        Model model;

        
        public string CurrentSaveFile { get ; set ; }

        static Controller instance;
        static public Controller Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Controller();
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }
                

        public Controller()
        {
            consumable = new ModelConsumable();
            filledStone = new ModelSoulStone();
            enemy = new ModelEnemy();
            model = new Model();
            player = new ModelPlayer();
            statistic = new ModelStatistic();
        }

        #region Model
        public void OpenTheGates()
        {
            model.OpenConnection();
        }

        public void CloseTheGates()
        {
            model.CloseConnection();
        }

        #endregion

        #region FilledStone
        public void DeleteSoulStoneDB()
        {
            filledStone.ClearDB(CurrentSaveFile);
        }

        public void SaveToSoulStoneDB(string monster, int level)
        {
            filledStone.SaveSoulStone(CurrentSaveFile, monster, level);
        }

        public Dictionary<int, FilledStone> LoadFromFilledStoneDB()
        {
            return filledStone.LoadSoulStone(CurrentSaveFile);
        }
        #endregion


        #region Consumable
        public void DeleteConsumableDB()
        {
            consumable.ClearDB(CurrentSaveFile);
        }

        public void SaveToConsumableDB(string name, int amount)
        {
            consumable.SaveConsumable(CurrentSaveFile, name, amount);
        }

        public Dictionary<int, Consumable> LoadFromConsumableDB()
        {
            return consumable.LoadConsumable(CurrentSaveFile);
        }
        #endregion


        #region Statistic
        public void DeleteStatisticDB()
        {
            statistic.ClearDB(CurrentSaveFile);
        }

        public void SaveToStatisticDB(int gold, int soulCount)
        {
            statistic.SaveStatistic(CurrentSaveFile, gold, soulCount);
        }

        public void LoadFromStatisticDB()
        {
            statistic.LoadStatistic(CurrentSaveFile);
        }

        #endregion


        #region Quest


        #endregion


        #region Player
        public void DeletePlayerDB()
        {
            player.ClearDB(CurrentSaveFile);
        }

        public void SaveToPlayerDB(int X, int Y, string zone, int soulWeapon, int soulArmour, int soulTrinket1, int soulTrinket2, int soulTrinket3)
        {
            player.SavePlayer(CurrentSaveFile, X, Y, zone, soulWeapon, soulArmour, soulTrinket1, soulTrinket2, soulTrinket3);
        }

        public void LoadFromPlayerDB()
        {
            player.LoadPlayer(CurrentSaveFile);
        }

        #endregion
        

        #region NPC


        #endregion


        #region Enemy


        #endregion
    }
}
