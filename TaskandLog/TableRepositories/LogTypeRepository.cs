using SQLite;
using TaskandLog.Database;
using TaskandLog.Tables;

namespace TaskandLog.TableRepositories
{
    public class LogTypeRepository
    {
		readonly DB Database = new();

		public void Init()
		{
			Database.DatabaseConnection = Database.DatabaseInit();
            Database.DatabaseConnection.CreateTable<LogType>();
		}

		public void AddLogType(string Log_type_name)
		{
			Init();
			var logtype = new LogType
			{
				Log_type_name = Log_type_name,
				
			};
			_ = Database.DatabaseConnection.Insert(logtype);
		}

		public void DeleteLogType(int Log_type_id)
		{
			Init();
            Database.DatabaseConnection.Delete<LogType>(Log_type_id);
		}

		public void UpdateLogType(int Log_type_id, string Log_type_name)
		{
			Init();
			var logtype = new LogType
			{
				Log_type_id= Log_type_id,
				Log_type_name = Log_type_name,

			};
			_ = Database.DatabaseConnection.Update(logtype);

		}

		public List<LogType> GetLogTypes()
		{
			Init();
			return Database.DatabaseConnection.Table<LogType>().ToList();
		}
	}

}
