using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    class Controller
    {
        ModelSoulStone filledStone;
        ModelEnemy enemy;
        ModelLog log;
        ModelPlayer player;
        ModelStatistic statistic;
        Model model;
        
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
            model = new Model();
            filledStone = new ModelSoulStone();
            enemy = new ModelEnemy();
            player = new ModelPlayer();
            statistic = new ModelStatistic();
            log = new ModelLog();
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

        public void SaveToSoulStoneDB(string monster, int experience, string equipmentSlot, int level, int damage, int maxHealth, float attackSpeed)
        {
            filledStone.SaveSoulStone(monster, experience, equipmentSlot, level, damage, maxHealth, attackSpeed);
        }

        public List<FilledStone> LoadFromFilledStoneDB()
        {
            return filledStone.LoadSoulStone();
        }
        #endregion
        
        #region Statistic
        public void DeleteStatisticDB()
        {
            statistic.ClearDB();
        }

        public void SaveToStatisticDB(int gold, int soulCount, bool earthDragonDead, bool fireDragonDead, bool darkDragonDead, bool metalDragonDead, bool waterDragonDead, bool airDragonDead, bool neutralDragonDead)
        {
            statistic.SaveStatistic(gold, soulCount, earthDragonDead, fireDragonDead, darkDragonDead, metalDragonDead, waterDragonDead, airDragonDead, neutralDragonDead);
        }

        public void LoadFromStatisticDB()
        {
            statistic.LoadStatistic();
        }

        #endregion
        
        #region Log
        public void DeleteLogDB()
        {
            log.ClearDB();
        }

        public void SaveToLogDB(int sheepLog, int wolfLog, int bearLog, int plantEaterLog, int insectSoldierLog, int slimeSnakeLog, int tentacleLog,
            int frogLog, int fishLog, int mummyLog, int vampireLog, int bansheeLog, int bucketManLog, int defenderLog, int sentryLog, int fireGolemLog,
            int infernalDemonLog, int ashZombieLog, int falconLog, int batLog, int ravenLog)
        {
            log.SaveLog(sheepLog, wolfLog, bearLog, plantEaterLog, insectSoldierLog, slimeSnakeLog, tentacleLog, frogLog, fishLog, mummyLog, vampireLog, bansheeLog, bucketManLog, defenderLog, sentryLog, fireGolemLog, infernalDemonLog, ashZombieLog, falconLog, batLog, ravenLog);
        }

        public void LoadFromLogDB()
        {
           log.LoadLog();
        }

        #endregion
        
        #region Player
        public void DeletePlayerDB()
        {
            player.ClearDB();
        }

        public void SaveToPlayerDB(float X, float Y, string zone, int currentHealth, int soulWeapon, int soulArmour, int soulTrinket1, int soulTrinket2, int soulTrinket3)
        {
            player.SavePlayer(X, Y, zone, currentHealth, soulWeapon, soulArmour, soulTrinket1, soulTrinket2, soulTrinket3);
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

        public void SaveToEnemyDB(int level, float X, float Y, int defense, int damage, int maxHealth, float attackSpeed, float metalResistance, float earthResistance, float airResistance, float fireResistance, float darkResistance, float waterResistance, string monster)
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
