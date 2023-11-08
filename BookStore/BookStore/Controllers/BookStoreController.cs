using AutoMapper;
using BookStore.Entities;
using BookStore.Models;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/bookstore")]
    public class BookStoreController : ControllerBase
    {
        private readonly IBookStoreService _service;
        public BookStoreController(IBookStoreService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> CreateBookStore([FromBody] CreateBookStoreDto dto)
        {
            var id = await _service.Create(dto);
            return Created($"/api/bookstore/{id}", null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBookStore([FromBody] UpdateBookStoreDto dto, [FromRoute] int id)
        {
            await _service.Update(dto, id);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookStoreDto>>> GetAll()
        {
            var results = await _service.GetAll();
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookStoreDto>> Get([FromRoute] int id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) 
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}

