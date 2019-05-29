﻿using Microsoft.Xna.Framework;
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
                "monster string, " +
                "experience integer, " +
                "level integer )";
            cmd = connection.CreateCommand();
            cmd.CommandText = sqlexp;
            cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Deletes the database to make it ready for a new save
        /// </summary>
        public void ClearDB()
        {
            cmd.CommandText = "DELETE FROM SoulStone";
            cmd.ExecuteNonQuery();
        }

        public void SaveSoulStone(string monster, int experience, int level)
        {
            cmd.CommandText = $"INSERT INTO SoulStone (id, monster, experience, level) VALUES (null, '{monster}', {experience}, {level})";
            cmd.ExecuteNonQuery();
        }

        public List<FilledStone> LoadSoulStone()
        {
            List<FilledStone> soulStones = new List<FilledStone>();

          
            cmd.CommandText = "SELECT * FROM SoulStone";
            
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                soulStones.Add(new FilledStone(reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3)));
            }
            reader.Close();
           
            return soulStones;
        }


    }
}
