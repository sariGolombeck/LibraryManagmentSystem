using System;
using LibraryManagmentSystem.Core.Models;

namespace LibraryManagmentSystem.Core.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishDate { get; set; }
        public BookGenre Genre { get; set; }
        public int CopiesAvailable { get; set; }
        public int TotalCopies { get; set; }
        public BookLanguage Language { get; set; }
        public string Description { get; set; }
    }
}
