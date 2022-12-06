using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskandLog.Tables
{
    [Table("task")]
    public class Tasks
    {
        [PrimaryKey, AutoIncrement]
        public int Task_id { get; set; }
        public string Task_name { get; set; }
        

    }
}
