using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagmentSystem.Core.Interfaces.Repositories;
using LibraryManagmentSystem.Core.Interfaces.Services;
using LibraryManagmentSystem.Core.Models;

namespace LibraryManagmentSystem.Core.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID provided.");
            }

            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                throw new KeyNotFoundException($"No author found with ID {id}.");
            }

            return author;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            try
            {
                return await _authorRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching all authors.", ex);
            }
        }

        public async Task<bool> AddAuthorAsync(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author), "Author data cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(author.FirstName))
            {
                throw new ArgumentException("The First Name field must be filled.");
            }

            if (await _authorRepository.ExistsByIdentityOrPassportAsync(author.IdentityOrPassport))
            {
                throw new ArgumentException($"An author with IdentityOrPassport {author.IdentityOrPassport} already exists.");
            }

            try
            {
                return await _authorRepository.AddAsync(author);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding the author.", ex);
            }
        }

        public async Task<bool> UpdateAuthorAsync(int id, Author author)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID provided.");
            }

            author.Id = id;

            if (string.IsNullOrWhiteSpace(author.FirstName))
            {
                throw new ArgumentException("Author first name is required.");
            }

            try
            {
                var existingAuthor = await _authorRepository.GetByIdAsync(id);
                if (existingAuthor == null)
                {
                    throw new KeyNotFoundException($"No author found with ID {id}.");
                }

                if (existingAuthor.IdentityOrPassport != author.IdentityOrPassport)
                {
                    if (await _authorRepository.ExistsByIdentityOrPassportAsync(author.IdentityOrPassport))
                    {
                        throw new ArgumentException($"An author with IdentityOrPassport {author.IdentityOrPassport} already exists.");
                    }
                }

                return await _authorRepository.UpdateAsync(author);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the author.", ex);
            }
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID provided.");
            }

            try
            {
                var existingAuthor = await _authorRepository.GetByIdAsync(id);
                if (existingAuthor == null)
                {
                    throw new KeyNotFoundException($"No author found with ID {id}.");
                }

                return await _authorRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while deleting the author.", ex);
            }
        }

        public async Task<bool> AuthorExistsAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID provided.");
            }

            try
            {
                return await _authorRepository.ExistsAsync(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while checking if the author exists.", ex);
            }
        }
    }
}
