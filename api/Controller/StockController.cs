using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using api.Dtos;
using api.Models;
using api.Interfaces;
using System.Threading.Tasks;
using api.Helpers;

namespace api.Controller
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IStockRepository _stockRepository;


    public StockController( IMapper mapper, IStockRepository stockRepository)
    {
        _mapper = mapper;
        _stockRepository = stockRepository;
    }

    [HttpGet]
   public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    var stocks = await _stockRepository.GetAllAsync(query);
    var stockDtos = _mapper.Map<IEnumerable<StockDto>>(stocks);

    return Ok(stockDtos);
}

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (!ModelState.IsValid)
                return BadRequest(ModelState);
        var stock = await _stockRepository.GetByIdAsync(id);
        if (stock == null) return NotFound();

        
        return Ok(_mapper.Map<StockDto>(stock));
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CreateStockRequestDto request)
    {
        if (!ModelState.IsValid)
                return BadRequest(ModelState);
        // DTO'dan Stock modeline dönüşüm
        var stock = _mapper.Map<Stock>(request);
        
        
        await _stockRepository.CreateAsync(stock);
        
        // Oluşturulan nesnenin DTO'sunu döndürüyoruz
        return CreatedAtAction(nameof(GetById), new { id = stock.Id }, _mapper.Map<StockDto>(stock));
    }
    [HttpPut("{id:int}")]
public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
{
    if (!ModelState.IsValid)
                return BadRequest(ModelState);
    // Repository zaten güncellemeyi ve SaveChangesAsync() işlemini içeride yaptı!
    var stock = await _stockRepository.UpdateAsync(id, updateDto);
    
    if (stock == null) return NotFound();

    
    return Ok(_mapper.Map<StockDto>(stock));
}

[HttpDelete("{id:int}")]
public async Task<IActionResult> Delete([FromRoute] int id)
{
    
    // Repository zaten bulma, silme ve kaydetme işlemini yaptı
    var stock = await _stockRepository.DeleteAsync(id);
    
    if (stock == null) return NotFound();

    // Artık _context.Stocks.Remove veya _context.SaveChanges() yazmana GEREK YOK.
    return Ok("Stock deleted successfully.");
}
}
}