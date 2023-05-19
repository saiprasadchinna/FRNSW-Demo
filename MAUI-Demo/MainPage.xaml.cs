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
        foreach (var s in GetItems(50))
        {
            myItems.Add(s);
        }

        myCollectionView.ItemsSource = myItems;
        myCollectionView.RemainingItemsThreshold = 5;
        myCollectionView.RemainingItemsThresholdReached += MyCollectionView_RemainingItemsThresholdReached;
    }
    private void MyCollectionView_RemainingItemsThresholdReached(object sender, EventArgs e)
    {
        foreach (var s in GetItems(15))
        {
            myItems.Add(s);
        }
    }
    private List<string> GetItems(int numberOfItems)
    {
        var resultList = new List<string>();

        for (var i = 0; i <= numberOfItems; i++)
        {
            resultList.Add(randomizer.Next(10000, 99999).ToString());
        }

        return resultList;
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }

    private void GoBackTabbedPage_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new TabbedDemo());
    }

    private void EmployeeListPage_Clicked(object sender, EventArgs e)
    {
        var bookingService = new BookingService();
        var listviewmodel = new EmployeeListViewModel(bookingService);
        Navigation.PushModalAsync(new EmployeeView(listviewmodel));
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

        Navigation.PushModalAsync(new OktaSignIn(authclient, new HttpClient()));
    }
}

