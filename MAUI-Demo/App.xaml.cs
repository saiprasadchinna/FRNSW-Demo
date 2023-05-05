using MAUI_Demo.MVVM.Views;

namespace MAUI_Demo;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
        var navPage = new NavigationPage(new MainPage());
        MainPage = navPage;
    }
}
