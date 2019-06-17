using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    /// <summary>
    /// This model is used for saving and loading the current progress on scanning the enemies (with the Sentry's ability).
    /// </summary>
    class ModelLog : Model
    {
        /// <summary>
        /// Creates a table for saving scanning progress for each enemy type if the table hasn't already been created.
        /// </summary>
        public ModelLog()
        {
            string sqlexp = "CREATE TABLE IF NOT EXISTS Log (sheep integer primary key, " +
                "wolf integer, " +
                "bear integer, " +
                "plantEater integer, " +
                "insectSoldier integer, " +
                "slimeSnake integer, " +
                "tentacle integer, " +
                "frog integer, " +
                "fish integer, " +
                "mummy integer, " +
                "vampire integer, " +
                "banshee integer, " +
                "bucketMan integer, " +
                "defender integer, " +
                "sentry integer, " +
                "firegolem integer, " +
                "infernalDemon integer, " +
                "ashZombie integer, " +
                "falcon integer, " +
                "bat integer, " +
                "raven integer)";
            cmd = connection.CreateCommand();
            cmd.CommandText = sqlexp;
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Clears the Log database to make it ready for a new save.
        /// </summary>
        public void ClearDB()
        {
            cmd.CommandText = "DELETE FROM Log";
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Saves the progress of scanning each type of monster.
        /// </summary>
        /// <param name="sheepLog">sheep</param>
        /// <param name="wolfLog">wolf</param>
        /// <param name="bearLog">bear</param>
        /// <param name="plantEaterLog">plant eater</param>
        /// <param name="insectSoldierLog">insect soldier</param>
        /// <param name="slimeSnakeLog">slime snake</param>
        /// <param name="tentacleLog">tentacle</param>
        /// <param name="frogLog">frog</param>
        /// <param name="fishLog">fish</param>
        /// <param name="mummyLog">mummy</param>
        /// <param name="vampireLog">vampire</param>
        /// <param name="bansheeLog">banshee</param>
        /// <param name="bucketManLog">bucket man</param>
        /// <param name="defenderLog">defender</param>
        /// <param name="sentryLog">sentry</param>
        /// <param name="fireGolemLog">fire golem</param>
        /// <param name="infernalDemonLog">infernal demon</param>
        /// <param name="ashZombieLog">ash zombie</param>
        /// <param name="falconLog">falcon</param>
        /// <param name="batLog">bat</param>
        /// <param name="ravenLog">raven</param>
        public void SaveLog(int sheepLog, int wolfLog, int bearLog, int plantEaterLog, int insectSoldierLog, int slimeSnakeLog, int tentacleLog, 
            int frogLog, int fishLog, int mummyLog, int vampireLog, int bansheeLog, int bucketManLog, int defenderLog, int sentryLog, int fireGolemLog, 
            int infernalDemonLog, int ashZombieLog, int falconLog, int batLog, int ravenLog)
        {
            cmd.CommandText = $"INSERT INTO Log (sheep, wolf, bear, plantEater, insectSoldier, slimeSnake, tentacle, frog, fish, mummy, vampire, banshee, bucketMan, defender, sentry, fireGolem, infernalDemon, ashZombie, falcon, bat, raven) VALUES ({sheepLog}, {wolfLog}, {bearLog}, {plantEaterLog}, {insectSoldierLog}, {slimeSnakeLog}, {tentacleLog}, {frogLog}, {fishLog}, {mummyLog}, {vampireLog}, {bansheeLog}, {bucketManLog}, {defenderLog}, {sentryLog}, {fireGolemLog}, {infernalDemonLog}, {ashZombieLog}, {falconLog}, {batLog}, {ravenLog})";
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Loads the progress on scanning each monster type.
        /// </summary>
        public void LoadLog()
        {
            cmd.CommandText = "SELECT * FROM Log";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Log.Instance.SheepLog = reader.GetInt32(0);
                Log.Instance.WolfLog = reader.GetInt32(1);
                Log.Instance.BearLog = reader.GetInt32(2);
                Log.Instance.PlantEaterLog = reader.GetInt32(3);
                Log.Instance.InsectSoldierLog = reader.GetInt32(4);
                Log.Instance.SlimeSnakeLog = reader.GetInt32(5);
                Log.Instance.TentacleLog = reader.GetInt32(6);
                Log.Instance.FrogLog = reader.GetInt32(7);
                Log.Instance.FishLog = reader.GetInt32(8);
                Log.Instance.MummyLog = reader.GetInt32(9);
                Log.Instance.VampireLog = reader.GetInt32(10);
                Log.Instance.BansheeLog = reader.GetInt32(11);
                Log.Instance.BucketManLog = reader.GetInt32(12);
                Log.Instance.DefenderLog = reader.GetInt32(13);
                Log.Instance.SentryLog = reader.GetInt32(14);
                Log.Instance.FireGolemLog = reader.GetInt32(15);
                Log.Instance.InfernalDemonLog = reader.GetInt32(16);
                Log.Instance.AshZombieLog = reader.GetInt32(17);
                Log.Instance.FalconLog = reader.GetInt32(18);
                Log.Instance.BatLog = reader.GetInt32(19);
                Log.Instance.RavenLog = reader.GetInt32(20);
            }
            reader.Close();
        }
    }
}
