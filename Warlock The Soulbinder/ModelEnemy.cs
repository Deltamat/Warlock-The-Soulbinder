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
            cmd.CommandText = $"INSERT INTO Enemy (id, level, X, Y, defense, damage, maxHealth, attackSpeed, metalResistance, earthResistance, airResistance, fireResistance, darkResistance, waterResistance, monster) VALUES (null, {level}, '{Math.Floor(X)+ 0.1f}', '{Math.Floor(Y)+0.1f}', {defense}, {damage}, {maxHealth}, '{Math.Floor(attackSpeed) + 0.1f}', '{metalResistance}', '{earthResistance}', '{airResistance}', '{fireResistance}', '{darkResistance}', '{waterResistance}', '{monster}')";
            cmd.ExecuteNonQuery();
        }

        public List<Enemy> LoadEnemy()
        {
            List<Enemy> enemies = new List<Enemy>();
            cmd.CommandText = "SELECT * FROM Enemy";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //float a2, a3, a7, a8, a9, a10, a11, a12, a13;
                //try
                //{
                //    a2 = (float)Convert.ToDouble(reader.GetString(2));
                //}
                //catch (Exception)
                //{
                //    a2 = (float)Convert.ToDouble(reader.GetString(2)) + 0.000001f;
                //}
                //try
                //{
                //    a3 = (float)Convert.ToDouble(reader.GetString(3));
                //}
                //catch (Exception)
                //{
                //    a3 = (float)Convert.ToDouble(reader.GetString(3)) + 0.000001f;
                //}
                //try
                //{
                //    a7 = (float)Convert.ToDouble(reader.GetString(7));
                //}
                //catch (Exception)
                //{
                //    a7 = (float)Convert.ToDouble(reader.GetString(7)) ;
                //}
                //try
                //{
                //    a8 = (float)Convert.ToDouble(reader.GetString(8));
                //}
                //catch (Exception)
                //{
                //    a8 = (float)Convert.ToDouble(reader.GetString(8)) + 0.000001f;
                //}
                //try
                //{
                //    a9 = (float)Convert.ToDouble(reader.GetString(9));
                //}
                //catch (Exception)
                //{
                //    a9 = (float)Convert.ToDouble(reader.GetString(9)) + 0.000001f;
                //}
                //try
                //{
                //    a10 = (float)Convert.ToDouble(reader.GetString(10));
                //}
                //catch (Exception)
                //{
                //    a10 = (float)Convert.ToDouble(reader.GetString(10)) + 0.000001f;
                //}
                //try
                //{
                //    a11 = (float)Convert.ToDouble(reader.GetString(11));
                //}
                //catch (Exception)
                //{
                //    a11 = (float)Convert.ToDouble(reader.GetString(11)) + 0.000001f;
                //}
                //try
                //{
                //    a12 = (float)Convert.ToDouble(reader.GetString(12));
                //}
                //catch (Exception)
                //{
                //    a12 = (float)Convert.ToDouble(reader.GetString(12)) + 0.000001f;
                //}
                //try
                //{
                //    a13 = (float)Convert.ToDouble(reader.GetString(13));
                //}
                //catch (Exception)
                //{
                //    a13 = (float)Convert.ToDouble(reader.GetString(13)) + 0.000001f;
                //}
                //enemies.Add(new Enemy(reader.GetInt32(1), new Vector2(a2, a3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), a7, a8, a9, a10, a11, a12, a13, reader.GetString(14)));
                enemies.Add(new Enemy(reader.GetInt32(1), new Vector2((float)Convert.ToDouble(reader.GetString(2)) - 0.1f, (float)Convert.ToDouble(reader.GetString(3))-0.1f), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), (float)Convert.ToDouble(reader.GetString(7)) - 0.1f, (float)Convert.ToDouble(reader.GetString(8)), (float)Convert.ToDouble(reader.GetString(9)), (float)Convert.ToDouble(reader.GetString(10)), (float)Convert.ToDouble(reader.GetString(11)), (float)Convert.ToDouble(reader.GetString(12)), (float)Convert.ToDouble(reader.GetString(13)), reader.GetString(14)));
            }
            return enemies;
        }
    }
}
