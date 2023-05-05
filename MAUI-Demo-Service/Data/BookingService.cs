using MAUI_Demo_Service.Models;
using MAUI_Demo_Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_Demo_Service.Data
{
    public class BookingService
    {
        RestService service = new RestService();
        public async Task<List<Bookings>> GetBookingDetailsAsyncFromAPI()
        {
            return await service.getBookingDetails();
        }

        public async Task<List<WeatherForecast>> GetWhetherDetailsAsyncFromAPI()
        {
            return await service.getWhetherDetails();
        }
        public async Task<bool> AddBookingDetailsAsyncFromAPI(string Name, string Address)
        {
            return await service.AddBookingDetails(Name, Address);
        }
        public async Task<bool> DoOperationBookingDetailsAsyncFromAPI(string type, int id)
        {
            return await service.DoOperationBookingDetails(type, id);
        }
    }
}
