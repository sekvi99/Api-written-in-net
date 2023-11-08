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
        private readonly BookStore.Entities.BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public BookStoreController(BookStore.Entities.BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;   
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookStoreDto>> GetAll()
        {
            var bookStores = _dbContext
                .BookStores
                .Include(bookStores => bookStores.Address)
                .Include(bookStores => bookStores.Books)
                .ToList();

            var bookStoreDtos = _mapper.Map<List<BookStoreDto>>(bookStores);
            return Ok(bookStoreDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<BookStoreDto> Get([FromRoute] int id)
        {
            var bookStore = _dbContext
                .BookStores
                .Include(bookStores => bookStores.Address)
                .Include(bookStores => bookStores.Books)
                .FirstOrDefault(bookStore =>  bookStore.Id == id);
            
            if (bookStore is null)
            {
                return NotFound();
            }

            var bookStoreDto = _mapper.Map<BookStoreDto>(bookStore);
            return Ok(bookStoreDto);
        }
    }
}

