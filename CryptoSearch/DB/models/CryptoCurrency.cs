using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace CryptoSearch.DB.models
{
    public class CryptoCurrency
    {
        public CryptoCurrency(DateTime date, int priceToUSD)
        {
            this.date = date.ToString();
            this.priceToUSD = priceToUSD;
        }

        [BsonId]
        public string date { get; set; }

        [BsonElement("price")]
        public int priceToUSD { get; set; }
    }
}
