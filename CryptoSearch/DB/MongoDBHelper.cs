using CryptoSearch.DB.models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSearch.DB
{
    public class MongoDBHelper
    {
        public static void pasteCoinData(string coinId, DateTime date, int priceToUSD)
        {
            var client = new MongoClient("");
            var database = client.GetDatabase("Systemaintegrowana");

            var collectionNames = database.ListCollectionNames().ToList();

            var buffCollection = database.GetCollection<CryptoCurrency>(coinId);
            var currency = new CryptoCurrency(date, priceToUSD);

            buffCollection.InsertOne(currency);
        }
    }
}
