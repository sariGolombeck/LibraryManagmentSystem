using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagmentSystem.Core.Interfaces.Repositories;
using LibraryManagmentSystem.Core.Models;
using LibraryManagmentSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagmentSystem.Core.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DataContext _context;

        public AuthorRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await _context.Authors
                .FindAsync(id)
                ?? throw new KeyNotFoundException($"Author with ID {id} not found.");
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<bool> AddAsync(Author author)
        {
            if (author == null)
                throw new ArgumentNullException(nameof(author));

            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Author author)
        {
            if (author == null)
                throw new ArgumentNullException(nameof(author));

            var existingAuthor = await _context.Authors.FindAsync(author.Id);

            if (existingAuthor == null)
                throw new KeyNotFoundException($"No author found with ID {author.Id}");

            existingAuthor.FirstName = author.FirstName;
            existingAuthor.LastName = author.LastName;
            existingAuthor.Biography = author.Biography;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
                throw new KeyNotFoundException($"No author found with ID {id}");

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Authors.AnyAsync(a => a.Id == id);
        }

        public async Task<bool> ExistsByIdentityOrPassportAsync(string identityOrPassport)
        {
            return await _context.Authors.AnyAsync(a => a.IdentityOrPassport == identityOrPassport);
        }
    }
}
