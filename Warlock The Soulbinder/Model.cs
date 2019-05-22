using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    class Model
    {
        private static string connectionString = @"Data Source=Warlock1.db;version=3;New=true;Compress=true";
        protected SQLiteConnection connection = new SQLiteConnection(ConnectionString);
        protected SQLiteCommand cmd;

        public static string ConnectionString { get => connectionString; private set => connectionString = value; }

        public Model()
        {
            ConnectionString = @"Data Source=Warlock1.db;version=3;New=true;Compress=true";
            
        }

        public void OpenConnection()
        {
            connection.Open();
        }

        public void CloseConnection()
        {
            connection.Close();
        }
      
    }
}
