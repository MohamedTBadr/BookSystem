using AutoMapper;
using BookReview.Data.DTO.AuthorDTOs;
using BookReview.Data.DTO.BooksDTOs;
using BookReview.Data.Models;

namespace BookReview.Services
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() {
            CreateMap<BookCreateDTO, Book>().ReverseMap();
            CreateMap<BookDTO, Book>();

            CreateMap<Book, BookDTO>().ForMember(m => m.AuthorName, options => options.MapFrom(src => src.Author.AuthorName));
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<AuthorCreateDTO, Author>().ReverseMap();
            CreateMap<AuthorUpdateDTO, Author>().ReverseMap();
            
        }
    }
}
