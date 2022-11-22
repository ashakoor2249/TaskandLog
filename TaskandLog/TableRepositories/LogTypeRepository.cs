using SQLite;
using TaskandLog.Database;
using TaskandLog.Tables;

namespace TaskandLog.TableRepositories
{
    public class LogTypeRepository
    {
		public static SQLiteConnection DatabaseConnection;

		public static void Init()
		{
			DatabaseConnection = DB.DatabaseInit();
			DatabaseConnection.CreateTable<LogType>();
		}

		public void AddLogType(string Log_type_name)
		{
			Init();
			var logtype = new LogType
			{
				Log_type_name = Log_type_name,
				
			};
			_ = DatabaseConnection.Insert(logtype);
		}

		public void DeleteLogType(int Log_type_id)
		{
			Init();
			DatabaseConnection.Delete<LogType>(Log_type_id);
		}

		public void UpdateLogType(int Log_type_id, string Log_type_name)
		{
			Init();
			var logtype = new LogType
			{
				Log_type_id= Log_type_id,
				Log_type_name = Log_type_name,

			};
			_ = DatabaseConnection.Update(logtype);

		}

		public List<LogType> GetLogTypes()
		{
			Init();
			return DatabaseConnection.Table<LogType>().ToList();
		}
	}

}
