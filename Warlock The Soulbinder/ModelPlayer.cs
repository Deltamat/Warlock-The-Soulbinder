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
            string sqlexp = $"CREATE TABLE IF NOT EXISTS Player{Controller.Instance.CurrentSaveFile} (id integer primary key, " +
                "spriteName string, " +
                "X integer, " +
                "Y integer, " +
                "zone string, " +
                $"FOREIGN KEY(soulWeapon) REFERENCES SoulStone{Controller.Instance.CurrentSaveFile}(id)," +
                $"FOREIGN KEY(soulArmour) REFERENCES SoulStone{Controller.Instance.CurrentSaveFile}(id)," +
                $"FOREIGN KEY(soulTrinket1) REFERENCES SoulStone{Controller.Instance.CurrentSaveFile}(id)," +
                $"FOREIGN KEY(soulTrinket2) REFERENCES SoulStone{Controller.Instance.CurrentSaveFile}(id)," +
                $"FOREIGN KEY(soulTrinket3) REFERENCES SoulStone{Controller.Instance.CurrentSaveFile}(id))";
            cmd = connection.CreateCommand();
            cmd.CommandText = sqlexp;
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Deletes the database to make it ready for a new save
        /// </summary>
        public void ClearDB(string selectedSaveFile)
        {
            cmd.CommandText = $"DELETE FROM Player{selectedSaveFile}";
            cmd.ExecuteNonQuery();
        }

        public void SavePlayer(string selectedSaveFile, int X, int Y, string zone)
        {
            cmd.CommandText = $"INSERT INTO Player{selectedSaveFile} (id, X, Y, zone) VALUES (null, {X}, {Y}, {zone})";
            cmd.ExecuteNonQuery();
        }

        public void LoadPlayer(string selectedSaveFile)
        {
            cmd.CommandText = $"SELECT FROM * Player{selectedSaveFile}";

            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Player.Instance.Position = new Vector2(reader.GetInt32(1), reader.GetInt32(2));
                GameWorld.Instance.currentZone = $"{reader.GetString(3)}";
            }
            reader.Close();
        }
    }
}
