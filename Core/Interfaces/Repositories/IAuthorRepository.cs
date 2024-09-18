
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using LibraryManagmentSystem.Core.Models;

//namespace LibraryManagmentSystem.Core.Repositories
//{
//    public interface IAuthorRepository
//    {
//        // Get an author by their unique identifier
//        Task<Author> GetByIdAsync(int id);

//        // Get all authors
//        Task<IEnumerable<Author>> GetAllAsync();

//        // Add a new author
//        Task AddAsync(Author author);

//        // Update an existing author
//        Task UpdateAsync(Author author);

//        // Delete an author by their unique identifier
//        Task DeleteAsync(int id);

//        // Check if an author exists by their unique identifier
//        Task<bool> ExistsAsync(int id);
//    }
//}
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagmentSystem.Core.Models;

namespace LibraryManagmentSystem.Core.Interfaces.Repositories
{
    public interface IAuthorRepository
    {
        Task<Author> GetByIdAsync(int id);
        Task<IEnumerable<Author>> GetAllAsync();
        Task<bool> AddAsync(Author author);
        Task<bool> UpdateAsync(Author author);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsByIdentityOrPassportAsync(string identityOrPassport);

    }
}
