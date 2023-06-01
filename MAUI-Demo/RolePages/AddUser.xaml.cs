using MAUI_Demo.MVVM.ViewModels;
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
        bool status = objUserViewModel.AddUsers(NameEntry.Text, EmailEntry.Text, Convert.ToInt32(PhoneEntry.Text), item.RoleId,0).Result;
        if (status)
        {
            Navigation.PushAsync(new UserList());
        }
        
    }
}