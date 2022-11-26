using Stock.Service.Models;

namespace Stock.Service.Interfaces
{
    public interface IStooqService
    {
        Task<StooqResult?> GetStocks(string symbol);
    }
}