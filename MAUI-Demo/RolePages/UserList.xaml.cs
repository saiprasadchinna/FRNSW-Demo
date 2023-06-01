using CommunityToolkit.Mvvm.Input;
using MAUI_Demo.MVVM.ViewModels;
using MAUI_Demo_Service.Data;
using MAUI_Demo_Service.Models;
using System.ComponentModel;
using System.Windows.Input;

namespace MAUI_Demo.RolePages;

public partial class UserList : ContentPage, INotifyPropertyChanged
{
   
    public UserList()
    {
        InitializeComponent();
        BindingContext = new UserListViewModel();
        //usersListView.ItemsSource= new UserListViewModel().getUserList().Result;
    }
    public void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
    {
        ((ListView)sender).SelectedItem = null;
    }


    public async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var userDetails = ((ListView)sender).SelectedItem as User;
        if (userDetails != null)
        {
            var page = new UserDetailsPage();
            userDetails.RoleList= getRoleList().Result;
            
            page.BindingContext = userDetails;
            await Navigation.PushAsync(page);
        }
    }
    public Task<List<MAUI_Demo_Service.Models.Role>> getRoleList()
    {
        UserService userService = new UserService();
        return Task.Run(() => userService.GetRoleList());
    }

    private void Add_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddUser());
    }
}