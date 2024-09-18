
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagmentSystem.Core.Interfaces.Repositories;
using LibraryManagmentSystem.Core.Interfaces.Services;
using LibraryManagmentSystem.Core.Models;

namespace LibraryManagementSystem.Services
{
    public class BookBorrowerService : IBookBorrowerService
    {
        private readonly IBookBorrowerRepository _bookBorrowerRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IBorrowerRepository _borrowerRepository;

        public BookBorrowerService(
            IBookBorrowerRepository bookBorrowerRepository,
            IBookRepository bookRepository,
            IBorrowerRepository borrowerRepository)
        {
            _bookBorrowerRepository = bookBorrowerRepository;
            _bookRepository = bookRepository;
            _borrowerRepository = borrowerRepository;
        }

        public async Task<BookBorrower> GetBookBorrowerByIdAsync(int id)
        {
            var bookBorrower = await _bookBorrowerRepository.GetByIdAsync(id);
            if (bookBorrower == null)
            {
                throw new KeyNotFoundException($"BookBorrower with ID {id} not found.");
            }
            return bookBorrower;
        }

        public async Task<IEnumerable<BookBorrower>> GetAllBookBorrowersAsync()
        {
            return await _bookBorrowerRepository.GetAllAsync();
        }

        //public async Task<bool> AddBookBorrowerAsync(BookBorrower bookBorrower)
        //{
        //    try
        //    {
        //        // Validation
        //        if (bookBorrower.BookId <= 0 || bookBorrower.BorrowerId <= 0)
        //        {
        //            throw new ArgumentException("Invalid Book ID or Borrower ID.");
        //        }

        //        var book = await _bookRepository.GetByIdAsync(bookBorrower.BookId);
        //        var borrower = await _borrowerRepository.GetByIdAsync(bookBorrower.BorrowerId);

        //        if (book == null)
        //        {
        //            throw new KeyNotFoundException($"Book with ID {bookBorrower.BookId} not found.");
        //        }

        //        if (borrower == null)
        //        {
        //            throw new KeyNotFoundException($"Borrower with ID {bookBorrower.BorrowerId} not found.");
        //        }

        //        var existingBorrowerRecord = await _bookBorrowerRepository.GetByBookAndBorrowerAsync(bookBorrower.BookId, bookBorrower.BorrowerId);
        //        if (existingBorrowerRecord != null && !existingBorrowerRecord.IsReturned)
        //        {
        //            throw new InvalidOperationException("The book is already borrowed by this borrower.");
        //        }

        //        return await _bookBorrowerRepository.AddAsync(bookBorrower);
        //    }

        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error in AddBookBorrowerAsync: {ex.Message}");
        //        return false;
        //    }
        //}
        public async Task<bool> AddBookBorrowerAsync(BookBorrower bookBorrower)
        {
            try
            {
                // Validation
                if (bookBorrower.BookId <= 0 || bookBorrower.BorrowerId <= 0)
                {
                    throw new ArgumentException("Invalid Book ID or Borrower ID.");
                }

                var book = await _bookRepository.GetByIdAsync(bookBorrower.BookId);
                var borrower = await _borrowerRepository.GetByIdAsync(bookBorrower.BorrowerId);

                if (book == null)
                {
                    throw new KeyNotFoundException($"Book with ID {bookBorrower.BookId} not found.");
                }

                if (borrower == null)
                {
                    throw new KeyNotFoundException($"Borrower with ID {bookBorrower.BorrowerId} not found.");
                }

                var existingBorrowerRecord = await _bookBorrowerRepository.GetByBookAndBorrowerAsync(bookBorrower.BookId, bookBorrower.BorrowerId);
                if (existingBorrowerRecord != null && !existingBorrowerRecord.IsReturned)
                {
                    throw new InvalidOperationException("The book is already borrowed by this borrower.");
                }

                return await _bookBorrowerRepository.AddAsync(bookBorrower);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddBookBorrowerAsync: {ex.Message}");
                // Consider throwing or logging the error as needed
                throw;
            }
        }

        public async Task<bool> UpdateBookBorrowerAsync(BookBorrower bookBorrower, int id)
        {
            try
            {
                var existingBookBorrower = await _bookBorrowerRepository.GetByIdAsync(id);
                if (existingBookBorrower == null)
                {
                    throw new KeyNotFoundException($"BookBorrower with ID {id} not found.");
                }

                var book = await _bookRepository.GetByIdAsync(bookBorrower.BookId);
                var borrower = await _borrowerRepository.GetByIdAsync(bookBorrower.BorrowerId);

                if (book == null)
                {
                    throw new KeyNotFoundException($"Book with ID {bookBorrower.BookId} not found.");
                }

                if (borrower == null)
                {
                    throw new KeyNotFoundException($"Borrower with ID {bookBorrower.BorrowerId} not found.");
                }

                bookBorrower.Id = id;
                return await _bookBorrowerRepository.UpdateAsync(bookBorrower);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateBookBorrowerAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteBookBorrowerAsync(int id)
        {
            try
            {
                var bookBorrower = await _bookBorrowerRepository.GetByIdAsync(id);
                if (bookBorrower == null)
                {
                    throw new KeyNotFoundException($"BookBorrower with ID {id} not found.");
                }
                return await _bookBorrowerRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteBookBorrowerAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<BookBorrower>> GetBorrowedBooksByBorrowerAsync(int borrowerId)
        {
            return await _bookBorrowerRepository.GetBorrowedBooksByBorrowerAsync(borrowerId);
        }

        public async Task<IEnumerable<BookBorrower>> GetBorrowersByBookAsync(int bookId)
        {
            return await _bookBorrowerRepository.GetBorrowersByBookAsync(bookId);
        }

        public async Task<BookBorrower> GetByBookAndBorrowerAsync(int bookId, int borrowerId)
        {
            var bookBorrower = await _bookBorrowerRepository.GetByBookAndBorrowerAsync(bookId, borrowerId);
            if (bookBorrower == null)
            {
                throw new KeyNotFoundException($"No BookBorrower record found for Book ID {bookId} and Borrower ID {borrowerId}.");
            }
            return bookBorrower;
        }

        public async Task<bool> BookBorrowerExistsAsync(int id)
        {
            return await _bookBorrowerRepository.ExistsAsync(id);
        }

        public async Task<IEnumerable<BookBorrower>> GetOverdueBooksAsync()
        {
            var overdueBooks = await _bookBorrowerRepository.GetOverdueBooksAsync();
            return overdueBooks; 
        }
    }
}
