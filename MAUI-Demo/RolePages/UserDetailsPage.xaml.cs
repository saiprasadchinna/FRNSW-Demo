using MAUI_Demo.Auth0;
using MAUI_Demo_Service.Data;
using MAUI_Demo_Service.Models;

namespace MAUI_Demo.RolePages;

public partial class UserDetailsPage : ContentPage
{

    string selectedRoleIDs;

    public UserDetailsPage()
    {
        InitializeComponent();
        //BindingContext = this;
        //OnPropertyChanged("SelectedRole");
    }

    private void btnSave_Clicked(object sender, EventArgs e)
    {
        var item = rolePicker.SelectedItem as Role;
        bool status = AddUsers(NameEntry.Text, EmailEntry.Text, Convert.ToInt32(PhoneEntry.Text), item.RoleId, Convert.ToInt32(lblUserId.Text)).Result;
        if (status)
        {
            Navigation.PushAsync(new UserList());
        }
        else
        {
            DisplayAlert("DB Side Error", "User already exists please give different email Id to add the user", "OK");
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

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var previous = e.PreviousSelection.ToList();
        var current = e.CurrentSelection;
        selectedRoleIDs = "";
        foreach (var item in current)
        {
            var roleItem = (Role)item;
            if (string.IsNullOrEmpty(selectedRoleIDs))
            {
                selectedRoleIDs = selectedRoleIDs + Convert.ToString(roleItem.RoleId);
            }
            else
            {
                if (!selectedRoleIDs.Contains(Convert.ToString(roleItem.RoleId)))
                    selectedRoleIDs = selectedRoleIDs + "," + Convert.ToString(roleItem.RoleId);
            }
        }
    }
}