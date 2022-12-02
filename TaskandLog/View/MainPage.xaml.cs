using TaskandLog.ViewModel;
using TaskandLog.SharedComponents;
using System.Diagnostics;
using TaskandLog.Tables;
using TaskandLog.TableRepositories;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace TaskandLog;

public partial class MainPage : ContentPage
{
    readonly Methods methods = new ();
	readonly LogEntryRepository LogEntryRepo = new();
    public List<LogEntry> LogEntries = new();
    public string UpdateDate { get; set;}
	public string UpdateType { get; set; }
	public string UpdateDescription { get; set; }
	public string UpdateLog { get; set; }

    public Outlook.Application ObjApp = new();
    public Outlook.MailItem Mail { get; set; } = null;
    public string To { get; set; }
    public string Cc { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }

    public string PassdownLogTemplate { get; set; } = Path.Combine(FileSystem.AppDataDirectory, "PassdownLogTemplate.msg");

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

		if(selectLog.SelectedItem != null)
		{
            LogEntry Logs = (LogEntry)selectLog.SelectedItem;
			if(Logs.Log_entry_id==1)
			{
				return;
			}

            UpdateLog = await DisplayPromptAsync("Prompt", "Update Log Entry(Keep spaces in between)", initialValue: Logs.Log_entry_date_time + " " + Logs.Log_entry_type + " " + Logs.Log_entry_description);
			if(UpdateLog!= null)
			{
                var split = UpdateLog.Split(' ', 3);
                UpdateDate = split[0];
                UpdateType = split[1];
                UpdateDescription = split[2];
                LogEntryRepo.UpdateLogEntry(Logs.Log_entry_id, UpdateDate, UpdateType, UpdateDescription);
                LogEntries.Clear();
                LogEntries = LogEntryRepo.GetLogEntries();
                selectLog.ItemsSource = LogEntries;
            }
			else
			return;
		
        }

		else
		return;
		
    }

    private void Button_Clicked_EmailPassdownLog(object sender, EventArgs e)
    {

        try
        {
            Mail = (Outlook.MailItem)ObjApp.CreateItemFromTemplate(PassdownLogTemplate);
        }
        catch (Exception ex)
        {
            DisplayAlert("Alert", "Close MOE, make sure Outlook is running, and try again. " + ex.Message, "close");
        }
       
    }
}

