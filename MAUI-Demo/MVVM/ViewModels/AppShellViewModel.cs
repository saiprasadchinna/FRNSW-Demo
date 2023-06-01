using System.Windows.Input;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using MAUI_Demo.RolePages;
using MAUI_Demo.Views.Startup;
using MAUI_Demo.Auth0;
using MAUI_Demo_Service.Data;

namespace MAUI_Demo.MVVM.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {

        public ICommand SignOutCommand => new Command<string>(async  => SignOut());
        //public ICommand HelpCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));
        private readonly Auth0Client auth0Client;
        private readonly UserService _userService;
        //private HttpClient _httpClient;
          

        void SignOut()
        {
            //if (Preferences.ContainsKey(nameof(App.UserDetails)))
            //{
            //    Preferences.Remove(nameof(App.UserDetails));
            //}
            //await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            OktaSignIn obj = new OktaSignIn();
            obj.logoutUser();
            //obj.OnLogoutCustomClicked(object sender, EventArgs e);

            //var adminDashboardInfo = AppShell.Current.Items.Where(f => f.Route == nameof(DashboardPage)).FirstOrDefault();
            //if (adminDashboardInfo != null) AppShell.Current.Items.Remove(adminDashboardInfo); 

            //await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
