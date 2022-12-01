using TaskandLog.ViewModel;
using TaskandLog.SharedComponents;
using System.Diagnostics;
using TaskandLog.Tables;
using TaskandLog.TableRepositories;

namespace TaskandLog;

public partial class MainPage : ContentPage
{
    readonly Methods methods = new ();
	readonly LogEntryRepository LogEntryRepo = new();
    public List<LogEntry> LogEntries = new();
    public string UpdateDate { get; set;}
	public string UpdateType { get; set; }
	public string UpdateDescription { get; set; }


	public MainPage(MainPageViewModel MainPageViewModel)
	{
		InitializeComponent();
		BindingContext = MainPageViewModel;

	}

	private void SelectLogType_SelectedIndexChanged(object sender, EventArgs e)
	{
		int selectedIndex = selectLogType.SelectedIndex;

		if (selectedIndex != -1)
		{
            selectNumber.Text=methods.SetPrefix(selectLogType.SelectedItem.ToString());
        }
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {

		if(selectLog.SelectedItem == null)
		{
           await DisplayAlert("Check", "Please select a log entry to update.", "close");
        }

		else
		{
            LogEntry Logs = (LogEntry)selectLog.SelectedItem;
            UpdateDate = await DisplayPromptAsync("Date Update", "Enter new Date/Time or click Cancel to keep.", initialValue: Logs.Log_entry_date_time);
            UpdateType = await DisplayPromptAsync("Type",  "Enter new Type or click Cancel to keep.", initialValue: Logs.Log_entry_type);
            UpdateDescription = await DisplayPromptAsync("Description Update", "Enter new Description or click Cancel to keep.", initialValue: Logs.Log_entry_description);
			LogEntryRepo.UpdateLogEntry(Logs.Log_entry_id, UpdateDate, UpdateType, UpdateDescription);

            LogEntries.Clear();
            LogEntries = LogEntryRepo.GetLogEntries();
			selectLog.ItemsSource = LogEntries;


        }
		//await DisplayAlert("Check", Logs.Log_entry_id.ToString(), "close");
    }
}

