using SQLite;

namespace TaskandLog.Database
{
	public class DB
	{
		public static string DatabasePath;
		public static string DatabaseName;
		public static SQLiteConnection DatabaseConnection;

		public static SQLiteConnection DatabaseInit()
		{
			DatabaseName = "TaskAndLog.db";
			DatabasePath = Path.Combine(FileSystem.AppDataDirectory, DatabaseName);
			DatabaseConnection = new SQLiteConnection(DatabasePath);
			return DatabaseConnection;
		}
	}
}
