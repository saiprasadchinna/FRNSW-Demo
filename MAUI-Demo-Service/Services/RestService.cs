using MAUI_Demo_Service.Data;
using MAUI_Demo_Service.Models;
using System.Diagnostics;
using System.Text.Json;

namespace MAUI_Demo_Service.Services;
public class RestService
{
    HttpClient _client;
    JsonSerializerOptions _serializerOptions;

    public List<Bookings> bookingItems { get; private set; }

    public RestService()
    {
        _client = new HttpClient();
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }
    public async Task<List<Bookings>> getBookingDetails()
    {
        List<Bookings> Items = new List<Bookings>();

        Uri uri = new Uri(string.Format("http://webapi.dev.local/api/Bookings/getBookingDetails", string.Empty));
        try
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Items = JsonSerializer.Deserialize<List<Bookings>>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return Items;
    }
    public async Task<bool> AddBookingDetails(string Name, string Address)
    {
        bool Items = false;

        Uri uri = new Uri(string.Format("http://webapi.dev.local/api/Bookings/addBookings?Name=" + Name + "&Address=" + Address + "", string.Empty));
        try
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Items = JsonSerializer.Deserialize<bool>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return Items;
    }

    public async Task<bool> DoOperationBookingDetails(string type, int id)
    {
        bool Items = false;

        Uri uri = new Uri(string.Format("http://webapi.dev.local/api/Bookings/doOperations?Type=" + type + "&Id=" + id + "", string.Empty));
        try
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Items = JsonSerializer.Deserialize<bool>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return Items;
    }

    public async Task<List<WeatherForecast>> getWhetherDetails()
    {
        List<WeatherForecast> Items = new List<WeatherForecast>();

        Uri uri = new Uri(string.Format("http://webapi.dev.local/WeatherForecast", string.Empty));
        try
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Items = JsonSerializer.Deserialize<List<WeatherForecast>>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return Items;
    }
    public async Task<List<Employee>> GetEmployeeDetails()
    {
        List<Employee> Items = new List<Employee>();

        Uri uri = new Uri(string.Format("https://hub.dummyapis.com/employee?noofRecords=10&idStarts=1001", string.Empty));
        try
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Items = JsonSerializer.Deserialize<List<Employee>>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return Items;
    }
}

