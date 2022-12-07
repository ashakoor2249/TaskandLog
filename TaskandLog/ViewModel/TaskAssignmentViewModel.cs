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
    [ObservableProperty] public string assignedTaskStatusMessage;

    [ObservableProperty] public AssignedTask selectedAssignedTask= new();


    [ObservableProperty] public ObservableCollection<string> sunWatchPersonaleList = new();
    [ObservableProperty] public ObservableCollection<string> taskList = new();
    [ObservableProperty] public ObservableCollection<string> dayList = new();
    

    [ObservableProperty] public ObservableCollection<AssignedTask> assignedTaskList = new();
   




    public TaskAssignmentViewModel()
    {
        if (Database.DatabaseConnection == null)
        {
            Database.DatabaseInit();
        }
        PopulateSunWatchPersonaleList();
        PopulateTasksList();
        PopulateDayList();
        GetAllAssignedTasks();
    }

   public void PopulateSunWatchPersonaleList()
    {
        SunWatchPersonales = SunWatchPersonaleRepo.GetSunWatchPersonale();
        if(SunWatchPersonales != null) 
        {
            foreach(SunWatchPersonale sunwatchpersonale in SunWatchPersonales)
            {
                sunWatchPersonaleList.Add(sunwatchpersonale.SunWatch_personale_name+"("+sunwatchpersonale.SunWatch_personale_shift+" Shift)");
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

    [RelayCommand]
    public void GetAllAssignedTasks()
    {
        AssignedTasks.Clear();
        AssignedTasks = AssignedTaskRepo.GetAssignedTasks();

        if(AssignedTaskList.Count!=0)
        {
            AssignedTaskList.Clear();
        }

        foreach(var assignedtask in AssignedTasks)
        {
            AssignedTaskList.Add(assignedtask);
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

        GetAllAssignedTasks();

    }

    [RelayCommand]
    public async void RemoveAssignedTask()
    {
        int AssignedTaskId;
        if(SelectedAssignedTask==null)
        {
            AssignedTaskStatusMessage = "Error: Failed To Remove Log.";
            await Task.Delay(2000);
            AssignedTaskStatusMessage = "";
            return;
        }

        else
        {
            AssignedTaskId = SelectedAssignedTask.Assigned_task_id;
        }

        if(AssignedTaskId==1)
        {
            return;
        }
        else
        {
            try
            {
                AssignedTaskRepo.DeleteAssignedTask(AssignedTaskId);
                GetAllAssignedTasks();
            }
            catch (Exception ex)
            {
                AssignedTaskStatusMessage = "Error: Failed To Remove Assigned Task. " + ex.Message;
                await Task.Delay(2000);
                AssignedTaskStatusMessage = "";
            }

        }

    }

}

