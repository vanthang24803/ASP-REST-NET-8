using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class StockDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Symbol { get; set; } 
        public string? CompanyName { get; set; } 
        public decimal? Purchase { get; set; }
        public decimal? LastDiv { get; set; }
        public string? Industry { get; set; } 
        public long? MarketCap { get; set; }

        public List<CommentDto> Comments { get; set; } = new List<CommentDto>();

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}