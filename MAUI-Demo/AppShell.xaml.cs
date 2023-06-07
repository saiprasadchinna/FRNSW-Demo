using MAUI_Demo.Auth0;
using MAUI_Demo.MVVM.ViewModels;
using MAUI_Demo.MVVM.Views;
using MAUI_Demo.Views.Startup;
using MAUI_Demo_Service.Data;
using System.Diagnostics;
using System.Windows.Input;

namespace MAUI_Demo;

public partial class AppShell : Shell
{
    public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();
    public ICommand HelpCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));



    public bool IsLogged
    {
        get => (bool)GetValue(IsLoggedProperty);
        set => SetValue(IsLoggedProperty, value);
    }

    public static readonly BindableProperty IsLoggedProperty =
        BindableProperty.Create("IsLogged", typeof(bool), typeof(AppShell), false, propertyChanged: IsLogged_PropertyChanged);

    private static void IsLogged_PropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        //handle log in/log out event
        if ((bool)newValue)
        {
            //RegisterRoutes();
        }
        //user just logged in logic
        else
        {

        }
        //user just logged out logic
    }

    public bool IsUnAuthorize
    {
        get => (bool)GetValue(IsUnAuthorizeProperty);
        set => SetValue(IsUnAuthorizeProperty, value);
    }

    public static readonly BindableProperty IsUnAuthorizeProperty =
        BindableProperty.Create("IsUnAuthorize", typeof(bool), typeof(AppShell), true, propertyChanged: IsUnAuthorize_PropertyChanged);

    private static void IsUnAuthorize_PropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        //handle log in/log out event
        if ((bool)newValue)
        {
            //RegisterRoutes();
        }
        //user just logged in logic
        else
        {

        }
        //user just logged out logic
    }
    public AppShell()
    {
        InitializeComponent();
        //RegisterRoutes();
        this.BindingContext = new AppShellViewModel();
    }
    void RegisterRoutes()
    {
        Routes.Add("MainPage", typeof(MainPage));
        Routes.Add("bookingsview", typeof(BookingsView));
        Routes.Add("oktasignin", typeof(OktaSignIn));

        if (!string.IsNullOrEmpty(TokenHolder.AccessToken))
        {
            Routes.Add("pageroles", typeof(PageRoleMapping));
        }

        foreach (var item in Routes)
        {
            Routing.RegisterRoute(item.Key, item.Value);
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        //logoutUser();
        //OktaSignIn obj = new OktaSignIn();
        //obj.OnLogoutCustomClicked(sender, e);
        //logoutOktaUserUsingToken();
        var filename = Process.GetCurrentProcess().MainModule.FileName;
        // Start a new instance of the application

        Process.Start(filename);
        Application.Current.Quit();
        //Navigation.PushAsync(new LoginPage());
    }

    public async void logoutUser()
    {
        //OktaSignIn obj = new OktaSignIn();
        //obj.logoutUser();
    }
    public Task logoutOktaUserUsingToken()
    {
        UserService userService = new UserService();
        
        return Task.Run(() => userService.OktaRevokeAccessToken(TokenHolder.AccessToken));
    }

    //protected override void OnAppearing()
    //{
    //    base.OnAppearing();

    //    Shell.SetTabBarIsVisible(this, false);
    //}
}
