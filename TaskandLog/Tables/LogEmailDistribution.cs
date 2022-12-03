using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskandLog.Tables
{
    public class LogEmailDistribution
    {
        [PrimaryKey, AutoIncrement]
        public int Log_email_distribution_id { get; set; }
        public string Log_email_distribution_to { get; set; }

        public string Log_email_distribution_cc { get; set; }
    }
}
