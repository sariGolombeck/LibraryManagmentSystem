
using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystem.Models
{
    public class BookBorrowerPostModel
    {
        // Reference to the borrowed book
        [Required(ErrorMessage = "BookId is required.")]
        public int BookId { get; set; }

        // Reference to the borrower who borrowed the book
        [Required(ErrorMessage = "BorrowerId is required.")]
        public int BorrowerId { get; set; }

        // The date the book was borrowed
        [Required(ErrorMessage = "BorrowedDate is required.")]
        public DateTime BorrowedDate { get; set; }

        // The due date by which the book should be returned
        [Required(ErrorMessage = "DueDate is required.")]
        public DateTime DueDate { get; set; }

        // Loan duration in days
        [Required(ErrorMessage = "LoanDuration is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "LoanDuration must be greater than 0.")]
        public int LoanDuration { get; set; }

        // Status to indicate if the book has been returned
        // Default is false
        public bool IsReturned { get; set; } = false; // Set default value to false

        public BookBorrowerPostModel()
        {
            LoanDuration = 14; 
            BorrowedDate = DateTime.Now;
            DueDate = BorrowedDate.AddDays(LoanDuration); 
        }
    }
}
