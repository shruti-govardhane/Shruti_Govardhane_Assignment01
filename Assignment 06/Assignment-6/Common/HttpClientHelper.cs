﻿

using System.Net.Http;
using System.Text;

namespace Employee_Management_System.Common
{
    public class HttpClientHelper
    {
        public static async Task<string> MakePostRequest(string baseUrl, string endpoint, string apiRequestData)
        {
            var socketsHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(10),
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(5),
                MaxConnectionsPerServer = 10
            };

            using (HttpClient httpClient = new HttpClient(socketsHandler))
            {
                httpClient.Timeout = TimeSpan.FromMinutes(5);
                httpClient.BaseAddress = new Uri(baseUrl);
                StringContent apiRequestContent = new StringContent(apiRequestData, Encoding.UTF8, "application/json");
                var httpResponse = httpClient.PostAsync(endpoint, apiRequestContent).Result;
                var httpResponseString = httpResponse.Content.ReadAsStringAsync().Result;

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception(httpResponseString);
                }
                return httpResponseString;
            }
        }
       public static async Task<string> MakeGetRequest(string baseUrl, string endpoint)
        {
            var socketsHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(10),
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(5),
                MaxConnectionsPerServer = 10
            };
            using (HttpClient httpsClient = new HttpClient(socketsHandler))
            {
                httpsClient.Timeout = TimeSpan.FromMinutes(5);
                httpsClient.BaseAddress = new Uri(baseUrl);


                var response = await httpsClient.GetAsync(endpoint);
                var responseString = await response.Content.ReadAsStringAsync();

                if(response == null)
                {
                    return null;
                }
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(responseString);
                }
                return responseString;
            }

        }
    }
}

