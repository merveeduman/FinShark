using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public StockRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock> DeleteAsync(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stock == null) return null;
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<List<Stock>> GetAllAsync()
{
    return await _context.Stocks
        .Include(s => s.Comments) //stocklarla beraber ilgili commentsleri de getirir
        .ToListAsync();
}
    
        public async Task<Stock> GetByIdAsync(int id)//idlere göre stockla beraber commentsları da getir
        {
            return await _context.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> StockExists(int id)
        {
            return await _context.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            
            if (existingStock == null) return null;

            // 3. ELLE ATAMA YERİNE BURAYI KULLAN:
            // stockDto içindeki verileri existingStock üzerine yazar.
            _mapper.Map(stockDto, existingStock);

            await _context.SaveChangesAsync();
            return existingStock;
        }
    }
}