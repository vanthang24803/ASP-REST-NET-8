using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.Stock;
using Api.Helper;
using Api.Models;

namespace Api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllStocks(QueryObject query);

        Task<Stock?> GetStockById(string id);

        Task<Stock> CreateStock(Stock stock);
        
        Task<Stock?> UpdateStock(string id , UpdateStockRequestDto update);

        Task<Stock?> UpdateOptions(string id , UpdateStockOptionsRequestDto update);
        Task<Stock?> DeleteStock(string id);

         Task<bool> StockExists(string id);
    }
}