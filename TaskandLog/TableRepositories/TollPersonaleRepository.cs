using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskandLog.Database;
using TaskandLog.Tables;

namespace TaskandLog.TableRepositories
{
    public class TollPersonaleRepository
    {
        readonly DB Database = new();

        public static SQLiteConnection DatabaseConnection { get; set; }
        public void Init()
        {
            DatabaseConnection = Database.DatabaseInit();
            DatabaseConnection.CreateTable<TollPersonale>();
        }

        public TableQuery<TollPersonale> QueryLogReceipiants()
        {
            Init();
            return DatabaseConnection.Table<TollPersonale>().Where(value => value.Personale_department.ToUpper().Contains("SunWatch"));
        }
    }
}
