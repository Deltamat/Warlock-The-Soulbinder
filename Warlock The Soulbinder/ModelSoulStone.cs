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
        /// <summary>
        /// Creates a table for saving filled soulstones if the table hasn't already been created.
        /// </summary>
        public ModelSoulStone()
        {
            string sqlexp = "CREATE TABLE IF NOT EXISTS SoulStone (id integer primary key, " +
                "monster string, " +
                "experience integer, " +
                "equipmentSlot string, " +
                "level integer, " +
                "damage integer, " +
                "maxHealth string, " +
                "attackSpeed float )";
            cmd = connection.CreateCommand();
            cmd.CommandText = sqlexp;
            cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Deletes the SoulStone database to make it ready for a new save.
        /// </summary>
        public void ClearDB()
        {
            cmd.CommandText = "DELETE FROM SoulStone";
            cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// Saves the necesary stats on the soulstones, so that they later potentially will be loaded 
        /// via a special constructor in the FilledStone class to prevent the otherwise random stats at point of creation inside the game.
        /// </summary>
        /// <param name="monster">Type of the monster the soulstone was extracted from.</param>
        /// <param name="experience">Current experience on the soulstone.</param>
        /// <param name="equipmentSlot">The slot the soulstone is equipped to if any.</param>
        /// <param name="level">Current level of the soulstone.</param>
        /// <param name="damage">The damage boost of the soulstone.</param>
        /// <param name="maxHealth">Max health boost of the soulstone.</param>
        /// <param name="attackSpeed">Attackspeed boost of the soulstone.</param>
        public void SaveSoulStone(string monster, int experience, string equipmentSlot, int level, int damage, int maxHealth, float attackSpeed)
        {
            cmd.CommandText = $"INSERT INTO SoulStone (id, monster, experience, equipmentSlot, level, damage, maxhealth, attackSpeed) VALUES (null, '{monster}', {experience}, '{equipmentSlot}', {level}, {damage}, {maxHealth}, {attackSpeed.ToString(GameWorld.Instance.replaceComma)})";
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Loads all soulstones and return them as a list as well as checks if any of them are equipped to any equipmentslot and proceeds to equip it to that slot if it finds one.
        /// </summary>
        /// <returns>A list of filled soulstones.</returns>
        public List<FilledStone> LoadSoulStone()
        {
            List<FilledStone> soulStones = new List<FilledStone>();
            cmd.CommandText = "SELECT * FROM SoulStone";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                FilledStone stone = new FilledStone(reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetFloat(7));
                switch (reader.GetString(3))
                {
                    case "Weapon":
                        stone.Equipped = true;
                        Equipment.Instance.EquipStone(0, stone);
                        break;
                    case "Armor":
                        stone.Equipped = true;
                        Equipment.Instance.EquipStone(1, stone);
                        break;
                    case "Skill1":
                        stone.Equipped = true;
                        Equipment.Instance.EquipStone(2, stone);
                        break;
                    case "Skill2":
                        stone.Equipped = true;
                        Equipment.Instance.EquipStone(3, stone);
                        break;
                    case "Skill3":
                        stone.Equipped = true;
                        Equipment.Instance.EquipStone(4, stone);
                        break;
                    default:
                        break;
                }
                soulStones.Add(stone);
            }
            reader.Close();
            return soulStones;
        }
    }
}