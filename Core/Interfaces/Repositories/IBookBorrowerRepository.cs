//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using LibraryManagmentSystem.Core.Models;

//namespace LibraryManagmentSystem.Core.Repositories
//{
//    public interface IBookBorrowerRepository
//    {
//        // Get a book-borrower record by its unique identifier
//        Task<BookBorrower> GetByIdAsync(int id);

//        // Get all book-borrower records
//        Task<IEnumerable<BookBorrower>> GetAllAsync();

//        // Add a new book-borrower record
//        Task AddAsync(BookBorrower bookBorrower);

//        // Update an existing book-borrower record
//        Task UpdateAsync(BookBorrower bookBorrower);

//        // Delete a book-borrower record by its unique identifier
//        Task DeleteAsync(int id);

//        // Get all borrowed books by a specific borrower
//        Task<IEnumerable<BookBorrower>> GetBorrowedBooksByBorrowerAsync(int borrowerId);

//        // Get all borrowers of a specific book
//        Task<IEnumerable<BookBorrower>> GetBorrowersByBookAsync(int bookId);

//        // Check if a book-borrower record exists by its unique identifier
//        Task<bool> ExistsAsync(int id);

//        // Get a book-borrower record by book ID and borrower ID
//        Task<BookBorrower> GetByBookAndBorrowerAsync(int bookId, int borrowerId);
//    }
//}
using LibraryManagmentSystem.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Core.Interfaces.Repositories
{
    public interface IBookBorrowerRepository
    {
        Task<IEnumerable<BookBorrower>> GetAllAsync();
        Task<BookBorrower> GetByIdAsync(int id);
        Task<bool> AddAsync(BookBorrower entity);
        Task<bool> UpdateAsync(BookBorrower entity);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<BookBorrower>> GetBorrowedBooksByBorrowerAsync(int borrowerId);
        Task<IEnumerable<BookBorrower>> GetBorrowersByBookAsync(int bookId);
        Task<BookBorrower> GetByBookAndBorrowerAsync(int bookId, int borrowerId);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<BookBorrower>> GetOverdueBooksAsync();


    }
}
