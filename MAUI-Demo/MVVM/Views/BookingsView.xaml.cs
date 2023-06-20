using MAUI_Demo.ViewModels;
using System.ComponentModel;

namespace MAUI_Demo.MVVM.Views;


public partial class BookingsView : ContentPage, INotifyPropertyChanged
{

    private bool _inlineButtonEnabled = false;


    public bool InlineButtonEnabled
    {
        get
        {
            // breakpoint 1, which never hits with value = false
            return _inlineButtonEnabled;
        }
        set
        {
            // breakpoint 2, which hits
            _inlineButtonEnabled = value;
            OnPropertyChanged(nameof(InlineButtonEnabled));
        }
    }

    BookingsViewModel viewModel = new BookingsViewModel();

    public IEnumerable<MAUI_Demo_Service.Models.Bookings> bookingsList = new List<MAUI_Demo_Service.Models.Bookings>();

    public int DefaultLoad = 2;
    int CurrentPage = 1;
    int PreviousPage = 1;
    public int skipItems = 0;

    public BookingsView()
    {
        InitializeComponent();
        int[] items = { 1, 2, 3, 4, 5, 6, 7 };
        var a = items.Skip(2).Take(3);
        BindingContext = this;
        //bookingsList2 = ;
        collectionId2.ItemsSource = new BookingsViewModel().GetBookingDetails().Result.Skip((CurrentPage - 1) * DefaultLoad).Take(DefaultLoad);
        //skipItems = DefaultLoad;
        BtnPrevious.IsEnabled = false;
        CurrentPage = CurrentPage + 1;

    }
    private void refreshItems()
    {
        var bookingList = new BookingsViewModel().GetBookingDetails().Result;
    }

    private void Delete_Clicked(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        var parameter = (int)btn.CommandParameter;
        bool status = viewModel.DoOperations(parameter, "Delete").Result;

        if (status)
        {
            CurrentPage--;
            collectionId2.ItemsSource = new BookingsViewModel().GetBookingDetails().Result.Skip((CurrentPage - 1) * DefaultLoad).Take(DefaultLoad);
            CurrentPage++;
        }
    }

    private void Reject_Clicked(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        var parameter = (int)btn.CommandParameter;
        bool status = viewModel.DoOperations(parameter, "Reject").Result;

        if (status)
        {
            CurrentPage--;
            collectionId2.ItemsSource = new BookingsViewModel().GetBookingDetails().Result.Skip((CurrentPage - 1) * DefaultLoad).Take(DefaultLoad);
            CurrentPage++;
        }
    }

    private void Approved_Clicked(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        var parameter = (int)btn.CommandParameter;
        bool status = viewModel.DoOperations(parameter, "Confirm").Result;

        if (status)
        {
            CurrentPage--;
            collectionId2.ItemsSource = new BookingsViewModel().GetBookingDetails().Result.Skip((CurrentPage - 1) * DefaultLoad).Take(DefaultLoad);
            CurrentPage++;
        }
    }

    private void BtnNext_Clicked(object sender, EventArgs e)
    {
        BtnPrevious.IsEnabled = true;
        bookingsList = new BookingsViewModel().GetBookingDetails().Result.Skip((CurrentPage - 1) * DefaultLoad).Take(DefaultLoad);
        if (bookingsList.Count() > 0)
        {
            CurrentPage++;
            PreviousPage++;
            collectionId2.ItemsSource = bookingsList;
        }
        else
        {
            BtnNext.IsEnabled = false;
        }
    }

    private void BtnPrevious_Clicked(object sender, EventArgs e)
    {
        PreviousPage--;
        if (PreviousPage != 0)
        {
            bookingsList = new BookingsViewModel().GetBookingDetails().Result.Skip((PreviousPage - 1) * DefaultLoad).Take(DefaultLoad);
            if (bookingsList.Count() > 0)
            {
                BtnPrevious.IsEnabled = true;
                collectionId2.ItemsSource = bookingsList;

                if (PreviousPage == 0 || PreviousPage == 1)
                {
                    BtnPrevious.IsEnabled = false;
                }
                CurrentPage--;
                BtnNext.IsEnabled = true;
            }
            else
            {
                BtnPrevious.IsEnabled = false;
            }
        }
    }
    private void InlineBtnPrevious_Clicked(object sender, EventArgs e)
    {
        InlineButtonEnabled = true;
    }

    private void InlineBtnNext_Clicked(object sender, EventArgs e)
    {
        InlineButtonEnabled = false;
    }
}