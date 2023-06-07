using MAUI_Demo.Auth0;
using MAUI_Demo_Service.Data;
using MAUI_Demo_Service.Models;

namespace MAUI_Demo.RolePages;

public partial class UserDetailsPage : ContentPage
{

    public UserDetailsPage()
    {
        InitializeComponent();
    }

    private void btnSave_Clicked(object sender, EventArgs e)
    {
        var item = rolePicker.SelectedItem as Role;
        bool status = AddUsers(NameEntry.Text, EmailEntry.Text, Convert.ToInt32(PhoneEntry.Text), item.RoleId, Convert.ToInt32(lblUserId.Text)).Result;
        if (status)
        {
            Navigation.PushAsync(new UserList());
        }
    }

    public Task<bool> AddUsers(string Name, string Email, long PhoneNumber, long RoleId, long UserId)
    {
        UserService userService = new UserService();
        return Task.Run(() => userService.AddUsers(Name, Email, PhoneNumber, RoleId, UserId, TokenHolder.AccessToken));
    }
    void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;


        if (selectedIndex != -1)
        {
            var item = picker.Items[selectedIndex];
        }
    }
}