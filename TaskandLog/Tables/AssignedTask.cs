using SQLite;


namespace TaskandLog.Tables
{
    [Table("assignedtask")]
    public class AssignedTask
    {
        [PrimaryKey, AutoIncrement]
        public int Assigned_task_id { get; set; }
        public string Assigned_task_sunwatch_personale_name { get; set; }
        public string Assigned_task_name { get; set; }

        public string Assigned_task_day { get; set; }

    }
}
