using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;
using Xamarin.Essentials;

namespace SolanaWatchface
{
    public class CurrentPriceModel
    {
        private static string API_KEY = "XXX";
        private Root cryptoData;

        public CurrentPriceModel()
        {
            makeAPICall();
        }
        public void makeAPICall()
        {
            try
            {
                var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

                var queryString = HttpUtility.ParseQueryString(string.Empty);

                queryString["start"] = "1";
                queryString["limit"] = "10";
                queryString["convert"] = "CZK";

                URL.Query = queryString.ToString();

                var client = new WebClient();
                client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
                client.Headers.Add("Accepts", "application/json");
                string jsonString = client.DownloadString(URL.ToString());

                try
                {
                    cryptoData = JsonConvert.DeserializeObject<Root>(jsonString);
                }
                catch (Exception ex)
                {

                }
            }
            catch
            {

            }

        }
        public double FindCost(string cryptoName)
        {
            if (cryptoData == null)
            {
                //Console.WriteLine("null");
                return Preferences.Get(cryptoName, 0);
            }
            else
            {
                for (int i = 0; i < 5000; i++)
                {
                    //Console.WriteLine(cryptoData.data[i].symbol);
                    if (cryptoData.data[i].symbol.Equals(cryptoName))
                    {
                        Preferences.Set(cryptoName, cryptoData.data[i].quote.CZK.price);
                        return cryptoData.data[i].quote.CZK.price;
                    }
                }
            }

            return 0; // just in case (has to be here)
        }
    }
    public class Status
    {
        public DateTime timestamp { get; set; }
        public int error_code { get; set; }
        public object error_message { get; set; }
        public int elapsed { get; set; }
        public int credit_count { get; set; }
        public object notice { get; set; }
        public int total_count { get; set; }
    }

    public class CZK
    {
        public double price { get; set; }
        public double volume_24h { get; set; }
        public double? volume_change_24h { get; set; }
        public double? percent_change_1h { get; set; }
        public double? percent_change_24h { get; set; }
        public double? percent_change_7d { get; set; }
        public double? percent_change_30d { get; set; }
        public double? percent_change_60d { get; set; }
        public double? percent_change_90d { get; set; }
        public double? market_cap { get; set; }
        public double market_cap_dominance { get; set; }
        public double fully_diluted_market_cap { get; set; }
        public DateTime last_updated { get; set; }
    }

    public class Quote
    {
        public CZK CZK { get; set; }
    }

    public class Datum
    {
        public int id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public string slug { get; set; }
        public int num_market_pairs { get; set; }
        public DateTime date_added { get; set; }
        public List<string> tags { get; set; }
        //public int? max_supply { get { return 0; } set { Console.WriteLine("0"); } }
        public double circulating_supply { get; set; }
        public double total_supply { get; set; }
        public object platform { get; set; }
        public int cmc_rank { get; set; }
        public object self_reported_circulating_supply { get; set; }
        public object self_reported_market_cap { get; set; }
        public DateTime last_updated { get; set; }
        public Quote quote { get; set; }
    }

    public class Root
    {
        public Status status { get; set; }
        public List<Datum> data { get; set; }
    }
}
