using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Models;

namespace Api.Mapper
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                StockId = commentModel.StockId,
                CreateAt = commentModel.CreateAt,
                UpdateAt = commentModel.UpdateAt,
            };
        }

        // public static Comment ToCommentFromCreate()
        // {

        // }
    }
}