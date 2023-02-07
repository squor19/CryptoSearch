using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CryptoSearch
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            string test = APICoinGeckoManager.GetCurrent24("binance-bitcoin", "usd");
            DateTime date = new DateTime(2017, 12, 30);
            APICoinGeckoManager.updateCoinHistory(date, "bitcoin");
            int debug = 1;
        }
    }
}
