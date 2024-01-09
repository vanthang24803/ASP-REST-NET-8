using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Interfaces;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository
{
    public class CommentRepository(AppDbContext context) : ICommentRepository
    {

        public readonly AppDbContext _context = context;

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteAsync(string id)
        {
            var existingComment = await _context.Comments.FindAsync(id);

            if (existingComment is null)
            {
                return null;
            }

            _context.Comments.Remove(existingComment);
            await _context.SaveChangesAsync();
            return existingComment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            var comments = await _context.Comments.ToListAsync();
            return comments;
        }

        public async Task<Comment?> GetByIdAsync(string id)
        {
            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment is null)
            {
                return null;
            }

            return existingComment;
        }

        public async Task<Comment?> UpdateAsync(string id, Comment comment)
        {
            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment is null)
            {
                return null;
            }

            existingComment.Title = comment.Title;
            existingComment.Content = comment.Content;
            existingComment.UpdateAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return existingComment;
        }
    }
}