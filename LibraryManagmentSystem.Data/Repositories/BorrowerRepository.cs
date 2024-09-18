using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagmentSystem.Core.Interfaces.Repositories;
using LibraryManagmentSystem.Core.Models;
using LibraryManagmentSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Data.Repositories
{
    public class BorrowerRepository : IBorrowerRepository
    {
        private readonly DataContext _context;

        public BorrowerRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Borrower> GetByIdAsync(int id)
        {
            return await _context.Borrowers
                .Where(b => b.IsActive && b.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Borrower>> GetAllAsync()
        {
            return await _context.Borrowers
                .Where(b => b.IsActive)
                .ToListAsync();
        }

        public async Task<bool> AddAsync(Borrower borrower)
        {
            if (borrower == null)
            {
                throw new ArgumentNullException(nameof(borrower));
            }

            var existingBorrower = await _context.Borrowers
                .FirstOrDefaultAsync(b => b.Identity == borrower.Identity || b.PhoneNumber == borrower.PhoneNumber);

            if (existingBorrower != null)
            {
                if (!existingBorrower.IsActive)
                {
                    existingBorrower.IsActive = true;
                    _context.Borrowers.Update(existingBorrower);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                await _context.Borrowers.AddAsync(borrower);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Borrower borrower)
        {
            if (borrower == null)
            {
                throw new ArgumentNullException(nameof(borrower));
            }

            var existingBorrower = await _context.Borrowers.FindAsync(borrower.Id);

            if (existingBorrower == null || !existingBorrower.IsActive)
            {
                return false;
            }

            existingBorrower.FirstName = borrower.FirstName;
            existingBorrower.LastName = borrower.LastName;
            existingBorrower.PhoneNumber = borrower.PhoneNumber;
            existingBorrower.Email = borrower.Email;
            existingBorrower.City = borrower.City;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var borrower = await _context.Borrowers.FindAsync(id);
            if (borrower == null)
            {
                return false;
            }

            if (!borrower.IsActive)
            {
                return false;
            }

            borrower.IsActive = false;
            _context.Borrowers.Update(borrower);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Borrowers
                .AnyAsync(b => b.Id == id && b.IsActive);
        }

        public async Task<Borrower> GetByIdentityAsync(string identity)
        {
            return await _context.Borrowers
                .FirstOrDefaultAsync(b => b.Identity == identity && b.IsActive);
        }

        public async Task<Borrower> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.Borrowers
                .FirstOrDefaultAsync(b => b.PhoneNumber == phoneNumber && b.IsActive);
        }

        public async Task<IEnumerable<Borrower>> GetAllWithDeletedAsync()
        {
            return await _context.Borrowers.ToListAsync();
        }
    }
}
