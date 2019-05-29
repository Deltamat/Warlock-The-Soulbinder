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
            string sqlexp = "CREATE TABLE IF NOT EXISTS Player (X string primary key, " +
                "Y string, " +
                "zone string, " +
                "soulWeapon integer," +
                "soulArmour integer," +
                "soulTrinket1 integer," +
                "soulTrinket2 integer," +
                "soulTrinket3 integer," +
                "FOREIGN KEY(soulWeapon) REFERENCES SoulStone(id)," +
                "FOREIGN KEY(soulArmour) REFERENCES SoulStone(id)," +
                "FOREIGN KEY(soulTrinket1) REFERENCES SoulStone(id)," +
                "FOREIGN KEY(soulTrinket2) REFERENCES SoulStone(id)," +
                "FOREIGN KEY(soulTrinket3) REFERENCES SoulStone(id))";
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

        public void SavePlayer(float X, float Y, string zone, int soulWeapon, int soulArmour, int soulTrinket1, int soulTrinket2, int soulTrinket3)
        {
            cmd.CommandText = $"INSERT INTO Player (X, Y, zone, soulWeapon, soulArmour, soulTrinket1, soulTrinket2, soulTrinket3) VALUES ('{Math.Floor(X) + 0.1f}', '{Math.Floor(Y) + 0.1f}', '{zone}', {soulWeapon}, {soulArmour}, {soulTrinket1}, {soulTrinket2}, {soulTrinket3})";
            cmd.ExecuteNonQuery();
        }

        public void LoadPlayer()
        {
            cmd.CommandText = "SELECT * FROM Player";

            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                #region TryCatch
                try
                {
                    Equipment.Instance.Weapon = FilledStone.StoneList[reader.GetInt32(3)];
                }
                catch (Exception)
                {
                    Equipment.Instance.Weapon = null;
                }
                try
                {
                    Equipment.Instance.Armor = FilledStone.StoneList[reader.GetInt32(4)];
                }
                catch (Exception)
                {
                    Equipment.Instance.Armor = null;
                }
                try
                {
                    Equipment.Instance.Skill1 = FilledStone.StoneList[reader.GetInt32(5)];
                }
                catch (Exception)
                {
                    Equipment.Instance.Skill1 = null;
                }
                try
                {
                    Equipment.Instance.Skill2 = FilledStone.StoneList[reader.GetInt32(6)];
                }
                catch (Exception)
                {
                    Equipment.Instance.Skill2 = null;
                }
                try
                {
                    Equipment.Instance.Skill3 = FilledStone.StoneList[reader.GetInt32(7)];
                }
                catch (Exception)
                {
                    Equipment.Instance.Skill3 = null;
                }
                #endregion

                Player.Instance.Position = new Vector2((float)Convert.ToDouble(reader.GetString(0)) - 0.1f, (float)Convert.ToDouble(reader.GetString(1)) - 0.1f);
                GameWorld.Instance.currentZone = $"{reader.GetString(2)}";
            }
            reader.Close();
        }
    }
}
