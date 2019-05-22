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
            string sqlexp = "CREATE TABLE IF NOT EXISTS SoulStone (id integer primary key, " +
                "spriteName string, " +                
                "name string, " +
                "monster string, " +
                "goldCost integer, " +
                "type string, " +
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
            string whichSaveFile = $"{selectedSaveFile}";
            cmd.CommandText = $"DELETE FROM SoulStone{whichSaveFile}";
            cmd.ExecuteNonQuery();
        }

        public void SaveSoulStone(string selectedSaveFile, string spriteName, string name, string monster, int goldCost, string type, int level)
        {
            string whichSaveFile = $"{selectedSaveFile}";
            cmd.CommandText = $"INSERT INTO SoulStone{whichSaveFile} (id, spriteName, name, monster, goldCost, type, level) VALUES (null, {spriteName}, {name}, {monster}, {goldCost}, {type}, {level})";
        }

        public Dictionary<int, FilledStone> LoadSoulStone(string selectedSaveFile)
        {
            string whichSaveFile = $"{selectedSaveFile}";
            Dictionary<int, FilledStone> soulStoneDic = new Dictionary<int, FilledStone>();
            connection.Open();
            cmd.CommandText = $"SELECT FROM * SoulStone{whichSaveFile}";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                soulStoneDic.Add(reader.GetInt32(0), new FilledStone(reader.GetInt32(0), reader.GetString(1), Vector2.Zero, reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetString(5), reader.GetInt32(6), GameWorld.ContentManager));
            }
            reader.Close();
            connection.Close();
            return soulStoneDic;
        }


    }
}
