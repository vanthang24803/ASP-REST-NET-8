using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Dtos.Stock;
using Api.Interfaces;
using Api.Mapper;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _context;


        public StockRepository(AppDbContext context)
        {
            _context = context;

        }

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

        public async Task<List<Stock>> GetAllStocks()
        {
            return await _context.Stocks.ToListAsync();

        }

        public async Task<Stock?> GetStockById(string id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
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