using MAUI_Demo.Auth0;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MAUI_Demo;

public partial class OktaSignIn : ContentPage
{
    int count = 0;
    private readonly Auth0Client auth0Client;

    private HttpClient _httpClient;
    public OktaSignIn(Auth0Client client, HttpClient httpClient)
    {
        InitializeComponent();
        auth0Client = client;
        _httpClient = httpClient;

#if WINDOWS
    auth0Client.Browser = new WebViewBrowserAuthenticator(WebViewInstance);
#endif
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var loginResult = await auth0Client.LoginAsync();

        if (!loginResult.IsError)
        {
            await SecureStorage.Default.SetAsync("oauth_token", loginResult.IdentityToken);
            UsernameLbl.Text = loginResult.User.Identity.Name;
            UserPictureImg.Source = loginResult.User
              .Claims.FirstOrDefault(c => c.Type == "picture")?.Value;

            LoginView.IsVisible = false;
            HomeView.IsVisible = true;

            TokenHolder.IdentityToken = loginResult.IdentityToken; //👈 removed code
            TokenHolder.AccessToken = loginResult.AccessToken; //👈 new code
            TokenHolder.ReferenceToken = loginResult.RefreshToken; //👈 new code
        }
        else
        {
            await DisplayAlert("Error", loginResult.ErrorDescription, "OK");
        }
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        var logoutResult = await auth0Client.LogoutAsync();

        if (!logoutResult.IsError)
        {
            HomeView.IsVisible = false;
            LoginView.IsVisible = true;
        }
        else
        {
            await DisplayAlert("Error", logoutResult.ErrorDescription, "OK");
        }
    }

    private async void OnOktaLogoutClicked(object sender, EventArgs e)
    {
        new App();
        var logoutResult = await auth0Client.LogoutOktaAsync();

        if (!logoutResult.IsError)
        {
            HomeView.IsVisible = false;
            LoginView.IsVisible = true;
        }
        else
        {
            await DisplayAlert("Error", logoutResult.ErrorDescription, "OK");
        }

    }

    private async void OnLogoutCustomClicked(object sender, EventArgs e)
    {
        //#if WINDOWS
        //        WebViewInstance.IsVisible = false; // Fix for when logout is issued and even though valid, causes a 404 page
        //#endif
        var logoutResult = await auth0Client.CustomLogoutAsync();


        if (!logoutResult.IsError)
        {
            HomeView.IsVisible = false;
            LoginView.IsVisible = true; 
        }
        else
        {
            await DisplayAlert("Error", logoutResult.ErrorDescription, "OK");
        }

        //#if WINDOWS
        //        WebViewInstance.IsVisible = true;
        //#endif
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

    private async void WebView1_Navigating(object sender, WebNavigatingEventArgs e)
    {
        Debug.WriteLine("Navigating Fired");
    }

    public async void WebView1_Navigated(object sender, WebNavigatedEventArgs e)
    {
        Debug.WriteLine("Navigated Fired");
    }

    private void WebViewInstance_Navigating(object sender, WebNavigatingEventArgs e)
    {
       
    }
}