using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
namespace Vinetory
{
    class Database
    {
        public SQLiteConnection kon;
        public Database()
        {
            kon = new SQLiteConnection("Data source=Vinetory.db");
            if (!File.Exists("./Vinetory.db"))
            {
                SQLiteConnection.CreateFile("Vinetory.db");
            }
        }
    }
}
