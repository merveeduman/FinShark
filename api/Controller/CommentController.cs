using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Interfaces;
using api.Dtos.Comment;
using AutoMapper;
namespace api.Controller
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController: ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly IStockRepository _stockRepository;
        public CommentController(ICommentRepository commentRepository, IMapper mapper,IStockRepository stockRepository)
        {  _mapper = mapper;
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
        }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        if (!ModelState.IsValid)
                return BadRequest(ModelState);
        var comments = await _commentRepository.GetAllAsync();
         var commentsDtos = _mapper.Map<IEnumerable<CommentDto>>(comments);
            
            return Ok(commentsDtos);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        if (!ModelState.IsValid)
                return BadRequest(ModelState);
        var comment = await _commentRepository.GetByIdAsync(id);
        if (comment == null) return NotFound();

        
        return Ok(_mapper.Map<CommentDto>(comment));
    }
    [HttpPost("{stockId:int}")]
    public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto commentDto)
    {
        if (!ModelState.IsValid)
                return BadRequest(ModelState);
       if(!await _stockRepository.StockExists(stockId)) 
       {
           return BadRequest("Stock does not exist");
       }
       var commentModel = _mapper.Map<Comment>(commentDto);
       commentModel.StockId = stockId;
       await _commentRepository.CreateAsync(commentModel);
       return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, _mapper.Map<CommentDto>(commentModel));
               
            }

    [HttpPut("{id:int}")]
public async Task<IActionResult> Update(int id, UpdateCommentDto dto)
{
    if (!ModelState.IsValid)
                return BadRequest(ModelState);
    var comment = await _commentRepository.GetByIdAsync(id);
    if (comment == null)
        return NotFound();

    _mapper.Map(dto, comment);

    await _commentRepository.UpdateAsync(id,comment);

    return Ok(_mapper.Map<CommentDto>(comment));
}
[HttpDelete("{id:int}")]
public async Task<IActionResult> Delete(int id)
{
    if (!ModelState.IsValid)
                return BadRequest(ModelState);
    var success = await _commentRepository.DeleteAsync(id);
    if (!success)
        return NotFound("Comment does not exist");

    return NoContent();
    
}
}
}