using BookStore.Models;

namespace BookStore.Services.Interfaces
{
    public interface IBookStoreService
    {
        Task<BookStoreDto> GetById(int id);
        Task Delete(int id);
        Task Update(UpdateBookStoreDto dto, int id);
        Task<IEnumerable<BookStoreDto>> GetAll();
        Task<int> Create(CreateBookStoreDto dto);
    }
}
