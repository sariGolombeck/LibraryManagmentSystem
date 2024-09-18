using AutoMapper;
using LibraryManagmentSystem.Core.DTOs;
using LibraryManagmentSystem.Core.Models;


namespace LibraryManagmentSystem.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookDto, Book>().ReverseMap();
            CreateMap<AuthorDto, Author>().ReverseMap();
            CreateMap<BookBorrowerDto, BookBorrower>().ReverseMap();
            CreateMap<BorrowerDto, Borrower>().ReverseMap();
        }
    }
}
