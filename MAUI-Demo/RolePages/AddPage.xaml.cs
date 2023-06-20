using MAUI_Demo.Auth0;
using MAUI_Demo.ViewModels;
using MAUI_Demo_Service.Models;
using Microsoft.Maui.Storage;

namespace MAUI_Demo.RolePages;

public partial class AddPage : ContentPage
{
    AddPageViewModel objPageViewModel;
    string selectedRoleIDs;
    public AddPage()
    {
        InitializeComponent();
        objPageViewModel = new AddPageViewModel();
        BindingContext = objPageViewModel;
    }

    private void btnSave_Clicked(object sender, EventArgs e)
    {
        var item = rolePicker.SelectedItem as Role;
        bool status = objPageViewModel.AddPages(NameEntry.Text, 0, selectedRoleIDs, 0, TokenHolder.AccessToken).Result;
        if (status)
        {
            Navigation.PushAsync(new PageList());
        }
        else
        {
            DisplayAlert("DB Side Error", "User already exists please give different email Id to add the user", "OK");
        }
    }
    void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
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