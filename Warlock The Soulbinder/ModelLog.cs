using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    class ModelLog : Model
    {
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

        public void ClearDB()
        {
            cmd.CommandText = "DELETE FROM Log";
            cmd.ExecuteNonQuery();
        }

        public void SaveLog(int sheepLog, int wolfLog, int bearLog, int plantEaterLog, int insectSoldierLog, int slimeSnakeLog, int tentacleLog, 
            int frogLog, int fishLog, int mummyLog, int vampireLog, int bansheeLog, int bucketManLog, int defenderLog, int sentryLog, int fireGolemLog, 
            int infernalDemonLog, int ashZombieLog, int falconLog, int batLog, int ravenLog)
        {
            cmd.CommandText = $"INSERT INTO Log (sheep, wolf, bear, plantEater, insectSoldier, slimeSnake, tentacle, frog, fish, mummy, vampire, banshee, bucketMan, defender, sentry, fireGolem, infernalDemon, ashZombie, falcon, bat, raven) VALUES ({sheepLog}, {wolfLog}, {bearLog}, {plantEaterLog}, {insectSoldierLog}, {slimeSnakeLog}, {tentacleLog}, {frogLog}, {fishLog}, {mummyLog}, {vampireLog}, {bansheeLog}, {bucketManLog}, {defenderLog}, {sentryLog}, {fireGolemLog}, {infernalDemonLog}, {ashZombieLog}, {falconLog}, {batLog}, {ravenLog})";
            cmd.ExecuteNonQuery();
        }

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
