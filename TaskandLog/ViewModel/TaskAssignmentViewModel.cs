using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using TaskandLog.Database;
using TaskandLog.TableRepositories;
using TaskandLog.Tables;


namespace TaskandLog.ViewModel;
public partial class TaskAssignmentViewModel : ObservableObject
{
    readonly DB Database = new();

    public Tasks Tasks = new();
    public SunWatchPersonale SunWatchPersonale = new();
    public AssignedTask Assigned = new();

    public TasksRepository TasksRepo = new();
    public SunWatchPersonaleRepository SunWatchPersonaleRepo = new();
    public AssignedTaskRepository AssignedTaskRepo = new();

    public List<SunWatchPersonale> SunWatchPersonales = new();
    public List<Tasks> AllTasks = new();
    public List<AssignedTask> AssignedTasks= new();

    [ObservableProperty] public DateTime todaysDate = DateTime.Today;
    [ObservableProperty] public DateTime minDate = DateTime.Today;
    [ObservableProperty] public DateTime maxDate = DateTime.MaxValue;


    [ObservableProperty] public string selectedSunWatchPersonale;
    [ObservableProperty] public string selectedTask;
    [ObservableProperty] public string selectedDay;

    [ObservableProperty] ObservableCollection<string> sunWatchPersonaleList = new();
    [ObservableProperty] ObservableCollection<string> taskList = new();
    [ObservableProperty] ObservableCollection<string> assignedTaskList=new();
    [ObservableProperty] ObservableCollection<string> dayList = new();

    public TaskAssignmentViewModel()
    {
        if (Database.DatabaseConnection == null)
        {
            Database.DatabaseInit();
        }
        PopulateSunWatchPersonaleList();
        PopulateTasksList();
        PopulateDayList();
    }

   public void PopulateSunWatchPersonaleList()
    {
        SunWatchPersonales = SunWatchPersonaleRepo.GetSunWatchPersonale();
        if(SunWatchPersonales != null) 
        {
            foreach(SunWatchPersonale sunwatchpersonale in SunWatchPersonales)
            {
                sunWatchPersonaleList.Add(sunwatchpersonale.SunWatch_personale_name+" ("+sunwatchpersonale.SunWatch_personale_shift+" Shift)");
            }
            
        }

    }

    public void PopulateTasksList()
    {
        AllTasks = TasksRepo.GetTasks();
        if (AllTasks != null)
        {
            foreach (Tasks tasks in AllTasks)
            {
                taskList.Add(tasks.Task_name);
            }

        }

    }

    public void PopulateDayList()
    {
        foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
        {
            dayList.Add(day.ToString());
        }
    }

    public enum DayOfWeek
    {
        Sunday = 0,
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
    }

    public void GetAllAssignedTasks()
    {
        AssignedTasks = AssignedTaskRepo.GetAssignedTasks();
        if (AssignedTasks != null)
        {
            foreach (AssignedTask assignedtask in AssignedTasks)
            {
                assignedTaskList.Add(assignedtask.Assigned_task_id+" "+assignedtask.Assigned_task_sunwatch_personale_name+" "+assignedtask.Assigned_task_name);
            }

        }

    }

    public void DisplayTasks()
    {

    }

    [RelayCommand]
    public async void AddAssignedTask()
    {
        try
        {
            AssignedTaskRepo.AddAssignedTask(selectedSunWatchPersonale, selectedTask, selectedDay);
        }
        catch (Exception ex)
        {

        }
    }

    [RelayCommand]
    public async void RemoveAssignedTask()
    {

    }

    [RelayCommand]
    public async void UpdateAssignedTask()
    {

    }




    


}

