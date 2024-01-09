using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Dtos.Stock;
using Api.Helper;
using Api.Interfaces;
using Api.Mapper;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository
{
    public class StockRepository(AppDbContext context) : IStockRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Stock> CreateStock(Stock stock)
        {
            await _context.AddAsync(stock);
            await _context.SaveChangesAsync();

            return stock;
        }

        public async Task<Stock?> DeleteStock(string id)
        {
            var existingStock = await _context.Stocks.FindAsync(id);

            if (existingStock is null)
            {
                return null;
            }

            _context.Stocks.Remove(existingStock);
            await _context.SaveChangesAsync();
            return existingStock;
        }

        public async Task<List<Stock>> GetAllStocks(QueryObject query)
        {
            var stocks = _context.Stocks.Include(c => c.Comments).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDecsending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetStockById(string id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }

        public Task<bool> StockExists(string id)
        {
            return _context.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateOptions(string id, UpdateStockOptionsRequestDto update)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingStock == null)
            {
                return null;
            }

            existingStock.Symbol = update.Symbol ?? existingStock.Symbol;
            existingStock.CompanyName = update.CompanyName ?? existingStock.CompanyName;
            existingStock.Purchase = update.Purchase ?? existingStock.Purchase;
            existingStock.LastDiv = update.LastDiv ?? existingStock.LastDiv;
            existingStock.Industry = update.Industry ?? existingStock.Industry;
            existingStock.MarketCap = update.MarketCap ?? existingStock.MarketCap;
            existingStock.UpdateAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return existingStock;
        }

        public async Task<Stock?> UpdateStock(string id, UpdateStockRequestDto update)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingStock == null)
            {
                return null;
            }

            existingStock.Symbol = update.Symbol;
            existingStock.CompanyName = update.CompanyName;
            existingStock.Purchase = update.Purchase;
            existingStock.LastDiv = update.LastDiv;
            existingStock.Industry = update.Industry;
            existingStock.MarketCap = update.MarketCap;
            existingStock.UpdateAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return existingStock;
        }
    }
}