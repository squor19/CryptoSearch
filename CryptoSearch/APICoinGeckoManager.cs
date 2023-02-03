using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSearch
{
    public class APICoinGeckoManager
    {
        public static string TestConnection()
        {
            string responseContent;
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("accept", "application/json");
                var response = httpClient.GetAsync("https://api.coingecko.com/api/v3/ping").Result;
                responseContent = response.Content.ReadAsStringAsync().Result;
            }
            if (responseContent.Equals("{\"gecko_says\":\"(V3) To the Moon!\"}"))
            {
                return "Connection OK";
            } else
            {
                return "Connection Error";
            }
        }

        public static string GetCurrent24(string currencyId, string vsCurrencyId)
        {
            string request = "https://api.coingecko.com/api/v3/simple/price?ids=" + currencyId + "&vs_currencies=" + vsCurrencyId + "&include_24hr_change=true";
            string responseContent;
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("accept", "application/json");
                var response = httpClient.GetAsync(request).Result;
                responseContent = response.Content.ReadAsStringAsync().Result;
            }
            return responseContent;
        }
    }
}
