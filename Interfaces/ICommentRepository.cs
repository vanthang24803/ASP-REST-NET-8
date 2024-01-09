using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();

        Task<Comment?> GetByIdAsync(string id);

        Task<Comment> CreateAsync(Comment comment);

        Task<Comment?> UpdateAsync(string id, Comment comment);

        Task<Comment?> DeleteAsync(string id);
    }
}