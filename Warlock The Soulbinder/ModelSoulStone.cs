using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    class ModelSoulStone : Model
    {
        public ModelSoulStone()
        {
            string sqlexp = $"CREATE TABLE IF NOT EXISTS SoulStone{Controller.Instance.CurrentSaveFile} (id integer primary key, " +
                "monster string, " +
                "level integer )";
            cmd = connection.CreateCommand();
            cmd.CommandText = sqlexp;
            cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Deletes the database to make it ready for a new save
        /// </summary>
        public void ClearDB(string selectedSaveFile)
        {
            cmd.CommandText = $"DELETE FROM SoulStone{selectedSaveFile}";
            cmd.ExecuteNonQuery();
        }

        public void SaveSoulStone(string selectedSaveFile, string monster, int level)
        {
            string whichSaveFile = $"{selectedSaveFile}";
            cmd.CommandText = $"INSERT INTO SoulStone{whichSaveFile} (id, monster, level) VALUES (null, {monster}, {level})";
        }

        public Dictionary<int, FilledStone> LoadSoulStone(string selectedSaveFile)
        {
            Dictionary<int, FilledStone> soulStoneDic = new Dictionary<int, FilledStone>();

          
            cmd.CommandText = $"SELECT * FROM SoulStone{selectedSaveFile}";
            
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                soulStoneDic.Add(reader.GetInt32(0), new FilledStone(reader.GetString(1), reader.GetInt32(2)));
            }
            reader.Close();
           
            return soulStoneDic;
        }


    }
}
