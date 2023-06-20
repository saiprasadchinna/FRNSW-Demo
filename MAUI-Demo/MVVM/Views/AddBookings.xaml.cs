using MAUI_Demo.ViewModels;

namespace MAUI_Demo.MVVM.Views;

public partial class AddBookings : ContentPage
{
    //AddBookingsViewModel viewModel = new AddBookingsViewModel();
    public AddBookings()
    {
        InitializeComponent();
    }

    private void BtnAddBookings_Clicked(object sender, EventArgs e)
    {
        //bool status = viewModel.AddBookings(txtName.Text, txtAddress.Text).Result;
        //if (status)
        //{

        //}
        Navigation.PushAsync(new BookingsView());
    }
}