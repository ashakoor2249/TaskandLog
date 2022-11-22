using TaskandLog.ViewModel;

namespace TaskandLog;

public partial class MainPage : ContentPage
{
	

	public MainPage(MainPageViewModel MainPageViewModel)
	{
		InitializeComponent();
		BindingContext=MainPageViewModel;

	}

	private void SelectLogType_SelectedIndexChanged(object sender, EventArgs e)
	{

    }
}

