using MAUI_Demo.Auth0;
using MAUI_Demo.ViewModels;
using MAUI_Demo_Service.Data;
using MAUI_Demo_Service.Models;
//using UIKit;

namespace MAUI_Demo.RolePages;

public partial class PageDetails : ContentPage
{

    string selectedRoleIDs;
    public PageDetails()
    {
        InitializeComponent();
        BindingContext = new PageDetailsViewModel();
    }

    private void btnSave_Clicked(object sender, EventArgs e)
    {
        var selectedItem = roleCollection.SelectedItem;
        //var item = rolePicker.SelectedItem as Role;
        bool status = AddPages(PageNameEntry.Text, 0, selectedRoleIDs, Convert.ToInt32(lblPageId.Text)).Result;
        if (status)
        {
            Navigation.PushAsync(new PageList());
        }
        else
        {

        }
    }
    public Task<bool> AddPages(string PageName, long RoleId, string RoleList, long PageId)
    {
        UserService userService = new UserService();
        return Task.Run(() => userService.AddPages(PageName, RoleId, RoleList, PageId, TokenHolder.AccessToken));
    }

    private void roleCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var previous = e.PreviousSelection;
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