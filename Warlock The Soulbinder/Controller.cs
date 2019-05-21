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
            return consumable.LoadConsumable();
        }


        #endregion


        #region FilledStone
        public Dictionary<int, ModelSoulStone> LoadFilledStone()
        {
            //return filledStone.LoadSoulStone();
            return null;
        }


        #endregion
    }
}
