using SQLite;
using TaskandLog.Database;
using TaskandLog.Tables;

namespace TaskandLog.TableRepositories
{
	public class LogEntryRepository
	{

		public static SQLiteConnection DatabaseConnection;

		public static void Init()
		{
			DatabaseConnection = DB.DatabaseInit();
			DatabaseConnection.CreateTable<LogEntry>();
		}

		public void AddLogEntry(string Log_entry_date_time, string Log_entry_type, string Log_entry_description)
		{
			Init();
			var logentry = new LogEntry
			{
				Log_entry_date_time=Log_entry_date_time,
				Log_entry_type=Log_entry_type,
				Log_entry_description=Log_entry_description

			};
			_ = DatabaseConnection.Insert(logentry);
		}

		public void DeleteLogEntry(int Log_entry_id)
		{
			Init();
			DatabaseConnection.Delete<LogEntry>(Log_entry_id);
		}

		public void UpdateLogEntry(int Log_entry_id, string Log_entry_date_time, string Log_entry_type, string Log_entry_description)

        {
			Init();
			var logentry = new LogEntry
			{
				Log_entry_id = Log_entry_id,
                Log_entry_date_time = Log_entry_date_time,
                Log_entry_type = Log_entry_type,
                Log_entry_description = Log_entry_description


            };
			_ = DatabaseConnection.Update(logentry);

		}

		public List<LogEntry> GetLogEntries()
		{
			Init();
			return DatabaseConnection.Table<LogEntry>().ToList();
		}

	}
}
