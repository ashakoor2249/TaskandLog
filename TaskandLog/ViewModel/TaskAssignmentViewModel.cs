using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic;
using SQLite;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TaskandLog.Database;
using TaskandLog.TableRepositories;
using TaskandLog.Tables;


namespace TaskandLog.ViewModel;
public partial class TaskAssignmentViewModel : ObservableObject
{
    readonly DB Database = new();

    public TableQuery<SunWatchPersonale> SunWatchPersonaleQueryByName;
    
    public string SunWatchPersonaleShift="";

    public Tasks Tasks = new();
    public SunWatchPersonale SunWatchPersonale = new();
    public AssignedTask Assigned = new();

    public TasksRepository TasksRepo = new();
    public SunWatchPersonaleRepository SunWatchPersonaleRepo = new();
    public AssignedTaskRepository AssignedTaskRepo = new();

    public List<SunWatchPersonale> SunWatchPersonales = new();
    public List<Tasks> AllTasks = new();
    public List<AssignedTask> AssignedTasks= new();

    [ObservableProperty] public DateTime selectedDate = DateTime.Today;
    [ObservableProperty] public DateTime minDate = DateTime.Today;
    [ObservableProperty] public DateTime maxDate = DateTime.MaxValue;

    [ObservableProperty] public string selectedSunWatchPersonale;
    [ObservableProperty] public string selectedTask;
    [ObservableProperty] public string selectedDay;
    [ObservableProperty] public string assignedTaskStatusMessage;
    [ObservableProperty] public string emailOrionFirstShift;
    [ObservableProperty] public string emailOrionSecondShift;
    [ObservableProperty] public string emailOrionThirdShift;
    [ObservableProperty] public string momsMccFirstShift;
    [ObservableProperty] public string momsMccSecondShift;
    [ObservableProperty] public string momsMccThirdShift;
    [ObservableProperty] public string scadaFirstShift;
    [ObservableProperty] public string scadaSecondShift;
    [ObservableProperty] public string scadaThirdShift;
    [ObservableProperty] public string p2000FirstShift;
    [ObservableProperty] public string p2000SecondShift;
    [ObservableProperty] public string p2000ThirdShift;


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
        DisplayTasks();
    }

   public void PopulateSunWatchPersonaleList()
    {
        SunWatchPersonales = SunWatchPersonaleRepo.GetSunWatchPersonale();
        if(SunWatchPersonales != null) 
        {
            foreach(SunWatchPersonale sunwatchpersonale in SunWatchPersonales)
            {
                sunWatchPersonaleList.Add(sunwatchpersonale.SunWatch_personale_name);
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

    public enum Shifts
    {
        First=0,
        Second=1,
        Third=2,
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
        EmailOrionFirstShift = "";
        EmailOrionSecondShift = "";
        EmailOrionThirdShift = "";
        MomsMccFirstShift = "";
        MomsMccSecondShift = "";
        MomsMccThirdShift = "";
        ScadaFirstShift = "";
        ScadaSecondShift = "";
        ScadaThirdShift = "";
        P2000FirstShift = "";
        P2000SecondShift = "";
        P2000ThirdShift = "";

        SelectedDay = SelectedDate.ToString("dddd");
        foreach (var assignedtask in AssignedTaskList)
        {
            SunWatchPersonaleQueryByName = SunWatchPersonaleRepo.QueryBySunWatchPersonaleName(assignedtask.Assigned_task_sunwatch_personale_name);
            foreach (SunWatchPersonale sunWatchPersonale in SunWatchPersonaleQueryByName)
            {
                SunWatchPersonaleShift = sunWatchPersonale.SunWatch_personale_shift;
            }

            foreach(var task in AllTasks )
            {
                foreach (Shifts shift in Enum.GetValues(typeof(Shifts)))
                {
                    if(SunWatchPersonaleShift.Equals("First") && (assignedtask.Assigned_task_name.Equals("Emails/Orion")) && (assignedtask.Assigned_task_day.Equals(SelectedDay)) ) 
                    {
                        EmailOrionFirstShift = assignedtask.Assigned_task_sunwatch_personale_name;
                    }

                    else if(SunWatchPersonaleShift.Equals("Second") && (assignedtask.Assigned_task_name.Equals("Emails/Orion")) && (assignedtask.Assigned_task_day.Equals(SelectedDay)) )
                    {
                        EmailOrionSecondShift = assignedtask.Assigned_task_sunwatch_personale_name;
                    }

                    else if(SunWatchPersonaleShift.Equals("Third") && (assignedtask.Assigned_task_name.Equals("Emails/Orion")) && (assignedtask.Assigned_task_day.Equals(SelectedDay)))
                    {
                        EmailOrionThirdShift = assignedtask.Assigned_task_sunwatch_personale_name;
                    }

                    else if (SunWatchPersonaleShift.Equals("First") && (assignedtask.Assigned_task_name.Equals("MOMs/MCC")) && (assignedtask.Assigned_task_day.Equals(SelectedDay)))
                    {
                        momsMccFirstShift= assignedtask.Assigned_task_sunwatch_personale_name;
                    }

                    else if (SunWatchPersonaleShift.Equals("Second") && (assignedtask.Assigned_task_name.Equals("MOMs/MCC")) && (assignedtask.Assigned_task_day.Equals(SelectedDay)))
                    {
                        momsMccSecondShift = assignedtask.Assigned_task_sunwatch_personale_name;
                    }

                    else if (SunWatchPersonaleShift.Equals("Third") && (assignedtask.Assigned_task_name.Equals("MOMs/MCC")) && (assignedtask.Assigned_task_day.Equals(SelectedDay)))
                    {
                        momsMccThirdShift = assignedtask.Assigned_task_sunwatch_personale_name;
                    }

                    else if (SunWatchPersonaleShift.Equals("First") && (assignedtask.Assigned_task_name.Equals("SCADA")) && (assignedtask.Assigned_task_day.Equals(SelectedDay)))
                    {
                        scadaFirstShift = assignedtask.Assigned_task_sunwatch_personale_name;
                    }

                    else if (SunWatchPersonaleShift.Equals("Second") && (assignedtask.Assigned_task_name.Equals("SCADA")) && (assignedtask.Assigned_task_day.Equals(SelectedDay)))
                    {
                        scadaSecondShift = assignedtask.Assigned_task_sunwatch_personale_name;
                    }

                    else if (SunWatchPersonaleShift.Equals("Third") && (assignedtask.Assigned_task_name.Equals("SCADA")) && (assignedtask.Assigned_task_day.Equals(SelectedDay)))
                    {
                        scadaThirdShift = assignedtask.Assigned_task_sunwatch_personale_name;
                    }

                    else if (SunWatchPersonaleShift.Equals("First") && (assignedtask.Assigned_task_name.Equals("P2000")) && (assignedtask.Assigned_task_day.Equals(SelectedDay)))
                    {
                        p2000FirstShift = assignedtask.Assigned_task_sunwatch_personale_name;
                    }

                    else if (SunWatchPersonaleShift.Equals("Second") && (assignedtask.Assigned_task_name.Equals("P2000")) && (assignedtask.Assigned_task_day.Equals(SelectedDay)))
                    {
                        p2000SecondShift = assignedtask.Assigned_task_sunwatch_personale_name;
                    }

                    else if (SunWatchPersonaleShift.Equals("Third") && (assignedtask.Assigned_task_name.Equals("P2000")) && (assignedtask.Assigned_task_day.Equals(SelectedDay)))
                    {
                        p2000ThirdShift = assignedtask.Assigned_task_sunwatch_personale_name;
                    }
                }
            }

        }


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
        DisplayTasks();
    }

    [RelayCommand]
    public async void RemoveAssignedTask()
    {
        int AssignedTaskId;
        if(SelectedAssignedTask==null)
        {
            AssignedTaskStatusMessage = "Error: Failed To Remove Task.";
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
                DisplayTasks();
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

