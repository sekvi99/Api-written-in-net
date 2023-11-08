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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var id = _service.Create(dto);
                return Created($"/api/bookstore/{id}", null);
            }
            catch (Exception ex)
            {
                return Conflict("Provided BookStore already exist");
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateBookStore([FromBody] UpdateBookStoreDto dto, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isUpdated = _service.Update(dto, id);

            if(!isUpdated)
            {
                return NotFound();
            }

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
            
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id) 
        {
            var isDeleted = _service.Delete(id);

            if (!isDeleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

