using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TaskandLog.Tables
{
    [Table("sunwatchpersonale")]
    public class SunWatchPersonale
    {
        [PrimaryKey, AutoIncrement]
        public int SunWatch_personale_id { get; set; }
        [NotNull]
        public string SunWatch_personale_knid { get; set; }
        [NotNull]
        public string SunWatch_personale_name { get; set; }
        [NotNull]
        public string SunWatch_personale_shift { get; set; }
        [NotNull]
        public string SunWatch_personale_days_worked { get; set; }
    }
}
