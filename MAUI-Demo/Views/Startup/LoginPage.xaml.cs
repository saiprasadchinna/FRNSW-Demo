using MAUI_Demo.Auth0;
using MAUI_Demo.RolePages;
using MAUI_Demo_Service.Data;
using MAUI_Demo_Service.Models;
using Microsoft.Maui.Controls;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MAUI_Demo.Views.Startup;

public partial class LoginPage : ContentPage
{
    public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();

    private readonly Auth0Client auth0Client;
    private readonly UserService _userService;
    public LoginPage()
    {
        InitializeComponent();
        auth0Client = new Auth0Client(new()
        {
            Domain = "dev-17683470.okta.com",
            ClientId = "0oa912ox83mA6vxCh5d7",
            ClientSecret = "UWwSHKynb6mKQYNG8Wph3X962dvG5GDdZy1ty4ZG",
            Scope = "openid profile offline_access",
            Audience = "https://dev-17683470.okta.com",
#if WINDOWS
            RedirectUri = "https://localhost:44380/WeatherForecast"
#else
            RedirectUri = "myapp://callback"
#endif
        });

        _userService = new UserService();

#if WINDOWS
        auth0Client.Browser = new WebViewBrowserAuthenticator(WebViewInstance);
#endif
    }
    private void OnLoginClicked(object sender, EventArgs e)
    {
        loginUser();
    }
    public async void loginUser()
    {
        var loginResult = await auth0Client.LoginAsync();
        try
        {
            if (!loginResult.IsError)
            {
                await SecureStorage.Default.SetAsync("oauth_token", loginResult.IdentityToken);
                UsernameLbl.Text = loginResult.User.Identity.Name;
                UserPictureImg.Source = loginResult.User
                  .Claims.FirstOrDefault(c => c.Type == "picture")?.Value;


                string userEmail = loginResult.User.FindFirst("preferred_username").Value;
                //Validate Okta user exists in our Database or not if exists get the role pages.
                //var userlist = await _userService.GetUserList(loginResult.AccessToken);

                //var rolePagesList = await GetUserRolePages("saiprasad.thadem@pennywisesolutions.com");
                var rolePagesList = await GetUserRolePages(userEmail, loginResult.AccessToken);

                if (rolePagesList == null || rolePagesList.Count == 0)
                {
                    await DisplayAlert("Error", "User not have Role Page access so please close the app and ask Admin to assign roles to you", "OK");
                }
                else
                {
                    AddRoleMenus(rolePagesList);
                    TokenHolder.IdentityToken = loginResult.IdentityToken;
                    TokenHolder.AccessToken = loginResult.AccessToken;
                    TokenHolder.UserName = loginResult.User.Identity.Name;

                    LoginView.IsVisible = false;
                    HomeView.IsVisible = true;
                }
            }
            else
            {
                await DisplayAlert("Error", loginResult.ErrorDescription, "OK");
            }
        }
        catch (Exception ex)
        {

        }
    }

    public async Task<List<MAUI_Demo_Service.Models.UserRolePages>> GetUserRolePages(string email, string AccessToken)
    {
        List<MAUI_Demo_Service.Models.UserRolePages> userRolePageList = new List<MAUI_Demo_Service.Models.UserRolePages>();
        try
        {
            userRolePageList = await _userService.GetUserRolePages(email, AccessToken);
        }
        catch (Exception ex)
        {
        }
        return userRolePageList;
    }

    public async void AddRoleMenus(List<UserRolePages> rolePagesList)
    {
        try
        {
            var adminDashboardInfo = AppShell.Current.Items.Where(f => f.Route == nameof(DashBoardPage)).FirstOrDefault();
            if (adminDashboardInfo != null) AppShell.Current.Items.Remove(adminDashboardInfo);

            var flyoutItem = new FlyoutItem()
            {
                Title = "Dashboard Page",
                Route = nameof(DashBoardPage),
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems
            };
            foreach (var item in rolePagesList)
            {
                ShellContent content = new ShellContent();
                content.Title = item.PageName;
                content.FlyoutIcon = "dotnet_bot.svg";
                content.Route = item.PageName.ToLower();
                Type t = Type.GetType("MAUI_Demo.RolePages." + item.PageName);
                content.ContentTemplate = new DataTemplate(t);
                Routes.Add(item.PageName.ToLower(), t);
                flyoutItem.Items.Add(content);
            }
            foreach (var item in Routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
            if (!AppShell.Current.Items.Contains(flyoutItem))
            {
                AppShell.Current.Items.Add(flyoutItem);
                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    AppShell.Current.Dispatcher.Dispatch(async () =>
                    {
                        await Shell.Current.GoToAsync($"//{nameof(DashBoardPage)}", true);
                    });
                }
                else
                {
                    await Shell.Current.GoToAsync($"//{nameof(DashBoardPage)}");
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void GoBackTabbedPage_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new TabbedDemo());
    }
    public void OnLogoutCustomClicked(object sender, EventArgs e)
    {
        logoutUser();
    }
    public async void logoutUser()
    {
        //#if WINDOWS
        //        WebViewInstance.IsVisible = false; // Fix for when logout is issued and even though valid, causes a 404 page
        //#endif
        var logoutResult = await auth0Client.CustomLogoutAsync();


        if (!logoutResult.IsError)
        {
            HomeView.IsVisible = false;
            LoginView.IsVisible = true;

            // call API to kill the user access token.

            //Empty ID token,Access token
            TokenHolder.IdentityToken = "";
            TokenHolder.AccessToken = "";
        }
        else
        {
            await DisplayAlert("Error", logoutResult.ErrorDescription, "OK");
        }
        var filename = Process.GetCurrentProcess().MainModule.FileName;
        // Start a new instance of the application

        Process.Start(filename);
        Application.Current.Quit();
        // Close the current process
    }

    private async void BtnUserDetails_Clicked(object sender, EventArgs e)
    {
        var results = await auth0Client.getUserInfo(TokenHolder.AccessToken);
        using (var httpClient = new HttpClient())
        {
            string ApiUrl = "http://webapi.dev.local/api/Messages/defaultOktaTokenValid?accessToken=" + TokenHolder.AccessToken + "";
            //httpClient.DefaultRequestHeaders.Authorization
            //             = new AuthenticationHeaderValue("Bearer", TokenHolder.AccessToken);
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(ApiUrl);
                {
                    string content = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Info", content, "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }

    protected override bool OnBackButtonPressed()
    {
        return true;
    }

    private async void validOktaToken_Clicked(object sender, EventArgs e)
    {
        var results = await auth0Client.getUserInfo(TokenHolder.AccessToken);
        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenHolder.AccessToken);

            //string ApiUrl = "http://webapi.dev.local/api/Messages/defaultOktaTokenValid?accessToken=" + TokenHolder.AccessToken + "";
            //httpClient.DefaultRequestHeaders.Authorization
            //             = new AuthenticationHeaderValue("Bearer", TokenHolder.AccessToken);
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("http://webapi.dev.local/api/Messages/protected");
                {
                    string content = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Info", content, "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}