using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace PokeGraf.Banco
{
    public class IniciaBanco
    {
        public SQLiteConnection GetConnection()
        {
            if (!File.Exists("database.sqlite3"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
            }
            return new SQLiteConnection(@"Data Source=database.sqlite3;foreign keys=true");
        }
    }
}
