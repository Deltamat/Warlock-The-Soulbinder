using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    class ModelQuest : Model
    {
        public ModelQuest()
        {
            string sqlexp = $"CREATE TABLE IF NOT EXISTS Quest{Controller.Instance.CurrentSaveFile} (id integer primary key, " +
                "name string, " +
                "status string )";
            cmd = connection.CreateCommand();
            cmd.CommandText = sqlexp;
            cmd.ExecuteNonQuery();
          
        }

        public void ClearDB()
        {
            cmd.CommandText = $"DELETE FROM Quest{Controller.Instance.CurrentSaveFile}";
            cmd.ExecuteNonQuery();
        }

        public void SaveQuest(string questName, string questStatus)
        {
            cmd.CommandText = $"INSERT INTO Quest{Controller.Instance.CurrentSaveFile} (id, name, status) VALUES (null, {questName}, {questStatus})";
            cmd.ExecuteNonQuery();
        }
       

        public Dictionary<int, string> LoadQuest()
        {
            Dictionary<int, string> questsDic = new Dictionary<int, string>();
            cmd.CommandText = $"SELECT * FROM Quest{Controller.Instance.CurrentSaveFile}";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                questsDic.Add(reader.GetInt32(0), $"{reader.GetString(1)}" + $"{reader.GetString(2)}");
            }
            reader.Close();
            return questsDic;
        }
        
    }
}
