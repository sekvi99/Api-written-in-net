using AutoMapper;
using BookStore.Exceptions;
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

        private async Task<BookStore.Entities.BookStore> SelectById(int id)
        {
            _logger.LogTrace($"Selecting Book Store with Id: {id}");
            var bookStore = await _dbContext
                .BookStores
                .Include(bookStores => bookStores.Address)
                .Include(bookStores => bookStores.Books)
                .FirstOrDefaultAsync(bookStore => bookStore.Id == id);

            return bookStore;
        }

        public async Task<BookStoreDto> GetById(int id)
        {
            var bookStore = await SelectById(id);

            if (bookStore is null)
            {
                throw new NotFoundException("Book Store Not Found");
            }

            var bookStoreDto = _mapper.Map<BookStoreDto>(bookStore);
            return bookStoreDto;
        }

        public async Task<IEnumerable<BookStoreDto>> GetAll() 
        {
            _logger.LogTrace($"Selecting All BookStores From Database");
            var bookStores = await _dbContext
                .BookStores
                .Include(bookStores => bookStores.Address)
                .Include(bookStores => bookStores.Books)
                .ToListAsync();

            var bookStoreDtos = _mapper.Map<List<BookStoreDto>>(bookStores);
            return bookStoreDtos;
        }

        public async Task<int> Create(CreateBookStoreDto dto)
        {
            _logger.LogTrace($"Invoked method for creating new book Store");
            if (_dbContext.BookStores.Any(bs => bs.Name == dto.Name))
            {
                _logger.LogError("Provided Object Already Exist");
                throw new ResourceExistException("Book Store with provided name already exist");
            }

            var bookStore = _mapper.Map<BookStore.Entities.BookStore>(dto);
            _dbContext.BookStores.Add(bookStore);
            await _dbContext.SaveChangesAsync();

            return bookStore.Id;
        }

        public async Task Delete(int id)
        {
            _logger.LogTrace($"Invoked delete of book store with id: {id}");
            var bookStore = await SelectById(id);

            if (bookStore is null)
            {
                throw new NotFoundException("Book Store Not Found");
            }

            _dbContext.Remove(bookStore);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(UpdateBookStoreDto dto, int id)
        {
            _logger.LogTrace($"Invoked update of book store with id: {id}");
            var bookStore = await SelectById(id);

            if (bookStore is null)
            {
                throw new NotFoundException("Book Store Not Found");
            }

            bookStore.Name = dto.Name;
            bookStore.Description = dto.Description;
            bookStore.IsActive = dto.IsActive;

            await _dbContext.SaveChangesAsync();
        }
    }
}
