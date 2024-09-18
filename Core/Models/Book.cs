using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystem.Core.Models
{
    // Enumeration for book genres
    public enum BookGenre
    {
        Fiction,
        NonFiction,
        Mystery,
        Fantasy,
        ScienceFiction,
        Biography,
        Historical,
        Romance,
        Thriller,
        Children,
        Horror,
        Adventure
    }

    // Enumeration for languages
    public enum BookLanguage
    {
        English,
        Hebrew,
        French,
        Spanish,
        German,
        Chinese,
        Russian,
        Arabic,
        Japanese,
        Portuguese,
        Italian,
        Dutch,
        Korean,
        Greek,
        Turkish,
        Swedish
    }

    public class Book
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
