using AutoMapper;
using BookStore.Entities;
using BookStore.Models;
using BookStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class BookStoreService : IDataService<BookStoreDto>
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public BookStoreService(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public BookStoreDto GetById(int id)
        {
            var bookStore = _dbContext
                .BookStores
                .Include(bookStores => bookStores.Address)
                .Include(bookStores => bookStores.Books)
                .FirstOrDefault(bookStore => bookStore.Id == id);

            if (bookStore is null) return null;

            var bookStoreDto = _mapper.Map<BookStoreDto>(bookStore);
            return bookStoreDto;
        }

        public IEnumerable<BookStoreDto> GetAll()
        {
            var bookStores = _dbContext
                .BookStores
                .Include(bookStores => bookStores.Address)
                .Include(bookStores => bookStores.Books)
                .ToList();

            var bookStoreDtos = _mapper.Map<List<BookStoreDto>>(bookStores);
            return bookStoreDtos;
        }

        public void Create(BookStoreDto dto)
        {
            var bookStore = _mapper.Map<BookStore.Entities.BookStore>(dto);
            _dbContext.BookStores.Add(bookStore);
            _dbContext.SaveChanges();
        }
    }
}
