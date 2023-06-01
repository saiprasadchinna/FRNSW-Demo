using MAUI_Demo.Auth0;
using MAUI_Demo.MVVM.ViewModels;
using MAUI_Demo.MVVM.Views;
using MAUI_Demo_Service.Data;
//using MauiApp2;
using System.Collections.ObjectModel;

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
    private void GoBackTabbedPage_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new TabbedDemo());
    }

    private void BtnOktaLogin_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new OktaSignIn());
    }
}

