using AutoMapper;
using BookStore.Entities;
using BookStore.Models;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/bookstore")]
    public class BookStoreController : ControllerBase
    {
        private readonly IBookStoreService _service;
        private readonly IMapper _mapper;
        public BookStoreController(IMapper mapper, IBookStoreService service)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult CreateBookStore([FromBody] CreateBookStoreDto dto)
        {
            var id = _service.Create(dto);
            return Created($"/api/bookstore/{id}", null);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateBookStore([FromBody] UpdateBookStoreDto dto, [FromRoute] int id)
        {
            _service.Update(dto, id);
            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookStoreDto>> GetAll()
        {
            var results = _service.GetAll();
            return Ok(results);
        }

        [HttpGet("{id}")]
        public ActionResult<BookStoreDto> Get([FromRoute] int id)
        {
            var result = _service.GetById(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id) 
        {
            _service.Delete(id);
            return Ok();
        }
    }
}

