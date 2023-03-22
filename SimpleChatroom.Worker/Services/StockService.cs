using CsvHelper;
using SimpleChatroom.Worker.Models;
using System.Globalization;

namespace SimpleChatroom.Worker.Services
{
    public class StockService : IStockService
    {
        private readonly HttpClient _httpClient;

        public StockService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Stock> GetStockData(string symbol)
        {
            var responseStream = await _httpClient.GetStreamAsync($"https://stooq.com/q/l/?s={symbol}&f=sd2t2ohlcv&h&e=csv");

            using (var reader = new StreamReader(responseStream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Stock>().ToList();

                return records.First();
            }
        }
    }
}