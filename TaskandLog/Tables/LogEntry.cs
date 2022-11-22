﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskandLog.Tables
{
	[Table("logentry")]
	public class LogEntry
	{
		[PrimaryKey]
		public int Log_entry_id { get; set; }
		public string Log_entry { get; set; }
	}
}
