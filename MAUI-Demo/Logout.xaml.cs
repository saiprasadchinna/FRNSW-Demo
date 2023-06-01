using System.Diagnostics;

namespace MAUI_Demo;

public partial class Logout : ContentPage
{
    OktaSignIn _oktaLogout;
    public Logout()
	{
		InitializeComponent();
        _oktaLogout = new OktaSignIn();
        _oktaLogout.logoutUser();
        (Shell.Current as AppShell).IsLogged = false;
        (Shell.Current as AppShell).IsUnAuthorize = true;
    }
}