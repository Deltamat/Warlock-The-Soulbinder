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

        string currentSaveFile;

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
        }


        #region Consumable
        public Dictionary<int, Consumable> LoadConsumable()
        {
            return consumable.LoadConsumable(currentSaveFile);
        }

        public void DeleteConsumableDB()
        {
            consumable.ClearDB(currentSaveFile);
        }
        #endregion


        #region FilledStone
        public Dictionary<int, FilledStone> LoadFilledStone()
        {
            return filledStone.LoadSoulStone(currentSaveFile);
            
        }

        public void DeleteSoulStoneDB()
        {
            filledStone.ClearDB(currentSaveFile);
        }

        #endregion


        #region Player


        #endregion


        #region Enemy


        #endregion


        #region Quest


        #endregion


        #region NPC


        #endregion


        #region Statistic


        #endregion
    }
}
