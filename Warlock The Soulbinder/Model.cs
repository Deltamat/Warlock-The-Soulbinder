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
        private string connectionString;
        private SQLiteConnection connection;
        protected SQLiteCommand cmd;

        public Model()
        {
        }
    }
}
