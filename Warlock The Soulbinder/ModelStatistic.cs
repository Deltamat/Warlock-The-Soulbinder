using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    class ModelStatistic : Model
    {
        public ModelStatistic()
        {
            string sqlexp = "CREATE TABLE IF NOT EXISTS Statistic (gold integer primary key, " +
                "soulCount integer, " +
                "earthDragonDead boolean, " +
                "fireDragonDead boolean, " +
                "darkDragonDead boolean, " +
                "metalDragonDead boolean, " +
                "waterDragonDead boolean, " +
                "airDragonDead boolean, " +
                "neutralDragonDead boolean )";
            cmd = connection.CreateCommand();
            cmd.CommandText = sqlexp;
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Deletes the database to make it ready for a new save
        /// </summary>
        public void ClearDB()
        {
            cmd.CommandText = "DELETE FROM Statistic";
            cmd.ExecuteNonQuery();
        }

        public void SaveStatistic(int gold, int soulCount, bool earthDragonDead, bool fireDragonDead, bool darkDragonDead, bool metalDragonDead, bool waterDragonDead, bool airDragonDead, bool neutralDragonDead)
        {
            cmd.CommandText = $"INSERT INTO Statistic (gold, soulCount, earthDragonDead, fireDragonDead, darkDragonDead, metalDragonDead, waterDragonDead, airDragonDead, neutralDragonDead) VALUES ({gold}, {soulCount}, {earthDragonDead}, {fireDragonDead}, {darkDragonDead}, {metalDragonDead}, {waterDragonDead}, {airDragonDead}, {neutralDragonDead})";
            cmd.ExecuteNonQuery();
        }

        public void LoadStatistic()
        {
            cmd.CommandText = "SELECT * FROM Statistic";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                GameWorld.Instance.Gold = reader.GetInt32(0);
                GameWorld.Instance.SoulCount = reader.GetInt32(1);
                Combat.Instance.EarthDragonDead = reader.GetBoolean(2);
                Combat.Instance.FireDragonDead = reader.GetBoolean(3);
                Combat.Instance.DarkDragonDead = reader.GetBoolean(4);
                Combat.Instance.MetalDragonDead = reader.GetBoolean(5);
                Combat.Instance.WaterDragonDead = reader.GetBoolean(6);
                Combat.Instance.AirDragonDead = reader.GetBoolean(7);
                Combat.Instance.NeutralDragonDead = reader.GetBoolean(8);
            }
            reader.Close();
            
        }
    }
}
