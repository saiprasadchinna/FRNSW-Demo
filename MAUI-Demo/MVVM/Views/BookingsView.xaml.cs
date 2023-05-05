using MAUI_Demo.MVVM.Models;
using MAUI_Demo.MVVM.ViewModels;
using System.Collections.Generic;

namespace MAUI_Demo.MVVM.Views;

public partial class BookingsView : ContentPage
{
    BookingsViewModel viewModel = new BookingsViewModel();
    public BookingsView()
    {
        InitializeComponent();
        BindingContext = new BookingsViewModel().GetBookingDetails().Result;
        collectionId2.ItemsSource = new BookingsViewModel().GetBookingDetails().Result;
    }

    private void Delete_Clicked(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        var parameter = (int)btn.CommandParameter;
        bool status = viewModel.DoOperations(parameter, "Delete").Result;
        
        if (status)
        {
            collectionId2.ItemsSource = new BookingsViewModel().GetBookingDetails().Result;
        }
    }

    private void Reject_Clicked(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        var parameter = (int)btn.CommandParameter;
        bool status = viewModel.DoOperations(parameter, "Reject").Result;

        if (status)
        {
            collectionId2.ItemsSource = new BookingsViewModel().GetBookingDetails().Result;
        }
    }

    private void Approved_Clicked(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        var parameter = (int)btn.CommandParameter;
        bool status = viewModel.DoOperations(parameter, "Confirm").Result;

        if (status)
        {
            collectionId2.ItemsSource = new BookingsViewModel().GetBookingDetails().Result;
        }
    }
}