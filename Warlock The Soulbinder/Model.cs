using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    /// <summary>
    /// Superclass for all the models.
    /// </summary>
    class Model
    {
        protected SQLiteConnection connection;
        protected SQLiteCommand cmd;
        private const string connectionString1 = @"Data Source=Warlock1.db;version=3;New=true;Compress=true";
        private const string connectionString2 = @"Data Source=Warlock2.db;version=3;New=true;Compress=true";
        private const string connectionString3 = @"Data Source=Warlock3.db;version=3;New=true;Compress=true";

        /// <summary>
        /// Looks at which savefile is selected and sets the connectionString accorindgly. 
        /// Also makes sure to call a method that opens a connection to the database.
        /// </summary>
        public Model()
        {
            switch (GameWorld.Instance.CurrentSaveFile)
            {
                case "1":
                    connection = new SQLiteConnection(connectionString1);
                    break;
                case "2":
                    connection = new SQLiteConnection(connectionString2);
                    break;
                case "3":
                    connection = new SQLiteConnection(connectionString3);
                    break;
            }
            OpenConnection();
        }

        /// <summary>
        /// Opens a connection to the database for the currently used savefile unless one is already open.
        /// </summary>
        public void OpenConnection()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
        }

        /// <summary>
        /// Closes the connection to the database for the currently used savefile.
        /// </summary>
        public void CloseConnection()
        {
            connection.Close();
        }
    }
}