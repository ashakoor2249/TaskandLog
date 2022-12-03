using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskandLog.Database;
using TaskandLog.Tables;

namespace TaskandLog.TableRepositories
{
    
    public class LogEmailDistributionRepository
    {
        readonly DB Database = new();

        public void Init()
        {
            Database.DatabaseConnection = Database.DatabaseInit();
            Database.DatabaseConnection.CreateTable<LogEmailDistribution>();
        }

        public void AddLogEmailDistribution(string Log_email_distribution_to, string Log_email_distribution_cc)
        {
            Init();
            var logentryemaildistribution = new LogEmailDistribution()
            {
                Log_email_distribution_to = Log_email_distribution_to,
                Log_email_distribution_cc = Log_email_distribution_cc
            };

            _ = Database.DatabaseConnection.Insert(logentryemaildistribution);
        }

        public void DeleteLogEntry(int Log_email_distribution_id)
        {
            Init();
            Database.DatabaseConnection.Delete<LogEmailDistribution>(Log_email_distribution_id);
        }

        public List<LogEmailDistribution> GetLogEmailDistribution()
        {
            Init();
            return Database.DatabaseConnection.Table<LogEmailDistribution>().ToList();
        }
    }
}
