using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    class ModelConsumable : Model
    {
        /// <summary>
        /// Creates the columns for the table, unless the table with the specified name "Consumable" already exists.
        /// </summary>
        public ModelConsumable()
        {
            string sqlexp = "CREATE TABLE IF NOT EXISTS Consumable (id integer primary key, " +
                "spriteName string, " +
                "name string, " +
                "goldCost integer, " +
                "type string, " +
                "amount integer )";
            cmd = connection.CreateCommand();
            cmd.CommandText = sqlexp;
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Deletes the database to make it ready for a new save
        /// </summary>
        public void ClearDB(string selectedSaveFile)
        {
            
            cmd.CommandText = $"DELETE FROM Consumable{selectedSaveFile}";
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Saves all consumables to the selected save file
        /// </summary>
        /// <param name="spriteName"></param>
        /// <param name="name"></param>
        /// <param name="goldCost"></param>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        public void SaveConsumable(string selectedSaveFile, string spriteName, string name, int goldCost, string type, int amount)
        {
            cmd.CommandText = $"INSERT INTO Consumable{selectedSaveFile} (id, spriteName, name, goldCost, type, amount) VALUES (null, {spriteName}, {name}, {goldCost}, {type}, {amount})";
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Loads into a dictionary all the consumables, which then all need to be given their individual position
        /// </summary>
        /// <returns>a dictionary of all the consumables saved in the database</returns>
        public Dictionary<int, Consumable> LoadConsumable(string selectedSaveFile)
        {
            connection.Open();
            Dictionary<int, Consumable> consumableDic = new Dictionary<int, Consumable>();
            cmd.CommandText = $"SELECT FROM * Consumable{selectedSaveFile}";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                consumableDic.Add(reader.GetInt32(0), new Consumable(reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5)));
            }
            reader.Close();
            connection.Close();
            return consumableDic;
            
        }
    }
}
