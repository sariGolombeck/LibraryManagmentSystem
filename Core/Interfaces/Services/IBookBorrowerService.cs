
using LibraryManagmentSystem.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Core.Interfaces.Services
{
    public interface IBookBorrowerService
    {
        Task<BookBorrower> GetBookBorrowerByIdAsync(int id);
        Task<IEnumerable<BookBorrower>> GetAllBookBorrowersAsync();
        Task<bool> AddBookBorrowerAsync(BookBorrower bookBorrower);
        Task<bool> UpdateBookBorrowerAsync(BookBorrower bookBorrower, int id);
        Task<bool> DeleteBookBorrowerAsync(int id);
        Task<IEnumerable<BookBorrower>> GetBorrowedBooksByBorrowerAsync(int borrowerId);
        Task<IEnumerable<BookBorrower>> GetBorrowersByBookAsync(int bookId);
        Task<BookBorrower> GetByBookAndBorrowerAsync(int bookId, int borrowerId);
        Task<bool> BookBorrowerExistsAsync(int id);
        Task<IEnumerable<BookBorrower>> GetOverdueBooksAsync();

    }
}
