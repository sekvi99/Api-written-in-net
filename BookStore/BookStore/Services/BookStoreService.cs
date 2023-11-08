using AutoMapper;
using BookStore.Models;
using BookStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class BookStoreService : IBookStoreService
    {
        private readonly BookStore.Entities.BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<BookStoreService> _logger;
        public BookStoreService(BookStore.Entities.BookStoreDbContext dbContext, IMapper mapper, ILogger<BookStoreService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        private BookStore.Entities.BookStore SelectById(int id)
        {
            _logger.LogTrace($"Selecting Book Store with Id: {id}");
            var bookStore = _dbContext
                .BookStores
                .Include(bookStores => bookStores.Address)
                .Include(bookStores => bookStores.Books)
                .FirstOrDefault(bookStore => bookStore.Id == id);

            return bookStore;
        }

        public BookStoreDto GetById(int id)
        {
            var bookStore = this.SelectById(id);

            if (bookStore is null) return null;

            var bookStoreDto = _mapper.Map<BookStoreDto>(bookStore);
            return bookStoreDto;
        }

        public IEnumerable<BookStoreDto> GetAll() 
        {
            _logger.LogTrace($"Selecting All BookStores From Database");
            var bookStores = _dbContext
                .BookStores
                .Include(bookStores => bookStores.Address)
                .Include(bookStores => bookStores.Books)
                .ToList();

            var bookStoreDtos = _mapper.Map<List<BookStoreDto>>(bookStores);
            return bookStoreDtos;

        }

        public int Create(CreateBookStoreDto dto)
        {
            _logger.LogTrace($"Invoked method for creating new book Store");
            if (_dbContext.BookStores.Any(bs => bs.Name == dto.Name))
            {
                _logger.LogError("Provided Object Already Exist");
                throw new Exception("Object already exist");
            }

            var bookStore = _mapper.Map<BookStore.Entities.BookStore>(dto);
            _dbContext.BookStores.Add(bookStore);
            _dbContext.SaveChanges();

            return bookStore.Id;
        }

        public bool Delete(int id)
        {
            _logger.LogTrace($"Invoked delete of book store with id: {id}");
            var bookStore = this.SelectById(id);

            if (bookStore is null) return false;

            _dbContext.Remove(bookStore);
            _dbContext.SaveChanges();
            return true;
        }

        public bool Update(UpdateBookStoreDto dto, int id)
        {
            _logger.LogTrace($"Invoked update of book store with id: {id}");
            var bookStore = SelectById(id);

            if (bookStore is null) return false;

            bookStore.Name = dto.Name;
            bookStore.Description = dto.Description;
            bookStore.IsActive = dto.IsActive;

            _dbContext.SaveChanges();
            return true;
        }
    }
}
