using System;
using System.ComponentModel.DataAnnotations;
using LibraryManagmentSystem.Core.Models;

namespace LibraryManagmentSystem.Models
{
    public class BookPostModel
    {
        // Title of the book
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "Title can't be longer than 200 characters.")]
        public string Title { get; set; }

        // Author of the book
        [Required(ErrorMessage = "AuthorId is required.")]
        public int AuthorId { get; set; }

        // ISBN of the book
        [Required(ErrorMessage = "ISBN is required.")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "ISBN must be between 10 and 13 characters long.")]
        public string ISBN { get; set; }

        // Publish date of the book
        [Required(ErrorMessage = "PublishDate is required.")]
        public DateTime PublishDate { get; set; }

        // Genre of the book
        [Required(ErrorMessage = "Genre is required.")]
        public BookGenre Genre { get; set; }

        // Copies available for the book
        [Required(ErrorMessage = "CopiesAvailable is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "CopiesAvailable must be greater than or equal to 0.")]
        public int CopiesAvailable { get; set; }

        // Total copies of the book
        [Required(ErrorMessage = "TotalCopies is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "TotalCopies must be greater than or equal to 0.")]
        public int TotalCopies { get; set; }

        // Language of the book
        [Required(ErrorMessage = "Language is required.")]
        public BookLanguage Language { get; set; }

        // Description of the book
        [StringLength(1000, ErrorMessage = "Description can't be longer than 1000 characters.")]
        public string Description { get; set; }
    }
}
