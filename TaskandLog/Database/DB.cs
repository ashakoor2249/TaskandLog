using SQLite;

namespace TaskandLog.Database
{
	public class DB
	{
		public string DatabasePath { get; set; }
		public string DatabaseName { get; set; }
		public SQLiteConnection DatabaseConnection { get; set; }

		public SQLiteConnection DatabaseInit()
		{
			DatabaseName = "TaskAndLog.db";
			DatabasePath = Path.Combine(FileSystem.AppDataDirectory, DatabaseName);
			DatabaseConnection = new SQLiteConnection(DatabasePath);
			return DatabaseConnection;
		}
	}
}
