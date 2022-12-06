using SQLite;

namespace TaskandLog.Tables
{
    [Table("logtype")]
    public class LogType
    {
        [PrimaryKey, AutoIncrement]
        public int Log_type_id { get; set; }
        [NotNull]
        public string Log_type_name { get; set; }
    }
}
