using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;
        public CommentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
           await _context.Comments.AddAsync(commentModel);
           await _context.SaveChangesAsync();
           return commentModel;
        }

      

        public async Task<List<Comment>> GetAllAsync()
        {
            return await  _context.Comments.ToListAsync();
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<Comment> UpdateAsync(int id,Comment comment)
        {
            var existingComment = _context.Comments.FirstOrDefault(c => c.Id == id);
            if (existingComment == null) return null;
            existingComment.Title = comment.Title;
            existingComment.Content = comment.Content;
            _context.Comments.Update(existingComment);
            _context.SaveChanges();
            return Task.FromResult(existingComment);
            
        }
         public async Task<bool> DeleteAsync(int id)
{
    var comment = await _context.Comments
                                .FirstOrDefaultAsync(c => c.Id == id);

    if (comment == null)
        return false;

    _context.Comments.Remove(comment);
    await _context.SaveChangesAsync();

    return true;
}
    }
}