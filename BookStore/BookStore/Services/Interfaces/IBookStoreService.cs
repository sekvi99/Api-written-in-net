using BookStore.Models;

namespace BookStore.Services.Interfaces
{
    public interface IBookStoreService
    {
        BookStoreDto GetById(int id);
        bool Delete(int id);
        bool Update(UpdateBookStoreDto dto, int id);
        IEnumerable<BookStoreDto> GetAll();
        int Create(CreateBookStoreDto dto);
    }
}
