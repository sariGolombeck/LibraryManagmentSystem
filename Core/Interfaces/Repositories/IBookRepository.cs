using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagmentSystem.Core.Models;

namespace LibraryManagmentSystem.Core.Interfaces.Repositories
{
    public interface IBookRepository
    {
        Task<Book> GetByIdAsync(int id);
        Task<IEnumerable<Book>> GetAllAsync();
        Task<bool> AddAsync(Book book);
        Task<bool> UpdateAsync(Book book);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);

        // Search for books by title
        Task<IEnumerable<Book>> SearchByTitleAsync(string title);

        // Search for books by author name
        Task<IEnumerable<Book>> SearchByAuthorAsync(string authorName);
    }
}
