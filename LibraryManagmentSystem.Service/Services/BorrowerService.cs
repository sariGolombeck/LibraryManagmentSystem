using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LibraryManagmentSystem.Core.Interfaces.Repositories;
using LibraryManagmentSystem.Core.Interfaces.Services;
using LibraryManagmentSystem.Core.Models;

namespace LibraryManagementSystem.Core.Services
{
    public class BorrowerService : IBorrowerService
    {
        private readonly IBorrowerRepository _borrowerRepository;

        public BorrowerService(IBorrowerRepository borrowerRepository)
        {
            _borrowerRepository = borrowerRepository ?? throw new ArgumentNullException(nameof(borrowerRepository));
        }

        public async Task<Borrower> GetBorrowerByIdAsync(int id)
        {
            var borrower = await _borrowerRepository.GetByIdAsync(id);
            if (borrower == null)
            {
                throw new KeyNotFoundException($"Borrower with ID {id} not found");
            }
            return borrower;
        }

        public async Task<IEnumerable<Borrower>> GetAllBorrowersAsync()
        {
            return await _borrowerRepository.GetAllAsync();
        }

        public async Task<bool> AddBorrowerAsync(Borrower borrower)
        {
            if (borrower == null)
            {
                throw new ArgumentNullException(nameof(borrower));
            }

            if (string.IsNullOrWhiteSpace(borrower.Identity))
            {
                throw new ArgumentException("Identity cannot be null or empty", nameof(borrower.Identity));
            }

            // Check if a borrower with the same Identity already exists
            var existingBorrower = await _borrowerRepository.GetByIdentityAsync(borrower.Identity);
            if (existingBorrower != null)
            {
                throw new InvalidOperationException($"A borrower with the identity '{borrower.Identity}' already exists.");
            }

            return await _borrowerRepository.AddAsync(borrower);
        }

        public async Task<bool> UpdateBorrowerAsync(Borrower borrower)
        {
            if (borrower == null)
            {
                throw new ArgumentNullException(nameof(borrower));
            }

            if (string.IsNullOrWhiteSpace(borrower.Identity))
            {
                throw new ArgumentException("Identity cannot be null or empty", nameof(borrower.Identity));
            }

            var existingBorrower = await _borrowerRepository.GetByIdAsync(borrower.Id);
            if (existingBorrower == null)
            {
                throw new KeyNotFoundException($"Borrower with ID {borrower.Id} not found");
            }

            // Check if a borrower with the same Identity exists, excluding the current borrower
            var existingBorrowerWithIdentity = await _borrowerRepository.GetByIdentityAsync(borrower.Identity);
            if (existingBorrowerWithIdentity != null && existingBorrowerWithIdentity.Id != borrower.Id)
            {
                throw new InvalidOperationException($"A borrower with the identity '{borrower.Identity}' already exists.");
            }

            return await _borrowerRepository.UpdateAsync(borrower);
        }

        public async Task<bool> DeleteBorrowerAsync(int id)
        {
            if (!await _borrowerRepository.ExistsAsync(id))
            {
                throw new KeyNotFoundException($"Borrower with ID {id} not found");
            }

            return await _borrowerRepository.DeleteAsync(id);
        }

        public async Task<bool> BorrowerExistsAsync(int id)
        {
            return await _borrowerRepository.ExistsAsync(id);
        }

        public async Task<Borrower> GetBorrowerByIdentityAsync(string identity)
        {
            if (string.IsNullOrWhiteSpace(identity))
            {
                throw new ArgumentException("Identity cannot be null or empty", nameof(identity));
            }

            var borrower = await _borrowerRepository.GetByIdentityAsync(identity);
            if (borrower == null)
            {
                throw new KeyNotFoundException($"Borrower with identity '{identity}' not found");
            }
            return borrower;
        }

        public async Task<Borrower> GetBorrowerByPhoneNumberAsync(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ArgumentException("Phone number cannot be null or empty", nameof(phoneNumber));
            }

            var borrower = await _borrowerRepository.GetByPhoneNumberAsync(phoneNumber);
            if (borrower == null)
            {
                throw new KeyNotFoundException($"Borrower with phone number '{phoneNumber}' not found");
            }
            return borrower;
        }
    }
}
