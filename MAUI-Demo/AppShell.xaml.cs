using MAUI_Demo.Auth0;
using MAUI_Demo.ViewModels;
using MAUI_Demo_Service.Data;
using System.Diagnostics;

namespace MAUI_Demo;

public partial class AppShell : Shell
{

    public AppShell()
    {
        InitializeComponent();
        this.BindingContext = new AppShellViewModel();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        //logoutOktaUserUsingToken();
        var filename = Process.GetCurrentProcess().MainModule.FileName;
        // Start a new instance of the application

        Process.Start(filename);
        Application.Current.Quit();
    }
    public Task logoutOktaUserUsingToken()
    {
        UserService userService = new UserService();
        
        return Task.Run(() => userService.OktaRevokeAccessToken(TokenHolder.AccessToken));
    }
}
