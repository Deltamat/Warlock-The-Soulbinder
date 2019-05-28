using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    class ModelEnemy : Model
    {
        public ModelEnemy()
        {
            string sqlexp = "CREATE TABLE IF NOT EXISTS Enemy (id integer primary key, " +
                "level integer, " +
                "X string, " +
                "Y string, " +
                "defense integer, " +
                "damage integer, " +
                "maxHealth integer, " +
                "attackSpeed string, " +
                "metalResistance string, " +
                "earthResistance string, " +
                "airResistance string, " +
                "fireResistance string, " +
                "darkResistance string, " +
                "waterResistance string, " +
                "monster string )";
            cmd = connection.CreateCommand();
            cmd.CommandText = sqlexp;
            cmd.ExecuteNonQuery();
        }

        public void ClearDB()
        {
            cmd.CommandText = "DELETE FROM Enemy";
            cmd.ExecuteNonQuery();
        }

        public void SaveEnemy(int level, float X, float Y, int defense, int damage, int maxHealth, float attackSpeed, float metalResistance, float earthResistance, float airResistance, float fireResistance, float darkResistance, float waterResistance, string monster)
        {
            cmd.CommandText = $"INSERT INTO Enemy (id, level, X, Y, defense, damage, maxHealth, attackSpeed, metalResistance, earthResistance, airResistance, fireResistance, darkResistance, waterResistance, monster) VALUES (null, {level}, '{X}', '{Y}', {defense}, {damage}, {maxHealth}, '{attackSpeed}', '{metalResistance}', '{earthResistance}', '{airResistance}', '{fireResistance}', '{darkResistance}', '{waterResistance}', '{monster}')";
            cmd.ExecuteNonQuery();
        }

        public List<Enemy> LoadEnemy()
        {
            List<Enemy> enemies = new List<Enemy>();
            cmd.CommandText = "SELECT * FROM Enemy";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                enemies.Add(new Enemy(reader.GetInt32(1), new Vector2(reader.GetFloat(2), reader.GetFloat(3)), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetFloat(7), reader.GetFloat(8), reader.GetFloat(9), reader.GetFloat(10), reader.GetFloat(11), reader.GetFloat(12), reader.GetFloat(13), reader.GetString(14)));
            }
            return enemies;
        }
    }
}
