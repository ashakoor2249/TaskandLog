using TaskandLog.ViewModel;

namespace TaskandLog.View;

public partial class DatabasePage : ContentPage
{
	public DatabasePage(DatabasePageViewModel databasePageViewModel)
	{
		InitializeComponent();
		BindingContext= databasePageViewModel;
	}
}