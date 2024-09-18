using System;

namespace LibraryManagmentSystem.Core.Models
{
    public class BookBorrower
    {
        public int Id { get; set; }

        public Book Book { get; set; }
        public int BookId { get; set; }

        public Borrower Borrower { get; set; }
        public int BorrowerId { get; set; }

        public DateTime BorrowedDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedDate { get; set; }

        private bool _isReturned;

        // Indicates if the book has been returned
        public bool IsReturned
        {
            get => _isReturned;
            set
            {
                if (value && !_isReturned)
                {
                    ReturnedDate = DateTime.Now; // Set ReturnedDate to the current date and time
                }
                else if (!value)
                {
                    _isReturned = false;
                    ReturnedDate = null;
                }
                _isReturned = value;
            }
        }

        public int LoanDuration { get; set; }

        public BookBorrower()
        {
            LoanDuration = 14; // Default loan duration
            BorrowedDate = DateTime.Now;
            IsReturned = false; // Default value

            DueDate = BorrowedDate.AddDays(LoanDuration);
        }
    }
}
