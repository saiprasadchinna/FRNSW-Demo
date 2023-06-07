using MAUI_Demo.Auth0;
using MAUI_Demo.MVVM.ViewModels;
using MAUI_Demo_Service.Models;
using Microsoft.Maui.Storage;

namespace MAUI_Demo.RolePages;

public partial class AddPage : ContentPage
{
    AddPageViewModel objPageViewModel;
    public AddPage()
    {
        InitializeComponent();
        objPageViewModel = new AddPageViewModel();
        BindingContext = objPageViewModel;
    }

    private void btnSave_Clicked(object sender, EventArgs e)
    {
        var item = rolePicker.SelectedItem as Role;
        bool status = objPageViewModel.AddPages(NameEntry.Text, item.RoleId, 0,TokenHolder.AccessToken).Result;
        if (status)
        {

        }
        Navigation.PushAsync(new PageList());
    }
}