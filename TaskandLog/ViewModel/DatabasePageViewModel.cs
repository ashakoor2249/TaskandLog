using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskandLog.TableRepositories;
using TaskandLog.Tables;

namespace TaskandLog.ViewModel;

public partial class DatabasePageViewModel : ObservableObject
{
    public LogTypeRepository LogTypeRepo = new();
    public SunWatchPersonaleRepository SunWatchPersonaleRepo = new();
    public TasksRepository TasksRepo=new();

    public LogType LogType = new();
    public SunWatchPersonale SunWatchPersonale = new();
    public Tasks Tasks=new();


    public List<LogType> LogTypes = new();
    public List<SunWatchPersonale> SunWatchPersonales = new();
    public List<Tasks> AllTasks = new();

    [ObservableProperty] public string newType;
    [ObservableProperty] public string removeType;
    [ObservableProperty] public string newTypeStatusMessage;
    [ObservableProperty] public string removeTypeStatusMessage;
    [ObservableProperty] ObservableCollection<string> logTypeList = new();

    [ObservableProperty] public string newSunWatchPersonaleName;
    [ObservableProperty] public string newSunWatchPersonaleShift;
    [ObservableProperty] public string newSunWatchPersonaleDaysWorked;
    [ObservableProperty] public string removeSunWatchPersonale;
    [ObservableProperty] public string newSunWatchPersonaleStatusMessage;
    [ObservableProperty] public string removeSunWatchPersonaleStatusMessage;
    [ObservableProperty] ObservableCollection<string> sunWatchPersonaleList = new();

    [ObservableProperty] public string newTask;
    [ObservableProperty] public string removeTask;
    [ObservableProperty] public string newTaskStatusMesssage;
    [ObservableProperty] public string removeTaskStatusMessage;
    [ObservableProperty] ObservableCollection<string> taskList = new();

    public DatabasePageViewModel() 
    {
        
    }

    [RelayCommand]
    async void AddNewType()
    {
        try
        {
            LogTypeRepo.AddLogType(NewType);
            NewType = "";
            NewTypeStatusMessage = "Success: Type Added";
            await Task.Delay(2000);
            NewTypeStatusMessage = "";

        }
        catch(Exception ex)
        {
            NewTypeStatusMessage = "Error: Failed to add type. " + ex.Message;
            await Task.Delay(2000);
            NewTypeStatusMessage = "";
        }

    }

    [RelayCommand]
    async void DeleteType()
    {
        try
        {
            LogTypeRepo.DeleteLogType(Int32.Parse(RemoveType));
            RemoveType = "";
            RemoveTypeStatusMessage = "Success: Type Removed";
            await Task.Delay(2000);
            RemoveTypeStatusMessage = "";

        }
        catch (Exception ex)
        {
            RemoveTypeStatusMessage = "Error: Failed to remove type. " + ex.Message;
            await Task.Delay(2000);
            RemoveTypeStatusMessage = "";
        }
    }

    [RelayCommand]
    public void GetAllTypes()
    {
        logTypeList.Clear();
        LogTypes.Clear();
        try
        {
            LogTypes = LogTypeRepo.GetLogTypes();
            foreach (LogType logTypes in LogTypes)
            {
                logTypeList.Add(logTypes.Log_type_id + " " + logTypes.Log_type_name);
            }
        }
        catch (Exception ex)
        {
            logTypeList.Add("No Data in database. " + ex.Message);
        }

    }

    [RelayCommand]
    async void AddNewSunWatchPersonale()
    {
        try
        {
            SunWatchPersonaleRepo.AddSunWatchPersonale(NewSunWatchPersonaleName, NewSunWatchPersonaleShift, NewSunWatchPersonaleDaysWorked);
            NewSunWatchPersonaleName = "";
            NewSunWatchPersonaleShift = "";
            NewSunWatchPersonaleDaysWorked = "";
            NewSunWatchPersonaleStatusMessage = "Success: SunWatch Personale Added";
            await Task.Delay(2000);
            NewSunWatchPersonaleStatusMessage = "";

        }
        catch (Exception ex)
        {
            NewSunWatchPersonaleStatusMessage = "Error: Failed to add SunWatch Personale. " + ex.Message;
            await Task.Delay(2000);
            NewSunWatchPersonaleStatusMessage = "";
        }

    }

    [RelayCommand]
    async void DeleteSunWatchPersonale()
    {
        try
        {
            SunWatchPersonaleRepo.DeleteSunWatchPersonale(Int32.Parse(RemoveSunWatchPersonale));
            RemoveSunWatchPersonale = "";
            RemoveSunWatchPersonaleStatusMessage = "Success: SunWatch Personale Removed";
            await Task.Delay(2000);
            RemoveSunWatchPersonaleStatusMessage = "";

        }
        catch (Exception ex)
        {
            RemoveSunWatchPersonaleStatusMessage = "Error: Failed to remove SunWatch Personale. " + ex.Message;
            await Task.Delay(2000);
            RemoveSunWatchPersonaleStatusMessage = "";
        }

    }

    [RelayCommand]
    public void GetAllSunWatchPersonale()
    {
        sunWatchPersonaleList.Clear();
        SunWatchPersonales.Clear();
        try
        {
            SunWatchPersonales = SunWatchPersonaleRepo.GetSunWatchPersonale();
            foreach (SunWatchPersonale sunwatchpersonale in SunWatchPersonales)
            {
                sunWatchPersonaleList.Add(sunwatchpersonale.SunWatch_personale_id+ " " + sunwatchpersonale.SunWatch_personale_name + " "+ sunwatchpersonale.SunWatch_personale_shift + " "+ 
                                sunwatchpersonale.SunWatch_personale_days_worked);
            }
        }
        catch (Exception ex)
        {
            sunWatchPersonaleList.Add("No Data in database. " + ex.Message);
        }

    }

    [RelayCommand]
    async void AddNewTask()
    {
        try
        {
            TasksRepo.AddTask(NewTask);
            NewTask= "";
            NewTaskStatusMesssage = "Success: Task Added";
            await Task.Delay(2000);
            NewTaskStatusMesssage = "";

        }
        catch (Exception ex)
        {
            NewTaskStatusMesssage = "Error: Failed to add task. " + ex.Message;
            await Task.Delay(2000);
            NewTaskStatusMesssage = "";
        }


    }
    [RelayCommand]
    async void DeleteTask ()
    {
        try
        {
            TasksRepo.DeleteTask(Int32.Parse(RemoveTask));
            RemoveTaskStatusMessage = "";
            RemoveTaskStatusMessage = "Success: Task Removed";
            await Task.Delay(2000);
            RemoveTaskStatusMessage = "";

        }
        catch (Exception ex)
        {
            RemoveTaskStatusMessage = "Error: Failed to remove Task. " + ex.Message;
            await Task.Delay(2000);
            RemoveTaskStatusMessage = "";
        }

    }
    [RelayCommand]
    public void GetAllTasks()
    {
        taskList.Clear();
        AllTasks.Clear();
        try
        {
            AllTasks = TasksRepo.GetTasks();
            foreach (Tasks tasks in AllTasks)
            {
                taskList.Add(tasks.Task_id + " " + tasks.Task_name);
            }
        }
        catch (Exception ex)
        {
            NewTaskStatusMesssage = "Error: Failed to retrieve tasks: "+ex.Message;
        }

    }

}

