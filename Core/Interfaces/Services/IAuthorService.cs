//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using LibraryManagmentSystem.Core.Models;

//namespace LibraryManagmentSystem.Core.Services
//{
//    public interface IAuthorService
//    {
//        // Retrieve an author by their unique identifier
//        Task<Author> GetAuthorByIdAsync(int id);

//        // Retrieve all authors
//        Task<IEnumerable<Author>> GetAllAuthorsAsync();

//        // Add a new author
//        Task AddAuthorAsync(Author author);

//        // Update an existing author
//        Task UpdateAuthorAsync(int id,Author author);

//        // Delete an author by their unique identifier
//        Task DeleteAuthorAsync(int id);

//        // Check if an author exists by their unique identifier
//        Task<bool> AuthorExistsAsync(int id);
//    }
//}
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagmentSystem.Core.Models;

namespace LibraryManagmentSystem.Core.Interfaces.Services
{
    public interface IAuthorService
    {
        Task<Author> GetAuthorByIdAsync(int id);
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<bool> AddAuthorAsync(Author author);
        Task<bool> UpdateAuthorAsync(int id, Author author);
        Task<bool> DeleteAuthorAsync(int id);
        Task<bool> AuthorExistsAsync(int id);
    }
}
