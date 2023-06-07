using IdentityModel.OidcClient;
using MAUI_Demo.Auth0;
using MAUI_Demo.RolePages;
using MAUI_Demo_Service.Data;
using MAUI_Demo_Service.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MAUI_Demo;

public partial class OktaSignIn : ContentPage
{
    int count = 0;
    private readonly Auth0Client auth0Client;
    private readonly UserService _userService;
    //private HttpClient _httpClient;
    public OktaSignIn()
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
        }); ;
        //_httpClient = new HttpClient();

        _userService = new UserService();


#if WINDOWS
        auth0Client.Browser = new WebViewBrowserAuthenticator(WebViewInstance);
#endif
    }

    private void OnLoginClicked(object sender, EventArgs e)
    {
        loginUser();
        //_userService.GetUserList();
        //var rolePagesList = GetUserRolePages("saiprasad.thadem@pennywisesolutions.com");
    }

    public async void loginUser()
    {
        //var loginResult = await auth0Client.LoginAsync();

        //if (!loginResult.IsError)
        //{
        //    await SecureStorage.Default.SetAsync("oauth_token", loginResult.IdentityToken);
        //    UsernameLbl.Text = loginResult.User.Identity.Name;
        //    UserPictureImg.Source = loginResult.User
        //      .Claims.FirstOrDefault(c => c.Type == "picture")?.Value;

        //    LoginView.IsVisible = false;
        //    HomeView.IsVisible = true;

        //    //Validate Okta user exists in our Database or not if exists get the role pages.
        //    string userEmail = loginResult.User.FindFirst("preferred_username").Value;
        //var userlist = await _userService.GetUserList();
        var rolePagesList = await GetUserRolePages("saiprasad.thadem@pennywisesolutions.com", "");
        AddRoleMenus(rolePagesList);
        //    TokenHolder.IdentityToken = loginResult.IdentityToken;
        //    TokenHolder.AccessToken = loginResult.AccessToken;
        //    TokenHolder.UserName = loginResult.User.Identity.Name;            
        //}
        //else
        //{
        //    await DisplayAlert("Error", loginResult.ErrorDescription, "OK");
        //}
    }

    public async Task<List<MAUI_Demo_Service.Models.UserRolePages>> GetUserRolePages(string email,string AccessToken)
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
            Type t = Type.GetType("MAUI_Demo.RolePages." + item.PageName);
            content.ContentTemplate = new DataTemplate(t);
            flyoutItem.Items.Add(content);
        }

        if (!AppShell.Current.Items.Contains(flyoutItem))
        {
            AppShell.Current.Items.Add(flyoutItem);
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                AppShell.Current.Dispatcher.Dispatch(async () =>
                {
                    await Shell.Current.GoToAsync($"//{nameof(DashBoardPage)}");
                });
            }
            else
            {
                await Shell.Current.GoToAsync($"//{nameof(DashBoardPage)}");
            }
        }
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

}