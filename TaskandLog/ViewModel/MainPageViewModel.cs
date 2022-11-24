using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SQLite;
using System.Collections.ObjectModel;
using System.Data;
using TaskandLog.Database;
using TaskandLog.TableRepositories;
using TaskandLog.Tables;

namespace TaskandLog.ViewModel;

public partial class MainPageViewModel : ObservableObject
{

	public LogTypeRepository LogTypeRepo=new();
	public LogEntryRepository LogEntryRepo=new();
    public List<LogType> LogTypes = new();
    public List<LogEntry> LogEntries = new();

	[ObservableProperty]
	public DateTime selectedDate=DateTime.Today;

	[ObservableProperty]
	public DateTime minDate=DateTime.Today;

	[ObservableProperty]
	public DateTime maxDate=DateTime.MaxValue;

	[ObservableProperty]
	public TimeSpan selectedTime;

	[ObservableProperty]
	public string selectedLogType;

	[ObservableProperty]
	public string number; 

	[ObservableProperty]
	public string description;

	[ObservableProperty]
	public string newLogEntry;

	[ObservableProperty]
	ObservableCollection<string> logEntriesList= new ObservableCollection<string>();

	[ObservableProperty]
	public string newLogEntryStatusMessage;

    [ObservableProperty]
    public string removeLogEntryStatusMessage;

    [ObservableProperty]
	ObservableCollection<string> logTypeList;

	public MainPageViewModel()
	{
		logTypeList = new ObservableCollection<string>();
		if (DB.DatabaseConnection == null)
			DB.DatabaseInit();

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
			
			NewLogEntry = SelectedDate.ToLongDateString() +"/"+ SelectedTime+ " " + Number + " " + Description;
			LogEntryRepo.AddLogEntry(NewLogEntry);
			NewLogEntry = "";
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
		
	}

	[RelayCommand]
	async void PoulateLogEntriesList()
	{
        logEntriesList.Clear();
        try
		{
			LogEntries = LogEntryRepo.GetLogEntries();
			if (LogEntries != null)
			{
				foreach (LogEntry logentry in LogEntries)
				{
					logEntriesList.Add(logentry.Log_entry);
				}
			}
		}
		catch (Exception ex)
		{
			NewLogEntryStatusMessage = "Error: " + ex.Message;
			await Task.Delay(2000);
			NewLogEntryStatusMessage = "";
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
	
