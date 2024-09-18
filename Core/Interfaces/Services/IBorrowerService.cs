//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using LibraryManagementSystem.Core.Models;
//using LibraryManagmentSystem.Core.Models;

//namespace LibraryManagmentSystem.Core.Services
//{
//    public interface IBorrowerService
//    {
//        // Retrieve a borrower by their unique identifier
//        Task<Borrower> GetBorrowerByIdAsync(int id);

//        // Retrieve all borrowers
//        Task<IEnumerable<Borrower>> GetAllBorrowersAsync();

//        // Add a new borrower
//        Task AddBorrowerAsync(Borrower borrower);

//        // Update an existing borrower
//        Task UpdateBorrowerAsync(Borrower borrower);

//        // Delete a borrower by their unique identifier
//        Task DeleteBorrowerAsync(int id);

//        // Check if a borrower exists by their unique identifier
//        Task<bool> BorrowerExistsAsync(int id);

//        // Retrieve a borrower by their identity address
//        Task<Borrower> GetBorrowerByIdentityAsync(string identity);

//        // Retrieve a borrower by their phone number
//        Task<Borrower> GetBorrowerByPhoneNumberAsync(string phoneNumber);
//    }
//}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagmentSystem.Core.Models;

namespace LibraryManagmentSystem.Core.Interfaces.Services
{
    public interface IBorrowerService
    {
        // Retrieve a borrower by their unique identifier
        Task<Borrower> GetBorrowerByIdAsync(int id);

        // Retrieve all borrowers
        Task<IEnumerable<Borrower>> GetAllBorrowersAsync();

        // Add a new borrower
        Task<bool> AddBorrowerAsync(Borrower borrower);

        // Update an existing borrower
        Task<bool> UpdateBorrowerAsync(Borrower borrower);

        // Delete a borrower by their unique identifier
        Task<bool> DeleteBorrowerAsync(int id);

        // Check if a borrower exists by their unique identifier
        Task<bool> BorrowerExistsAsync(int id);

        // Retrieve a borrower by their identity address
        Task<Borrower> GetBorrowerByIdentityAsync(string identity);

        // Retrieve a borrower by their phone number
        Task<Borrower> GetBorrowerByPhoneNumberAsync(string phoneNumber);
    }
}
