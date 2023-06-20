using MAUI_Demo.Auth0;
using MAUI_Demo.Views.Startup;
using MAUI_Demo_Service.Data;
using System.ComponentModel;

namespace MAUI_Demo.RolePages;

public partial class PageList : ContentPage, INotifyPropertyChanged
{

    public PageList()
    {
        InitializeComponent();
        //BindingContext = new PageListViewModel();
    }

    public void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
    {
        ((ListView)sender).SelectedItem = null;
    }

    public async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            var pageDetails = ((ListView)sender).SelectedItem as MAUI_Demo_Service.Models.Page;
            if (pageDetails != null)
            {
                var page = new PageDetails();
                pageDetails.RoleList = getRoleList().Result;
                pageDetails.SelectedRole = getRoleList().Result[1];
                pageDetails.BgColor = "Yellow";

                var navigationParameter = new Dictionary<string, object>
            {
                { "SelectedPageItem", pageDetails },
                    {"SelectedPageRole", pageDetails.SelectedRole}
            };

                // The following route works because route names are unique in this application.
                await Shell.Current.GoToAsync($"pagedetails", navigationParameter);
            }
        }
        catch (Exception ex)
        {

        }
    }
    public Task<List<MAUI_Demo_Service.Models.Role>> getRoleList()
    {
        UserService userService = new UserService();
        return Task.Run(() => userService.GetRoleList(TokenHolder.AccessToken));
    }

    private void AddPage_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddPage());
    }

    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}