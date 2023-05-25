using MAUI_Demo.Auth0;
using MAUI_Demo.MVVM.ViewModels;
using MAUI_Demo.MVVM.Views;
using MAUI_Demo_Service.Data;
//using MauiApp2;
using System.Collections.ObjectModel;

namespace MAUI_Demo;

public partial class MainPage : ContentPage
{
    int count = 0;
    private readonly Random randomizer = new Random();
    private readonly ObservableCollection<string> myItems = new ObservableCollection<string>();

    public MainPage()
    {
        InitializeComponent();
        //AdminMenuItems.IsEnabled = true;
    }
 

    private void GoBackTabbedPage_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new TabbedDemo());
    }

 
    private void BtnOktaLogin_Clicked(object sender, EventArgs e)
    {
        Auth0Client authclient= new Auth0Client(new()
        {
            Domain = "dev-17683470.okta.com",
            ClientId = "0oa912ox83mA6vxCh5d7",
            Scope = "openid profile offline_access",
            Audience = "https://dev-17683470.okta.com",
#if WINDOWS
                    RedirectUri = "https://localhost:44380/WeatherForecast"
#else
            RedirectUri = "myapp://callback"
#endif
        });

        Navigation.PushAsync(new OktaSignIn(authclient, new HttpClient()));
    }
}

