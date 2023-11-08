using AutoMapper;
using BookStore.Entities;
using BookStore.Models;

namespace BookStore.Services
{
    public class DataService<TBaseDto> where TBaseDto:BaseDto
    {
        private readonly IMapper _mapper;
        private readonly BookStoreDbContext _context;
        public DataService(BookStoreDbContext context, IMapper mapper) 
        {
            _mapper = mapper;
            _context = context;
        }

        public TBaseDto GetById(int id);
        public TBaseDto UpdateById(int id);
        public void DeleteById(int id);
        public IEnumerable<TBaseDto> GetAll();
        public void Create();

    }
}
