using AutoMapper;
using BookStore.Models;
using BookStore.Entities;

namespace BookStore.Mapper
{
    public class BookStoreMappingProfile : Profile
    {
        public BookStoreMappingProfile()
        {
            CreateMap<BookStore.Entities.BookStore, BookStoreDto>()
                .ForMember(member => member.City, bookStore => bookStore.MapFrom(s => s.Address.City))
                .ForMember(member => member.Street, bookStore => bookStore.MapFrom(s => s.Address.Street))
                .ForMember(member => member.PostalCode, bookStore => bookStore.MapFrom(s => s.Address.PostalCode));

            CreateMap<Book, BookDto>();

            CreateMap<CreateBookStoreDto, BookStore.Entities.BookStore>()
                .ForMember(m => m.Address, c => c.MapFrom(dto => new Address() { City = dto.City, PostalCode = dto.PostalCode, Street = dto.Street }));
        }
    }
}
