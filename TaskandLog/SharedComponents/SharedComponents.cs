using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskandLog.TableRepositories;
using TaskandLog.Tables;

namespace TaskandLog.SharedComponents
{
	public class SharedComponents
	{
		public LogTypeRepository LogTypeRepo { get; set; }
		public LogEntryRepository LogEntryRepo { get; set; }
		public List<LogType> LogTypes { get; set; }
		public List<LogEntry> LogEntries { get; set; }
	}
}
