using MAUI_Demo.Auth0;
using MAUI_Demo.MVVM.ViewModels;
using MAUI_Demo_Service.Data;
using MAUI_Demo_Service.Models;
using System.ComponentModel;
using System.Diagnostics;

namespace MAUI_Demo.RolePages;

public partial class UserList : ContentPage, INotifyPropertyChanged
{
   
    public UserList()
    {
        InitializeComponent();
        BindingContext = new UserListViewModel(); 
    }
    public void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
    {
        ((ListView)sender).SelectedItem = null;
    }


    public async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            var userDetails = ((ListView)sender).SelectedItem as User;
            if (userDetails != null)
            {
                var page = new UserDetailsPage();
                userDetails.RoleList = getRoleList().Result;
                page.BindingContext = userDetails;
                await Navigation.PushAsync(page);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
            Debug.WriteLine("customlog " + ex.Message);
        }
    }
    public Task<List<MAUI_Demo_Service.Models.Role>> getRoleList()
    {
        UserService userService = new UserService();
        return Task.Run(() => userService.GetRoleList(TokenHolder.AccessToken));
    }

    private void Add_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddUser());
    }
}