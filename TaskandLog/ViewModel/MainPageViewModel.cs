﻿using CommunityToolkit.Mvvm.ComponentModel;
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

	public LogTypeRepository LogTypeRepo=new();
	public LogEntryRepository LogEntryRepo=new();
    public List<LogType> LogTypes = new();
    public List<LogEntry> LogEntries = new();

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
    public string removeLogEntryStatusMessage;

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

		}
	
	}

	[RelayCommand]
	async void AddLogEntry()
	{
		
		try
		{
			NewLogEntry = Date + " " + SelectedLogType + " " + Number + " " + Description;
			LogEntryRepo.AddLogEntry(NewLogEntry);
			NewLogEntry = "";
			NewLogEntryStatusMessage = "Success: Log Added.";
			await Task.Delay(2000);
			NewLogEntryStatusMessage = "";
		}

		catch (Exception ex)
		{
			NewLogEntryStatusMessage = "Error: Failed To Add Log. " + ex.Message;
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
	
