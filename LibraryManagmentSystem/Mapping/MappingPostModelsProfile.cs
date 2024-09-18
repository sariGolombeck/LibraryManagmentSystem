using AutoMapper;
using LibraryManagmentSystem.Models;
using LibraryManagmentSystem.Core.Models;

namespace LibraryManagmentSystem.Mapping
{
    public class MappingPostModelsProfile:Profile
    {
        public MappingPostModelsProfile()
        {
            CreateMap<BookPostModel, Book>().ReverseMap();
            CreateMap<AuthorPostModel, Author>().ReverseMap();
            CreateMap<BookBorrowerPostModel, BookBorrower>().ReverseMap();
            CreateMap<BorrowerPostModel, Borrower>().ReverseMap();

        }
    }
}
