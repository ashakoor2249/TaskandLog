using SQLite;
using TaskandLog.Database;
using TaskandLog.Tables;


namespace TaskandLog.TableRepositories
{
    public class AssignedTaskRepository
    {
        readonly DB Database = new();

        public void Init()
        {
            Database.DatabaseConnection = Database.DatabaseInit();
            Database.DatabaseConnection.CreateTable<AssignedTask>();
        }

        public void AddAssignedTask(string Assigned_task_sunwatch_personale_name, string Assigned_task_name, string Assigned_task_day)
        {
            Init();
            var assignedtask = new AssignedTask
            {
                Assigned_task_sunwatch_personale_name = Assigned_task_sunwatch_personale_name,
                Assigned_task_name= Assigned_task_name,
                Assigned_task_day= Assigned_task_day
                

            };
            _ = Database.DatabaseConnection.Insert(assignedtask);
        }

        public void DeleteAssignedTask(int Assigned_task_id)
        {
            Init();
            Database.DatabaseConnection.Delete<AssignedTask>(Assigned_task_id);
        }

        public void UpdateAssignedTask(int Assigned_task_id, string Assigned_task_sunwatch_personale_name, string Assigned_task_name, string Assigned_task_day)
        {
            Init();
            var assignedtask = new AssignedTask
            {
                Assigned_task_id = Assigned_task_id,
                Assigned_task_sunwatch_personale_name = Assigned_task_sunwatch_personale_name,
                Assigned_task_name=Assigned_task_name,
                Assigned_task_day= Assigned_task_day

            };
            _ = Database.DatabaseConnection.Update(assignedtask);

        }

        public List<AssignedTask> GetAssignedTasks()
        {
            Init();
            return Database.DatabaseConnection.Table<AssignedTask>().ToList();
        }

        public TableQuery<AssignedTask> QueryByTaskNameAndDay(string TaskName, string Day)
        {
            Init();
            return Database.DatabaseConnection.Table<AssignedTask>().Where(value => value.Assigned_task_name.Equals(TaskName) && value.Assigned_task_day.Equals(Day));

        }
    }
}
