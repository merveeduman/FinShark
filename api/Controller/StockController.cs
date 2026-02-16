using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using api.Dtos;
using api.Models;

namespace api.Controller
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public StockController(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        {
            // Veritabanından veriler gelene kadar bekle (await)
            var stocks = await _context.Stocks.ToListAsync();
            
            var stockDtos = _mapper.Map<IEnumerable<StockDto>>(stocks);
            
            return Ok(stockDtos);
        }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id ==  id);
        if (stock == null) return NotFound();

        
        return Ok(_mapper.Map<StockDto>(stock));
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CreateStockRequestDto request)
    {
        // DTO'dan Stock modeline dönüşüm
        var stock = _mapper.Map<Stock>(request);
        
        _context.Stocks.AddAsync(stock);
        await _context.SaveChangesAsync();
        
        // Oluşturulan nesnenin DTO'sunu döndürüyoruz
        return CreatedAtAction(nameof(GetById), new { id = stock.Id }, _mapper.Map<StockDto>(stock));
    }
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody]UpdateStockRequestDto updateDto)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id); //nesneyı bilmiyorsak dönmesin
        if (stock == null) return NotFound();

        // DTO'dan Stock modeline dönüşüm
        _mapper.Map(updateDto, stock);
        
        _context.SaveChangesAsync();
        
        // Güncellenen nesnenin DTO'sunu döndürüyoruz
        return Ok(_mapper.Map<StockDto>(stock));
    }
    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var stock = _context.Stocks.FirstOrDefault(s => s.Id == id);
        if (stock == null) return NotFound();

        _context.Stocks.Remove(stock);
        _context.SaveChanges();

        return Ok("Stock deleted successfully.");
    }
}
}