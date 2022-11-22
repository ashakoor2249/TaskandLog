using SQLite;

namespace TaskandLog.Tables
{
    [Table("logtype")]
    public class LogType
    {
        [PrimaryKey]
        public int Log_type_id { get; set; }
        public string Log_type_name { get; set; }
    }
}
