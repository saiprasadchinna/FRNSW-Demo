using MAUI_Demo_Service.Data;

namespace MAUI_Demo.MVVM.ViewModels
{
    public class AddBookingsViewModel
    {
        BookingService bookingService = new BookingService();
        public Task<bool> AddBookings(string Name, string Address)
        {
            //bookingItems = bookingService.GetBookingDetailsAsyncFromAPI();
            return Task.Run(() => bookingService.AddBookingDetailsAsyncFromAPI(Name, Address));
        }
    }
}
