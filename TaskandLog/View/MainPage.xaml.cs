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
    public LogEmailDistributionRepository LogEmailDistributionRepo= new();
    public List<LogEmailDistribution> LogEmailDistributionList = new();
    public string UpdateDate { get; set;}
	public string UpdateType { get; set; }
	public string UpdateDescription { get; set; }
	public string UpdateLog { get; set; }

    public Outlook.Application ObjApp = new();
    public Outlook.MailItem Mail { get; set; } = null;

    public string PassdownLogTemplate { get; set; } = Path.Combine(FileSystem.AppDataDirectory, "PassdownLogTemplate.msg");
    public string To { get; set; }
    public string Cc { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    

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

    private async void UpdateButton_Clicked(object sender, EventArgs e)
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

    private void EmailPassdownLog_ButtonClicked(object sender, EventArgs e)
    {
        LogEmailDistributionList = LogEmailDistributionRepo.GetLogEmailDistribution();
        foreach (LogEmailDistribution logEmailDistribution in LogEmailDistributionList)
        {
            To = logEmailDistribution.Log_email_distribution_to;
        }
        Subject = "SunWatch Passdown Log Email Notification";

        LogEntries=LogEntryRepo.GetLogEntries();
        Body = "<font size=4> Team, " + "<br>" + "<br>" + " Below are the current active items on the Passdown Log: "+ "<br>"+"<br>";
        foreach (var logEntry in LogEntries)
        {
            if(logEntry.Log_entry_id!=1)
            {
                Body += logEntry.Log_entry_id + " - " + logEntry.Log_entry_date_time + " - " + logEntry.Log_entry_type + " - " + logEntry.Log_entry_description + "<br>";
            }
            
        }

        try
        {
            Mail = (Outlook.MailItem)ObjApp.CreateItemFromTemplate(PassdownLogTemplate);
            Mail.To = To;
            Mail.Subject = Subject;
            Mail.HTMLBody= Body;
            Mail.Display();
            Mail = null;
        }
        catch (Exception ex)
        {
            DisplayAlert("Alert", "Close MOE, make sure Outlook is running, and try again. " + ex.Message, "close");
        }
       
    }
}

