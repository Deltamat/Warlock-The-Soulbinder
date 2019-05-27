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
            filledStone.ClearDB();
        }

        public void SaveToSoulStoneDB(string monster, int level)
        {
            filledStone.SaveSoulStone(monster, level);
        }

        public Dictionary<int, FilledStone> LoadFromFilledStoneDB()
        {
            return filledStone.LoadSoulStone();
        }
        #endregion


        #region Consumable
        public void DeleteConsumableDB()
        {
            consumable.ClearDB();
        }

        public void SaveToConsumableDB(string name, int amount)
        {
            consumable.SaveConsumable(name, amount);
        }

        public Dictionary<int, Consumable> LoadFromConsumableDB()
        {
            return consumable.LoadConsumable();
        }
        #endregion


        #region Statistic
        public void DeleteStatisticDB()
        {
            statistic.ClearDB();
        }

        public void SaveToStatisticDB(int gold, int soulCount)
        {
            statistic.SaveStatistic(gold, soulCount);
        }

        public void LoadFromStatisticDB()
        {
            statistic.LoadStatistic();
        }

        #endregion


        #region Quest
        public void DeleteQuestDB()
        {
            quest.ClearDB();
        }

        public void SaveToQuestDB(string questName, string questStatus)
        {
            quest.SaveQuest(questName, questStatus);
        }

        public Dictionary<int, string> LoadFromQuestDB()
        {
            return quest.LoadQuest();
        }

        #endregion


        #region Player
        public void DeletePlayerDB()
        {
            player.ClearDB();
        }

        public void SaveToPlayerDB(int X, int Y, string zone, int soulWeapon, int soulArmour, int soulTrinket1, int soulTrinket2, int soulTrinket3)
        {
            player.SavePlayer(X, Y, zone, soulWeapon, soulArmour, soulTrinket1, soulTrinket2, soulTrinket3);
        }

        public void LoadFromPlayerDB()
        {
            player.LoadPlayer();
        }

        #endregion
      
        #region Enemy
        public void DeleteEnemyDB()
        {
            enemy.ClearDB();
        }

        public void SaveToEnemyDB(int level, int X, int Y, int defense, int damage, int maxHealth, float attackSpeed, float metalResistance, float earthResistance, float airResistance, float fireResistance, float darkResistance, float waterResistance, string monster)
        {
            enemy.SaveEnemy(level, X, Y, defense, damage, maxHealth, attackSpeed, metalResistance, earthResistance, airResistance, fireResistance, darkResistance, waterResistance, monster);
        }

        public List<Enemy> LoadFromEnemyDB()
        {
            return enemy.LoadEnemy();
        }

        #endregion
    }
}
