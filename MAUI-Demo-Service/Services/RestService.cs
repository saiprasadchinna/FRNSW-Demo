using MAUI_Demo_Service.Data;
using MAUI_Demo_Service.Models;
using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
//using systerJson = System.Text.Json;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json;

namespace MAUI_Demo_Service.Services;
public class RestService
{
    HttpClient _client;
    //systerJson.JsonSerializerOptions _serializerOptions;

    public RestService()
    {
        _client = new HttpClient();
        //_serializerOptions = new systerJson.JsonSerializerOptions
        //{
        //    PropertyNamingPolicy = systerJson.JsonNamingPolicy.CamelCase,
        //    WriteIndented = true
        //};
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
                Items = JsonConvert.DeserializeObject<List<Bookings>>(content);
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
                Items = JsonConvert.DeserializeObject<bool>(content);
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
                Items = JsonConvert.DeserializeObject<bool>(content);
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
                Items = JsonConvert.DeserializeObject<List<WeatherForecast>>(content);
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
                Items = JsonConvert.DeserializeObject<List<Employee>>(content);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return Items;
    }
    public async Task<List<UserRolePages>> GetUserRolePages(string email, string AccessToken)
    {
        List<UserRolePages> Items = new List<UserRolePages>();

        Uri uri = new Uri(string.Format("http://webapi.dev.local/api/User/getUserRolePages?email=" + email + "", string.Empty));

        HttpContent htcontent = new FormUrlEncodedContent(
              new List<KeyValuePair<string, string>>()
              {
              }
              );
        htcontent.Headers.Add("FRNSW_OKTA_TOKEN", AccessToken);
        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = uri,
            Content = htcontent
        };
        try
        {
            HttpResponseMessage response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Items = JsonConvert.DeserializeObject<List<UserRolePages>>(content);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return Items;
    }

    public async Task<List<Role>> GetRoleList(string AccessToken)
    {
        List<Role> Items = new List<Role>();

        Uri uri = new Uri(string.Format("http://webapi.dev.local/api/User/getRoleList", string.Empty));
        HttpContent htcontent = new FormUrlEncodedContent(
              new List<KeyValuePair<string, string>>()
              {
              }
              );
        htcontent.Headers.Add("FRNSW_OKTA_TOKEN", AccessToken);
        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = uri,
            Content = htcontent
        };
        try
        {

            HttpResponseMessage response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Items = JsonConvert.DeserializeObject<List<Role>>(content);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return Items;
    }
    public async Task<List<User>> GetUserList(string AccessToken)
    {
        List<User> Items = new List<User>();

        Uri uri = new Uri(string.Format("http://webapi.dev.local/api/User/getUserList", string.Empty));
        HttpContent htcontent = new FormUrlEncodedContent(
             new List<KeyValuePair<string, string>>()
             {
             }
             );
        htcontent.Headers.Add("FRNSW_OKTA_TOKEN", AccessToken);
        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = uri,
            Content = htcontent
        };
        try
        {
            HttpResponseMessage response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string results = await response.Content.ReadAsStringAsync();
                Items = JsonConvert.DeserializeObject<List<User>>(results);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return Items;
    }

    public async Task<List<MAUI_Demo_Service.Models.Page>> GetPageList(string AccessToken)
    {
        List<MAUI_Demo_Service.Models.Page> Items = new List<MAUI_Demo_Service.Models.Page>();

        Uri uri = new Uri(string.Format("http://webapi.dev.local/api/User/getPageList", string.Empty));
        HttpContent htcontent = new FormUrlEncodedContent(
             new List<KeyValuePair<string, string>>()
             {
             }
             );
        htcontent.Headers.Add("FRNSW_OKTA_TOKEN", AccessToken);
        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = uri,
            Content = htcontent
        };
        try
        {

            HttpResponseMessage response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Items = JsonConvert.DeserializeObject<List<MAUI_Demo_Service.Models.Page>>(content);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return Items;
    }

