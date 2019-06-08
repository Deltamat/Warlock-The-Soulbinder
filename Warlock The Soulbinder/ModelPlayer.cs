using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Microsoft.Xna.Framework;

namespace Warlock_The_Soulbinder
{
    class ModelPlayer : Model
    {
        public ModelPlayer()
        {
            string sqlexp = "CREATE TABLE IF NOT EXISTS Player (X float primary key, " +
                "Y float, " +
                "zone string, " +
                "currentHealth integer)";
            cmd = connection.CreateCommand();
            cmd.CommandText = sqlexp;
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Deletes the database to make it ready for a new save
        /// </summary>
        public void ClearDB()
        {
            cmd.CommandText = "DELETE FROM Player";
            cmd.ExecuteNonQuery();
        }

        public void SavePlayer(float X, float Y, string zone, int currentHealth)
        {
            cmd.CommandText = $"INSERT INTO Player (X, Y, zone, currentHealth) VALUES ({X.ToString(GameWorld.Instance.replaceComma)}, {Y.ToString(GameWorld.Instance.replaceComma)}, '{zone}', {currentHealth})";
            cmd.ExecuteNonQuery();
        }

        public void LoadPlayer()
        {
            cmd.CommandText = "SELECT * FROM Player";

            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Player.Instance.Position = new Vector2(reader.GetFloat(0), reader.GetFloat(1));
                GameWorld.Instance.currentZone = $"{reader.GetString(2)}";
                Player.Instance.CurrentHealth = reader.GetInt32(3);
            }
            reader.Close();
        }
    }
}