using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptoSearch.DB;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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


        public static async void updateCoinHistory(DateTime date, string coinId)
        {
            while(date.Year < DateTime.Now.Year)
            {
                for (int i = 0; i < 20; i++)
                {
                    string result = MakeHistoryRequest(date, coinId);
                    int price = 0;

                    var match = Regex.Match(result, @"""usd"":\s*(\d+)");

                    if (match.Success)
                    {
                        price = int.Parse(match.Groups[1].Value);
                    }
                    Console.WriteLine(coinId + ":   " + price + "             " + date.ToString());
                    MongoDBHelper.pasteCoinData(coinId, date, price);
                    date = date.AddDays(1);
                    await Task.Delay(30000);
                }
            }
            
        }

        private static string MakeHistoryRequest(DateTime date, string coinId)
        {
            string request = "https://api.coingecko.com/api/v3/coins/" + coinId + "/history?date=" + date.Day + "-" + date.Month + "-" + date.Year + "&localization=false";
            string responseContent;
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("accept", "application/json");
                var response = httpClient.GetAsync(request).Result;
                responseContent = response.Content.ReadAsStringAsync().Result;
            }

            return responseContent;
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


