using TaskandLog.ViewModel;
using TaskandLog.SharedComponents;

namespace TaskandLog;

public partial class MainPage : ContentPage
{
    readonly Methods methods = new Methods();

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
}