    public async Task<bool> AddUsers(string Name, string Email, long PhoneNumber, long RoleId, long UserId, string AccessToken)
    {
        bool Items = false;

        Uri uri = new Uri(string.Format("http://webapi.dev.local/api/User/AddUsers?Name=" + Name + "&Email=" + Email + "&PhoneNumber=" + PhoneNumber + "&RoleId=" + RoleId + "&UserId=" + UserId + "", string.Empty));
        HttpContent htcontent = new FormUrlEncodedContent(
               new List<KeyValuePair<string, string>>()
               {
               }
               );
        htcontent.Headers.Add("FRNSW_OKTA_TOKEN", AccessToken);
        try
        {

            HttpResponseMessage response = await _client.PostAsync(uri, htcontent);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Items = JsonConvert.DeserializeObject<bool>(content);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return Items;
    }

    public async Task<bool> AddPages(string Name, long RoleId, string RoleList, long PageId, string AccessToken)
    {
        bool Items = false;

        Uri uri = new Uri(string.Format("http://webapi.dev.local/api/User/AddPages?PageName=" + Name + "&RoleId=" + RoleId + "&RoleList=" + RoleList + "&PageId=" + PageId + "", string.Empty));
        HttpContent htcontent = new FormUrlEncodedContent(
              new List<KeyValuePair<string, string>>()
              {
              }
              );
        htcontent.Headers.Add("FRNSW_OKTA_TOKEN", AccessToken);
        try
        {
            HttpResponseMessage response = await _client.PostAsync(uri, htcontent);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Items = JsonConvert.DeserializeObject<bool>(content);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return Items;
    }
    public async Task<bool> OktaRevokeAccessToken(string AccessToken)
    {
        bool Items = false;
        var payload = "{\"token\": " + AccessToken + ",\"token_type_hint\": \"access_token\"}";
        //var payload = "{\"token\": eyJraWQiOiItVkFwMU5wNU9veXQzSU1XWDVyLUFvdkk4MVJfeFBJTFRBemxCc3pCb1VNIiwiYWxnIjoiUlMyNTYifQ.eyJ2ZXIiOjEsImp0aSI6IkFULlNod0VKNWxyVHdmTUZyY3l3Y1NjZUE3bG85ak5QYVNVNTVEUWFOZWd5eWsub2FyMTNhOTcweHhxakc2OHA1ZDciLCJpc3MiOiJodHRwczovL2Rldi0xNzY4MzQ3MC5va3RhLmNvbSIsImF1ZCI6Imh0dHBzOi8vZGV2LTE3NjgzNDcwLm9rdGEuY29tIiwic3ViIjoic2FpcHJhc2FkY2hpbm5hNzc5QGdtYWlsLmNvbSIsImlhdCI6MTY4NTMzODM0OSwiZXhwIjoxNjg1MzQxOTQ5LCJjaWQiOiIwb2E5MTJveDgzbUE2dnhDaDVkNyIsInVpZCI6IjAwdTlxbWxsc3A4RXdQdW9QNWQ3Iiwic2NwIjpbIm9wZW5pZCIsInByb2ZpbGUiLCJvZmZsaW5lX2FjY2VzcyJdLCJhdXRoX3RpbWUiOjE2ODUzMzgzNDR9.huBU3rsOPszBFqmFwA4scKS70ToEDjNz58TrAI3E1NZCqZFzCi22oSjMRIazfcc2pFm3O6Xd3tF4eq3X8Y40L4nDt6uJGAgenasoPNLKWhSi5SDSkZIB6gthQX4Zga3Yk-r9Di7_mx2GjdZGsiy5cJ3LHxU0YO33NRCsLp01NqFQbbzxr36_m_cN6hsnX1ozG3X5BZmKCUxvDPLMMKDdkpTkRziHMQWcloKI5DZvwBddQWTF_Km0SVlqHqvfUbuXNHRpqbLhBqS2pMDK7AWM5hlk17x0-CyuH-0MrhunZf_kbWIuAUxpWHpco2WmNZUBtW1F6XE6U_5aQ0XDBVHBWA,\"token_type_hint\": \"access_token\"}";
        Uri uri = new Uri(string.Format("https://dev-17683470.okta.com/oauth2/v1/revoke?client_id=0oa912ox83mA6vxCh5d7&client_secret=UWwSHKynb6mKQYNG8Wph3X962dvG5GDdZy1ty4ZG", string.Empty));
        //HttpContent content = new StringContent(payload, Encoding.UTF8, "application/x-www-form-urlencoded");
        HttpContent content = new FormUrlEncodedContent(
           new List<KeyValuePair<string, string>>()
           {
               new KeyValuePair<string, string>("token",AccessToken),
               new KeyValuePair<string, string>("token_type_hint","access_token")
           }
           );

        content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
        content.Headers.ContentType.CharSet = "UTF-8";
        _client.DefaultRequestHeaders.ExpectContinue = false;
        try
        {
            HttpResponseMessage response = await _client.PostAsync(uri, content);
            //        HttpResponseMessage response = await _client.PostAsync(uri,
            //new StringContent(JsonConvert.SerializeObject(
            //  new { payload },
            //  Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                //string content2 = await response.Content.ReadAsStringAsync();
                //Items = JsonSerializer.Deserialize<bool>(content2);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return Items;
    }
}

