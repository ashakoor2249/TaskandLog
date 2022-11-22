using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SQLite;
using System.Collections.ObjectModel;
using TaskandLog.Database;
using TaskandLog.TableRepositories;
using TaskandLog.Tables;
using TaskandLog.SharedComponents;

namespace TaskandLog.ViewModel;

public partial class MainPageViewModel : ObservableObject
{
	readonly SharedComponents.SharedComponents SharedComponents;

	[ObservableProperty]
	public string date = DateTime.Now.ToString("dddd, MMMM dd, yyyy / HH:mm");

	[ObservableProperty]
	public string selectedLogType;

	[ObservableProperty]
	public string number; 

	[ObservableProperty]
	public string description;

	[ObservableProperty]
	public string newLogEntry;

	[ObservableProperty]
	public string newLogEntryStatusMessage;

	[ObservableProperty]
	ObservableCollection<string> logTypeList;

	public MainPageViewModel()
	{
		logTypeList = new ObservableCollection<string>();
		if (DB.DatabaseConnection == null)
			DB.DatabaseInit();

		PopulateTypeList();
	}

	public void PopulateTypeList()
	{
		SharedComponents.LogTypes = SharedComponents.LogTypeRepo.GetLogTypes();
		foreach(LogType logtype in SharedComponents.LogTypes)
		{
			logTypeList.Add(logtype.Log_type_id.ToString() +" "+ logtype.Log_type_name);
		}
	}

	[RelayCommand]
	async void AddLogEntry()
	{
		try
		{
			SharedComponents.LogEntryRepo.AddLogEntry(NewLogEntry);
			NewLogEntry = "";
			NewLogEntryStatusMessage = "Success: Region Added.";
			await Task.Delay(2000);
			NewLogEntryStatusMessage = "";
		}

		catch (Exception ex)
		{
			NewLogEntryStatusMessage = "Error: Failed to add region. " + ex.Message;
			await Task.Delay(2000);
			NewLogEntryStatusMessage = "";
		}
	}

	[RelayCommand]
	async void RemoveLog()
	{

	}

	[RelayCommand]
	async void ClearInputs()
	{

	}

	[RelayCommand]
	async void EmailLog()
	{

	}
	[RelayCommand]
	async void RefreshLog()
	{

	}
}
	
