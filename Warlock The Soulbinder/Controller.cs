using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    /// <summary>
    /// Controller class
    /// </summary>
    public class Controller
    {
        private Model model = new Model();
        private ModelSoulStone filledStone = new ModelSoulStone();
        private ModelEnemy enemy = new ModelEnemy();
        private ModelPlayer player = new ModelPlayer();
        private ModelLog log = new ModelLog();
        private ModelStatistic statistic = new ModelStatistic();
        
        private static Controller instance;
        /// <summary>
        /// Creates an instance for the singleton
        /// </summary>

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
                instance = new Controller();
                //instance = value;
            }
        }

        /// <summary>
        /// Creates a new Controller.
        /// </summary>
        private Controller()
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
        public void SaveToSoulStoneDB(string monster, int experience, string equipmentSlot, int level)
        {
            filledStone.SaveSoulStone(monster, experience, equipmentSlot, level);
        }

        /// <summary>
        /// Calls a method that returns a list of soulstones via the SoulStone table and a special constructor in the FilledStones class.
        /// </summary>
        /// <returns></returns>
        public List<FilledStone> LoadFromSoulStoneDB()
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
        /// <summary>
        /// Calls a method that saves all the parameters to the Log database.
        /// </summary>
        /// <param name="sheep">Progress towards scanning the sheep.</param>
        /// <param name="wolf">Progress towards scanning the wolves.</param>
        /// <param name="bear">Progress towards scanning the bears.</param>
        /// <param name="plantEater">Progress towards scanning the sheep.</param>
        /// <param name="insectSoldier">Progress towards scanning the insect soldiers.</param>
        /// <param name="slimeSnake">Progress towards scanning the slime snakes.</param>
        /// <param name="tentacle">Progress towards scanning the tentacles.</param>
        /// <param name="frog">Progress towards scanning the frogs.</param>
        /// <param name="fish">Progress towards scanning the fish.</param>
        /// <param name="mummy">Progress towards scanning the mummies.</param>
        /// <param name="vampire">Progress towards scanning the vampires.</param>
        /// <param name="banshee">Progress towards scanning the banshees.</param>
        /// <param name="bucketMan">Progress towards scanning the bucket men.</param>
        /// <param name="defender">Progress towards scanning the defenders.</param>
        /// <param name="sentry">Progress towards scanning the sentries.</param>
        /// <param name="fireGolem">Progress towards scanning the fire golems.</param>
        /// <param name="infernalDemon">Progress towards scanning the infernal demons.</param>
        /// <param name="ashZombie">Progress towards scanning the ash zombies.</param>
        /// <param name="falcon">Progress towards scanning the falcons.</param>
        /// <param name="bat">Progress towards scanning the bats.</param>
        /// <param name="raven">Progress towards scanning the ravens.</param>
        public void SaveToLogDB(int sheep, int wolf, int bear, int plantEater, int insectSoldier, int slimeSnake, int tentacle,
            int frog, int fish, int mummy, int vampire, int banshee, int bucketMan, int defender, int sentry, int fireGolem,
            int infernalDemon, int ashZombie, int falcon, int bat, int raven)
        {
            log.SaveLog(sheep, wolf, bear, plantEater, insectSoldier, slimeSnake, tentacle, frog, fish, mummy, vampire, banshee, bucketMan, defender, sentry, fireGolem, infernalDemon, ashZombie, falcon, bat, raven);
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