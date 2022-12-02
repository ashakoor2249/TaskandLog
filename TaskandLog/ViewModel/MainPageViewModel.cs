using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TaskandLog.Database;
using TaskandLog.TableRepositories;
using TaskandLog.Tables;



namespace TaskandLog.ViewModel;

public partial class MainPageViewModel : ObservableObject
{
    readonly DB Database = new();
    public LogTypeRepository LogTypeRepo = new();
    public LogEntryRepository LogEntryRepo = new();
    public List<LogType> LogTypes = new();
    public List<LogEntry> LogEntries = new();

    [ObservableProperty]public int updatedLogEntryId;
	[ObservableProperty] public string updatedLogEntryDateAndTime;
	[ObservableProperty] public string updatedLogEntryNumber;
    [ObservableProperty] public string updatedLogEntryDescription;

    [ObservableProperty]public DateTime selectedDate=DateTime.Today;
	[ObservableProperty]public DateTime minDate=DateTime.Today;
	[ObservableProperty]public DateTime maxDate=DateTime.MaxValue;
    [ObservableProperty]public TimeSpan selectedTime;
    [ObservableProperty]public string dateAndTime;

    [ObservableProperty] ObservableCollection<string> logTypeList=new();
    [ObservableProperty]public string selectedLogType;

    [ObservableProperty]public string number; 

	[ObservableProperty]public string description;

	[ObservableProperty]public string newLogEntryStatusMessage;
    [ObservableProperty] public string removeLogEntryStatusMessage;
    public ObservableCollection<LogEntry> Logs { get; set; } = new();
	[ObservableProperty]public LogEntry selectedLog;

    public MainPageViewModel()
	{
		if (Database.DatabaseConnection == null)
		{
            Database.DatabaseInit();
        }
		PopulateTypeList();
		PoulateLogEntriesList();
	}

	async void PopulateTypeList()
	{
		try
		{
            LogTypes = LogTypeRepo.GetLogTypes();
            foreach (LogType logtype in LogTypes)
            {
                logTypeList.Add(logtype.Log_type_name);
            }
        }

		catch (Exception ex)
		{
			NewLogEntryStatusMessage = "Error: Failed To Add Log. " + ex.Message;
			await Task.Delay(2000);
			NewLogEntryStatusMessage = "";
		}
	}

	[RelayCommand]
	async void AddLogEntry()
	{
		try
		{
			DateAndTime = SelectedDate.ToString("MM/dd/yyy") + "/" + SelectedTime;
            LogEntryRepo.AddLogEntry(DateAndTime, Number, Description);
			DateAndTime = "";
			NewLogEntryStatusMessage = "Success: Log Added.";
			await Task.Delay(2000);
			NewLogEntryStatusMessage = "";
            PoulateLogEntriesList();
        }

		catch (Exception ex)
		{
			NewLogEntryStatusMessage = "Error: Failed To Add Log. " + ex.Message;
			await Task.Delay(2000);
			NewLogEntryStatusMessage = "";
		}
	}

	[RelayCommand]
	async void RemoveLogEntry()
	{
		int LogId;
		if(SelectedLog==null)
		{
            NewLogEntryStatusMessage = "Error: Failed To Remove Log. ";
            await Task.Delay(2000);
            NewLogEntryStatusMessage = "";
            return;
        }
        LogId = SelectedLog.Log_entry_id;
		if(LogId==1)
		{
			return;
		}
        try
		{
            LogEntryRepo.DeleteLogEntry(LogId);
            PoulateLogEntriesList();
        }
		catch (Exception ex)
		{
            NewLogEntryStatusMessage = "Error: Failed To Remove Log. " + ex.Message;
            await Task.Delay(2000);
            NewLogEntryStatusMessage = "";
        }
    }


	[RelayCommand]
	public void PoulateLogEntriesList()
	{

        LogEntries.Clear();
		LogEntries=LogEntryRepo.GetLogEntries();

		if(Logs.Count!=0)
		{
			Logs.Clear();
		}

		foreach (var log in LogEntries) 
		{
			Logs.Add(log);
		}
    }

	[RelayCommand]
	async void EmailLogEntries()
	{
	
	}

	[RelayCommand]
	public void ClearLogInputs()
	{
        logTypeList?.Clear();
		PopulateTypeList();
		Number = "";
		Description= "";
	}

}
	
