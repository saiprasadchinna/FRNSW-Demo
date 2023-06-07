using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using WebAPI_BackOffice.Models;

namespace WebAPI_BackOffice.DB
{
    public class OktaContext
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;

        public OktaContext()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        public async Task<IntrospectResponse> ValidateOktaToken(string AccessToken)
        {
            string clientId = "0oa912ox83mA6vxCh5d7";
            string clientSecret = "UWwSHKynb6mKQYNG8Wph3X962dvG5GDdZy1ty4ZG";
            IntrospectResponse myDeserializedClass = new IntrospectResponse();
            string content2 = "";
            //var payload = "{\"token\": " + AccessToken + ",\"token_type_hint\": \"access_token\",\"client_id\": " + clientId + ",\"client_secret\": " + clientSecret + "}";
            //var payload = "{\"token\": eyJraWQiOiItVkFwMU5wNU9veXQzSU1XWDVyLUFvdkk4MVJfeFBJTFRBemxCc3pCb1VNIiwiYWxnIjoiUlMyNTYifQ.eyJ2ZXIiOjEsImp0aSI6IkFULlNod0VKNWxyVHdmTUZyY3l3Y1NjZUE3bG85ak5QYVNVNTVEUWFOZWd5eWsub2FyMTNhOTcweHhxakc2OHA1ZDciLCJpc3MiOiJodHRwczovL2Rldi0xNzY4MzQ3MC5va3RhLmNvbSIsImF1ZCI6Imh0dHBzOi8vZGV2LTE3NjgzNDcwLm9rdGEuY29tIiwic3ViIjoic2FpcHJhc2FkY2hpbm5hNzc5QGdtYWlsLmNvbSIsImlhdCI6MTY4NTMzODM0OSwiZXhwIjoxNjg1MzQxOTQ5LCJjaWQiOiIwb2E5MTJveDgzbUE2dnhDaDVkNyIsInVpZCI6IjAwdTlxbWxsc3A4RXdQdW9QNWQ3Iiwic2NwIjpbIm9wZW5pZCIsInByb2ZpbGUiLCJvZmZsaW5lX2FjY2VzcyJdLCJhdXRoX3RpbWUiOjE2ODUzMzgzNDR9.huBU3rsOPszBFqmFwA4scKS70ToEDjNz58TrAI3E1NZCqZFzCi22oSjMRIazfcc2pFm3O6Xd3tF4eq3X8Y40L4nDt6uJGAgenasoPNLKWhSi5SDSkZIB6gthQX4Zga3Yk-r9Di7_mx2GjdZGsiy5cJ3LHxU0YO33NRCsLp01NqFQbbzxr36_m_cN6hsnX1ozG3X5BZmKCUxvDPLMMKDdkpTkRziHMQWcloKI5DZvwBddQWTF_Km0SVlqHqvfUbuXNHRpqbLhBqS2pMDK7AWM5hlk17x0-CyuH-0MrhunZf_kbWIuAUxpWHpco2WmNZUBtW1F6XE6U_5aQ0XDBVHBWA,\"token_type_hint\": \"access_token\"}";
            Uri uri = new Uri(string.Format("https://dev-17683470.okta.com/oauth2/v1/introspect", string.Empty));
            //HttpContent content = new StringContent(payload, Encoding.UTF8, "application/x-www-form-urlencoded");
            HttpContent content = new FormUrlEncodedContent(
               new List<KeyValuePair<string, string>>()
               {
               new KeyValuePair<string, string>("token",AccessToken),
               new KeyValuePair<string, string>("token_type_hint","access_token"),
               new KeyValuePair<string, string>("client_id",clientId),
               new KeyValuePair<string, string>("client_secret",clientSecret)
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
                    content2 = await response.Content.ReadAsStringAsync();

                    myDeserializedClass = JsonConvert.DeserializeObject<IntrospectResponse>(content2);


                    //Item = JsonSerializer.Deserialize<IntrospectResponse>(content2, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return myDeserializedClass;
        }
    }
}
