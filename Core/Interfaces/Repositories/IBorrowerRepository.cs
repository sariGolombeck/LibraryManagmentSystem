//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using LibraryManagementSystem.Core.Models;
//using LibraryManagmentSystem.Core.Models;

//namespace LibraryManagmentSystem.Core.Repositories
//{
//    public interface IBorrowerRepository
//    {
//        // Retrieve a borrower by their unique identifier
//        Task<Borrower> GetByIdAsync(int id);

//        // Retrieve all borrowers
//        Task<IEnumerable<Borrower>> GetAllAsync();

//        // Add a new borrower
//        Task AddAsync(Borrower borrower);

//        // Update an existing borrower
//        Task UpdateAsync(Borrower borrower);

//        // Delete a borrower by their unique identifier
//        Task DeleteAsync(int id);

//        // Check if a borrower exists by their unique identifier
//        Task<bool> ExistsAsync(int id);

//        // Retrieve a borrower by their identity address
//        Task<Borrower> GetByIdentityAsync(string identity);

//        // Retrieve a borrower by their phone number
//        Task<Borrower> GetByPhoneNumberAsync(string phoneNumber);
//    }
//}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagmentSystem.Core.Models;

namespace LibraryManagmentSystem.Core.Interfaces.Repositories
{
    public interface IBorrowerRepository
    {
        // Retrieve a borrower by their unique identifier
        Task<Borrower> GetByIdAsync(int id);

        // Retrieve all borrowers
        Task<IEnumerable<Borrower>> GetAllAsync();

        Task<bool> AddAsync(Borrower borrower);

        // Update an existing borrower
        Task<bool> UpdateAsync(Borrower borrower);

        // Delete a borrower by their unique identifier
        Task<bool> DeleteAsync(int id);

        // Check if a borrower exists by their unique identifier
        Task<bool> ExistsAsync(int id);

        // Retrieve a borrower by their identity address
        Task<Borrower> GetByIdentityAsync(string identity);

        // Retrieve a borrower by their phone number
        Task<Borrower> GetByPhoneNumberAsync(string phoneNumber);
    }
}
