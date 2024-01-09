using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Dtos.Stock;
using Api.Helper;
using Api.Interfaces;
using Api.Mapper;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controller
{
    [ApiController]
    [Route("api/stock")]
    public class StockController(AppDbContext context, IStockRepository stockRepo) : ControllerBase
    {
        private readonly AppDbContext _context = context;
        private readonly IStockRepository _stockRepo = stockRepo;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stocks = await _stockRepo.GetAllStocks(query);

            var stockDto = stocks.Select(s => s.ToStockDto());

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailStockById([FromRoute] string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stockRepo.GetStockById(id);

            if (stock is null)
            {
                return NotFound("Stock not found");
            }

            return Ok(stock.ToStockDto());

        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = request.ToStockFromCreateDTO();

            await _stockRepo.CreateStock(stockModel);

            return CreatedAtAction(nameof(GetDetailStockById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateStockRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = await _stockRepo.UpdateStock(id, request);

            if (stockModel is null)
            {
                return NotFound("Stock not found");
            }

            return Ok(stockModel.ToStockDto());
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateOptions([FromRoute] string id, [FromBody] UpdateStockOptionsRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = await _stockRepo.UpdateOptions(id, request);

            if (stockModel is null)
            {
                return NotFound("Stock not found");
            }

            return Ok(stockModel.ToStockDto());
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DELETE([FromRoute] string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = await _stockRepo.DeleteStock(id);

            if (stockModel is null)
            {
                return NotFound("Stock not found");
            }
            return Ok("Delete Successfully");
        }
    }
}