using MAUI_Demo.Auth0;
using Microsoft.Maui.Controls.PlatformConfiguration;
//using MauiApp2;

namespace MAUI_Demo;

public partial class MainPage : ContentPage
{  
    public MainPage()
    {
        InitializeComponent();
        if (!string.IsNullOrEmpty(TokenHolder.AccessToken))
        {
            userId.Text = "User Name: " + TokenHolder.UserName;
        }
        else
        {
            userId.Text = "";
        }
        var adminDashboardInfo = AppShell.Current.Items.Where(f => f.Route == nameof(MainPage)).FirstOrDefault();
        if (adminDashboardInfo != null) AppShell.Current.Items.Remove(adminDashboardInfo);
    }
   
}

