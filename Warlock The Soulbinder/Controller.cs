using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    class Controller
    {
        Model model = new Model();
        ModelSoulStone filledStone = new ModelSoulStone();
        ModelEnemy enemy = new ModelEnemy();
        ModelPlayer player = new ModelPlayer();
        ModelLog log = new ModelLog();
        ModelStatistic statistic = new ModelStatistic();
        
        
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

        /// <summary>
        /// Tumbleweed.
        /// </summary>
        public Controller()
        {       
        }
        
        #region Model
        /// <summary>
        /// Calls a method that opens a connection to the database.
        /// </summary>
        public void OpenTheGates()
        {
            model.OpenConnection();
        }

        /// <summary>
        /// Calls a method that closes the connection to the database.
        /// </summary>
        public void CloseTheGates()
        {
            model.CloseConnection();
        }
        #endregion

        #region FilledStone
        /// <summary>
        /// Calls a method that clears the SoulStone table in current savefile.
        /// </summary>
        public void DeleteSoulStoneDB()
        {
            filledStone.ClearDB();
        }

        /// <summary>
        /// Calls a method that saves the randomly generated numbers so that they aren't random every time you reload a saved game.
        /// </summary>
        /// <param name="monster">Name of the monster.</param>
        /// <param name="experience">Current experience gathered on that stone.</param>
        /// <param name="equipmentSlot">Name of the equipment slot that it's currently in, if any.</param>
        /// <param name="level">Level of the soulstone.</param>
        /// <param name="damage">Damage bonus stat of the soulstone.</param>
        /// <param name="maxHealth">max health bonus of the soulstone.</param>
        /// <param name="attackSpeed">attackspeed of the soulstone.</param>
        public void SaveToSoulStoneDB(string monster, int experience, string equipmentSlot, int level, int damage, int maxHealth, float attackSpeed)
        {
            filledStone.SaveSoulStone(monster, experience, equipmentSlot, level, damage, maxHealth, attackSpeed);
        }

        /// <summary>
        /// Calls a method that returns a list of soulstones via the SoulStone table and a special constructor in the FilledStones class.
        /// </summary>
        /// <returns></returns>
        public List<FilledStone> LoadFromFilledStoneDB()
        {
            return filledStone.LoadSoulStone();
        }
        #endregion

        #region Statistic
        /// <summary>
        /// Calls a method that clears the Statistic table in current savefile.
        /// </summary>
        public void DeleteStatisticDB()
        {
            statistic.ClearDB();
        }
        /// <summary>
        /// Calls a method that saves the progress on killing the seven different dragons to the Statistic table.
        /// </summary>
        /// <param name="earthDragonDead">Is it dead yet?</param>
        /// <param name="fireDragonDead">Is it dead yet?</param>
        /// <param name="darkDragonDead">Is it dead yet?</param>
        /// <param name="metalDragonDead">Is it dead yet?</param>
        /// <param name="waterDragonDead">Is it dead yet?</param>
        /// <param name="airDragonDead">Is it dead yet?</param>
        /// <param name="neutralDragonDead">Is it dead yet?</param>
        public void SaveToStatisticDB(bool earthDragonDead, bool fireDragonDead, bool darkDragonDead, bool metalDragonDead, bool waterDragonDead, bool airDragonDead, bool neutralDragonDead)
        {
            statistic.SaveStatistic(earthDragonDead, fireDragonDead, darkDragonDead, metalDragonDead, waterDragonDead, airDragonDead, neutralDragonDead);
        }
        /// <summary>
        /// Calls a method that loads the saved progress on killing the seven different dragons from the Statistic table.
        /// </summary>
        public void LoadFromStatisticDB()
        {
            statistic.LoadStatistic();
        }
        #endregion

        #region Log
        /// <summary>
        /// Calls a method that clears the Enemy table in current savefile.
        /// </summary>
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

        /// <summary>
        /// Calls a method that loads the progress of scanning for each creature from the Log database.
        /// </summary>
        public void LoadFromLogDB()
        {
           log.LoadLog();
        }
        #endregion

        #region Player
        /// <summary>
        /// Calls a method that clears the Player table in current savefile.
        /// </summary>
        public void DeletePlayerDB()
        {
            player.ClearDB();
        }

        /// <summary>
        /// Calls a method that saves all the parameters below to the Player database.
        /// </summary>
        /// <param name="X">X-coordinate of the player in the overworld.</param>
        /// <param name="Y">Y-coordinate of the player in the overworld.</param>
        /// <param name="zone">Name of the current zone.</param>
        /// <param name="currentHealth">Current health of the player.</param>
        public void SaveToPlayerDB(float X, float Y, string zone, int currentHealth)
        {
            player.SavePlayer(X, Y, zone, currentHealth);
        }

        /// <summary>
        /// Calls a method that sets the Player's current health and position in current zone by loading them from the Player database.
        /// </summary>
        public void LoadFromPlayerDB()
        {
            player.LoadPlayer();
        }
        #endregion

        #region Enemy
        /// <summary>
        /// Calls a method that clears the Enemy table in current savefile.
        /// </summary>
        public void DeleteEnemyDB()
        {
            enemy.ClearDB();
        }

        /// <summary>
        /// Calls a method that saves all the parameters below to the Enemy database.
        /// </summary>
        /// <param name="level">Level of the enemy.</param>
        /// <param name="X">X-coordiante of the enemy in the overworld.</param>
        /// <param name="Y">Y-coordiante of the enemy in the overworld.</param>
        /// <param name="defense">Defense stat of the enemy.</param>
        /// <param name="damage">Damage stat of the enemy.</param>
        /// <param name="maxHealth">Max health of the enemy.</param>
        /// <param name="attackSpeed">Attack speed of the enemy.</param>
        /// <param name="metalResistance">Metal resistance of the enemy.</param>
        /// <param name="earthResistance">Earth resistance of the enemy.</param>
        /// <param name="airResistance">Air resistance of the enemy.</param>
        /// <param name="fireResistance">Fire resistance of the enemy.</param>
        /// <param name="darkResistance">Dark resistance of the enemy.</param>
        /// <param name="waterResistance">Water resistance of the enemy.</param>
        /// <param name="monster">Name of the monster.</param>
        public void SaveToEnemyDB(int level, float X, float Y, int defense, int damage, int maxHealth, float attackSpeed, float metalResistance, float earthResistance, float airResistance, float fireResistance, float darkResistance, float waterResistance, string monster)
        {
            enemy.SaveEnemy(level, X, Y, defense, damage, maxHealth, attackSpeed, metalResistance, earthResistance, airResistance, fireResistance, darkResistance, waterResistance, monster);
        }

        /// <summary>
        /// Calls a method that loads all enemies from the database and returns it as a list.
        /// </summary>
        /// <returns>A list of enemies, if any, from the databse</returns>
        public List<Enemy> LoadFromEnemyDB()
        {
            return enemy.LoadEnemy();
        }
        #endregion
    }
}