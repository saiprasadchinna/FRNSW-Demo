using MAUI_Demo_Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_Demo.ViewModels
{
    public class BookingsViewModel
    {
        private List<MAUI_Demo_Service.Models.Bookings> bookingItems;
        BookingService bookingService = new BookingService();


        public BookingsViewModel()
        {
            //await GetBookingDetails();
            //bookingItems = await bookingService.GetBookingDetailsAsyncFromAPI();
        }

        public Task<List<MAUI_Demo_Service.Models.Bookings>> GetBookingDetails()
        {
            //bookingItems = bookingService.GetBookingDetailsAsyncFromAPI();
            return Task.Run(() => bookingService.GetBookingDetailsAsyncFromAPI());
        }

        public Task<bool> DoOperations(int Id, string type)
        {
            //bookingItems = bookingService.GetBookingDetailsAsyncFromAPI();
            return Task.Run(() => bookingService.DoOperationBookingDetailsAsyncFromAPI(type, Id));
        }
    }
}
