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

        }

        #region FilledStone
        public void DeleteSoulStoneDB()
        {
            filledStone.ClearDB(CurrentSaveFile);
        }

        public void SaveSoulStoneDB(string spriteName, string name, string monster, int goldCost, string type, int level)
        {
            filledStone.SaveSoulStone(CurrentSaveFile, spriteName, name, monster, goldCost, type, level);
        }

        public Dictionary<int, FilledStone> LoadFilledStoneDB()
        {
            return filledStone.LoadSoulStone(CurrentSaveFile);
        }
        #endregion


        #region Consumable
        public void DeleteConsumableDB()
        {
            consumable.ClearDB(CurrentSaveFile);
        }

        public void SaveConsumableDB(string spriteName, string name, int goldCost, string type, int amount)
        {
            consumable.SaveConsumable(CurrentSaveFile, spriteName, name, goldCost, type, amount);
        }

        public Dictionary<int, Consumable> LoadConsumableDB()
        {
            return consumable.LoadConsumable(CurrentSaveFile);
        }
        #endregion


        #region Statistic


        #endregion


        #region Quest


        #endregion


        #region Player


        #endregion


        #region Enemy


        #endregion

        
        #region NPC


        #endregion        
    }
}
