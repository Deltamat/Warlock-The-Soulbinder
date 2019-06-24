using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    /// <summary>
    /// This model is used for saving and loading enemies.
    /// </summary>
    class ModelEnemy : Model
    {
        /// <summary>
        /// Creates a table for saving enemies if the table hasn't already been created.
        /// </summary>
        public ModelEnemy()
        {
            string sqlexp = "CREATE TABLE IF NOT EXISTS Enemy (id integer primary key, " +
                "level integer, " +
                "X float, " +
                "Y float, " +
                "defense integer, " +
                "damage integer, " +
                "maxHealth integer, " +
                "attackSpeed float, " +
                "metalResistance float, " +
                "earthResistance float, " +
                "airResistance float, " +
                "fireResistance float, " +
                "darkResistance float, " +
                "waterResistance float, " +
                "monster string )";
            cmd = connection.CreateCommand();
            cmd.CommandText = sqlexp;
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Clears the Enemy database to make it ready for a new save.
        /// </summary>
        public void ClearDB()
        {
            cmd.CommandText = "DELETE FROM Enemy";
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Saves all the otherwise random parameters the enemies have in the current zone,
        /// using a special constructor in the Enemy class so that they don't randomize, when they are potentially loaded again.
        /// </summary>
        /// <param name="level">Level of the creature.</param>
        /// <param name="X">X-coordinate.</param>
        /// <param name="Y">Y-coordinate.</param>
        /// <param name="defense">Defense stat.</param>
        /// <param name="damage">Damage stat.</param>
        /// <param name="maxHealth">Max health stat.</param>
        /// <param name="attackSpeed">Attack speed stat.</param>
        /// <param name="metalResistance">Metal resistance stat.</param>
        /// <param name="earthResistance">Earth resistance stat.</param>
        /// <param name="airResistance">Air resistance stat.</param>
        /// <param name="fireResistance">Fire resistance stat.</param>
        /// <param name="darkResistance">Dark resistance stat.</param>
        /// <param name="waterResistance">Water resistance stat.</param>
        /// <param name="monster">Name of the monster.</param>
        public void SaveEnemy(int level, float X, float Y, int defense, int damage, int maxHealth, float attackSpeed, float metalResistance, float earthResistance, float airResistance, float fireResistance, float darkResistance, float waterResistance, string monster)
        {
            cmd.CommandText = $"INSERT INTO Enemy (id, level, X, Y, defense, damage, maxHealth, attackSpeed, metalResistance, earthResistance, airResistance, fireResistance, darkResistance, waterResistance, monster) VALUES (null, {level}, {X.ToString(GameWorld.Instance.replaceComma)}, {Y.ToString(GameWorld.Instance.replaceComma)}, {defense}, {damage}, {maxHealth}, {attackSpeed.ToString(GameWorld.Instance.replaceComma)}, {metalResistance.ToString(GameWorld.Instance.replaceComma)}, {earthResistance.ToString(GameWorld.Instance.replaceComma)}, {airResistance.ToString(GameWorld.Instance.replaceComma)}, {fireResistance.ToString(GameWorld.Instance.replaceComma)}, {darkResistance.ToString(GameWorld.Instance.replaceComma)}, {waterResistance.ToString(GameWorld.Instance.replaceComma)}, '{monster}')";
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Loads the list of enemies for the current zone.
        /// </summary>
        /// <returns>List of enemies.</returns>
        public List<Enemy> LoadEnemy()
        {
            List<Enemy> enemies = new List<Enemy>();
            cmd.CommandText = "SELECT * FROM Enemy";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                enemies.Add(new Enemy(reader.GetInt32(1), new Vector2(reader.GetFloat(2), reader.GetFloat(3)), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetFloat(7), reader.GetFloat(8), reader.GetFloat(9), reader.GetFloat(10), reader.GetFloat(11), reader.GetFloat(12), reader.GetFloat(13), reader.GetString(14)));
            }
            reader.Close();
            return enemies;
        }
    }
}