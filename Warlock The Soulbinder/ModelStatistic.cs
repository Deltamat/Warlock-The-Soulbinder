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
            string sqlexp = $"CREATE TABLE IF NOT EXISTS Statistic{Controller.Instance.CurrentSaveFile} (gold integer primary key, " +
                "soulCount integer )";
            cmd = connection.CreateCommand();
            cmd.CommandText = sqlexp;
            cmd.ExecuteNonQuery();
        }

        public void ClearDB(string selectedSaveFile)
        {
            cmd.CommandText = $"DELETE FROM Statistic{selectedSaveFile}";
            cmd.ExecuteNonQuery();
        }

        public void SaveStatistic(string selectedSaveFile, int gold, int soulCount)
        {
            cmd.CommandText = $"INSERT INTO Statistic{selectedSaveFile} (gold, soulCount) VALUES ({gold}, {soulCount})";
            cmd.ExecuteNonQuery();
        }

        public void LoadStatistic(string selectedSaveFile)
        {
            cmd.CommandText = $"SELECT * FROM Statistic{selectedSaveFile}";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                GameWorld.Instance.Gold = reader.GetInt32(0);
                GameWorld.Instance.SoulCount = reader.GetInt32(1);
            }
            reader.Close();
            
        }
    }
}
