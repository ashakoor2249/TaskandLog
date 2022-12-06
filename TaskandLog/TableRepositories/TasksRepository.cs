using SQLite;
using TaskandLog.Database;
using TaskandLog.Tables;

namespace TaskandLog.TableRepositories
{
    public class TasksRepository
    {
        readonly DB Database = new();

        public void Init()
        {
            Database.DatabaseConnection = Database.DatabaseInit();
            Database.DatabaseConnection.CreateTable<Tasks>();
        }

        public void AddTask(string Task_name)
        {
            Init();
            var tasks = new Tasks
            {
                Task_name= Task_name,

            };
            _ = Database.DatabaseConnection.Insert(tasks);
        }

        public void DeleteTask(int Task_id)
        {
            Init();
            Database.DatabaseConnection.Delete<Tasks>(Task_id);
        }

        public void UpdateTask(int Task_id, string Task_name)
        {
            Init();
            var tasks = new Tasks
            {
                Task_id= Task_id,
                Task_name= Task_name

            };
            _ = Database.DatabaseConnection.Update(tasks);

        }

        public List<Tasks> GetTasks()
        {
            Init();
            return Database.DatabaseConnection.Table<Tasks>().ToList();
        }
    }
}
