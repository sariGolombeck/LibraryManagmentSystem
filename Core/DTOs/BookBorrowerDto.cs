using System;

namespace LibraryManagmentSystem.Core.DTOs
{
    public class BookBorrowerDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int BorrowerId { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedDate { get; set; }

        // Status indicating if the book has been returned
        public bool IsReturned => ReturnedDate.HasValue;
    }
}
