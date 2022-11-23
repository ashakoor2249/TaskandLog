using TaskandLog.ViewModel;

namespace TaskandLog;

public partial class MainPage : ContentPage
{
    private readonly MainPageViewModel viewModel=new();

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
			if(selectLogType.SelectedItem.ToString().Equals("Incident"))
			{
				selectNumber.Text = "I-";
			}
			else if(selectLogType.SelectedItem.ToString().Equals("ESR"))
			{
				selectNumber.Text = "E-";
			}

            else if (selectLogType.SelectedItem.ToString().Equals("Work Order"))
            {
                selectNumber.Text = "WO-";
            }

            else if (selectLogType.SelectedItem.ToString().Equals("MAF"))
            {
                selectNumber.Text = "M-";
            }

			else
			{
				selectNumber.Text = "";
			}
			
        }
	}
}

