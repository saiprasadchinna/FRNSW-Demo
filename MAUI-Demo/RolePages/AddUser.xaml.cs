using MAUI_Demo.Auth0;
using MAUI_Demo.ViewModels;
using MAUI_Demo_Service.Models;

namespace MAUI_Demo.RolePages;

public partial class AddUser : ContentPage
{
    AddUserViewModel objUserViewModel;
    public AddUser()
    {
        InitializeComponent();
        objUserViewModel = new AddUserViewModel();
        BindingContext = objUserViewModel;
    }

    private void btnSave_Clicked(object sender, EventArgs e)
    {
        var item = rolePicker.SelectedItem as Role;
        bool status = objUserViewModel.AddUsers(NameEntry.Text, EmailEntry.Text, Convert.ToInt32(PhoneEntry.Text), item.RoleId,0,TokenHolder.AccessToken).Result;
        if (status)
        {
            Navigation.PushAsync(new UserList());
        }
        else
        {
            DisplayAlert("DB Side Error", "User already exists please give different email Id to add the user", "OK");
        }

    }
}