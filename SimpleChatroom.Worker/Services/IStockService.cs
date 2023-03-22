using SimpleChatroom.Worker.Models;

namespace SimpleChatroom.Worker.Services
{
    public interface IStockService
    {
        Task<Stock> GetStockData(string symbol);
    }
}