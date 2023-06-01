using MAUI_Demo.MVVM.ViewModels;
using MAUI_Demo_Service.Data;
using MAUI_Demo_Service.Models;
using System.ComponentModel;

namespace MAUI_Demo.RolePages;

public partial class PageList : ContentPage, INotifyPropertyChanged
{
	public PageList()
	{
		InitializeComponent();
        BindingContext = new PageListViewModel();
    }

    public void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
    {
        ((ListView)sender).SelectedItem = null;
    }

    public async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var pageDetails = ((ListView)sender).SelectedItem as  MAUI_Demo_Service.Models.Page;
        if (pageDetails != null)
        {
            var page = new PageDetails();
            pageDetails.RoleList = getRoleList().Result;
            page.BindingContext = pageDetails;
            await Navigation.PushAsync(page);
        }
    }
    public Task<List<MAUI_Demo_Service.Models.Role>> getRoleList()
    {
        UserService userService = new UserService();
        return Task.Run(() => userService.GetRoleList());
    }

    private void AddPage_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddPage());
    }
}