using MAUI_Demo_Service.Data;
using MAUI_Demo_Service.Models;
using Microsoft.Maui.Storage;

namespace MAUI_Demo.RolePages;

public partial class PageDetails : ContentPage
{
    public PageDetails()
    {
        InitializeComponent();
    }

    private void btnSave_Clicked(object sender, EventArgs e)
    {
        var item = rolePicker.SelectedItem as Role;
        bool status = AddPages(PageNameEntry.Text, item.RoleId, Convert.ToInt32(lblPageId.Text)).Result;
        if (status)
        {
            Navigation.PushAsync(new PageList());
        }
    }
    public Task<bool> AddPages(string PageName, long RoleId, long PageId)
    {
        UserService userService = new UserService();
        return Task.Run(() => userService.AddPages(PageName, RoleId, PageId));
    }
}