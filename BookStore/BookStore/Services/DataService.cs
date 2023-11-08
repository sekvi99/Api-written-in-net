using AutoMapper;
using BookStore.Entities;
using BookStore.Models;

namespace BookStore.Services
{
    public abstract class DataService<TBaseDto> where TBaseDto:BaseDto
    {
        private readonly IMapper _mapper;
        private readonly BookStoreDbContext _context;
        public DataService(BookStoreDbContext context, IMapper mapper) 
        {
            _mapper = mapper;
            _context = context;
        }

        public abstract TBaseDto GetById(int id);
        public abstract TBaseDto UpdateById(int id);
        public abstract void DeleteById(int id);
        public abstract IEnumerable<TBaseDto> GetAll();
        public abstract void Create();

    }
}
