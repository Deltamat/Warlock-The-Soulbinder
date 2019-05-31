using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    class ModelConsumable : Model
    {
    //    /// <summary>
    //    /// Creates the columns for the table, unless the table with the specified name "Consumable" already exists.
    //    /// </summary>
    //    public ModelConsumable()
    //    {
    //        string sqlexp = "CREATE TABLE IF NOT EXISTS Consumable (id integer primary key, " +
    //            "name string, " +
    //            "amount integer )";
    //        cmd = connection.CreateCommand();
    //        cmd.CommandText = sqlexp;
    //        cmd.ExecuteNonQuery();
    //    }

    //    /// <summary>
    //    /// Deletes the database to make it ready for a new save
    //    /// </summary>
    //    public void ClearDB()
    //    {
    //        cmd.CommandText = "DELETE FROM Consumable";
    //        cmd.ExecuteNonQuery();
    //    }

    //    /// <summary>
    //    /// Saves all consumables to the selected save file
    //    /// </summary>
    //    /// <param name="name"></param>
    //    /// <param name="amount"></param>
    //    public void SaveConsumable(string name, int amount)
    //    {
    //        cmd.CommandText = $"INSERT INTO Consumable(id, name, amount) VALUES (null, '{name}', {amount})";
    //        cmd.ExecuteNonQuery();
    //    }

    //    /// <summary>
    //    /// Loads into a dictionary all the consumables, which then all need to be given their individual position
    //    /// </summary>
    //    /// <returns>a dictionary of all the consumables saved in the database</returns>
    //    public Dictionary<int, Consumable> LoadConsumable()
    //    {
    //        Dictionary<int, Consumable> consumableDic = new Dictionary<int, Consumable>();
    //        cmd.CommandText = "SELECT * FROM Consumable";
    //        SQLiteDataReader reader = cmd.ExecuteReader();
    //        while (reader.Read())
    //        {
    //            consumableDic.Add(reader.GetInt32(0), new Consumable(reader.GetString(1), reader.GetInt32(2)));
    //        }
    //        reader.Close();
    //        return consumableDic;
    //    }
    }
}
