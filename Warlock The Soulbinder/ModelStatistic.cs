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
        /// <summary>
        /// Creates a table for saving which dragons are dead if the table hasn't already been created.
        /// </summary>
        public ModelStatistic()
        {
            string sqlexp = "CREATE TABLE IF NOT EXISTS Statistic (earthDragonDead boolean primary key, " +
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
        /// Deletes the Statistic database to make it ready for a new save.
        /// </summary>
        public void ClearDB()
        {
            cmd.CommandText = "DELETE FROM Statistic";
            cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// Saves the status on which dragons are dead.
        /// </summary>
        /// <param name="earthDragonDead">Earth dragon dead?</param>
        /// <param name="fireDragonDead">Fire dragon dead?</param>
        /// <param name="darkDragonDead">Dark dragon dead?</param>
        /// <param name="metalDragonDead">Metal dragon dead?</param>
        /// <param name="waterDragonDead">Water dragon dead?</param>
        /// <param name="airDragonDead">Air dragon dead?</param>
        /// <param name="neutralDragonDead">Neutral dragon dead?</param>
        public void SaveStatistic(bool earthDragonDead, bool fireDragonDead, bool darkDragonDead, bool metalDragonDead, bool waterDragonDead, bool airDragonDead, bool neutralDragonDead)
        {
            cmd.CommandText = $"INSERT INTO Statistic (earthDragonDead, fireDragonDead, darkDragonDead, metalDragonDead, waterDragonDead, airDragonDead, neutralDragonDead) VALUES ({earthDragonDead}, {fireDragonDead}, {darkDragonDead}, {metalDragonDead}, {waterDragonDead}, {airDragonDead}, {neutralDragonDead})";
            cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// Loads the status on which dragons are dead.
        /// </summary>
        public void LoadStatistic()
        {
            cmd.CommandText = "SELECT * FROM Statistic";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Combat.Instance.EarthDragonDead = reader.GetBoolean(0);
                Combat.Instance.FireDragonDead = reader.GetBoolean(1);
                Combat.Instance.DarkDragonDead = reader.GetBoolean(2);
                Combat.Instance.MetalDragonDead = reader.GetBoolean(3);
                Combat.Instance.WaterDragonDead = reader.GetBoolean(4);
                Combat.Instance.AirDragonDead = reader.GetBoolean(5);
                Combat.Instance.NeutralDragonDead = reader.GetBoolean(6);
            }
            reader.Close();
        }
    }
}