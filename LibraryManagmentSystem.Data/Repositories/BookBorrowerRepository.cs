
using LibraryManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagmentSystem.Core.Interfaces.Repositories;
using LibraryManagmentSystem.Core.Models;
using LibraryManagmentSystem.Data;

public class BookBorrowerRepository : IBookBorrowerRepository
{
    private readonly DataContext _context;

    public BookBorrowerRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BookBorrower>> GetAllAsync()
    {
        return await _context.BookBorrowers
            .Include(bb => bb.Book)
            .ToListAsync();
    }

    public async Task<BookBorrower> GetByIdAsync(int id)
    {
        return await _context.BookBorrowers
            .FirstOrDefaultAsync(bb => bb.Id == id);
    }

    //public async Task<bool> AddAsync(BookBorrower entity)
    //{
    //    try
    //    {
    //        var book = await _context.Books.FindAsync(entity.BookId);
    //        if (book == null)
    //            throw new Exception("The book does not exist.");

    //        if (book.CopiesAvailable <= 0)
    //            throw new Exception("No available copies of the book.");

    //        book.CopiesAvailable -= 1;
    //        _context.Books.Update(book);
    //        _context.BookBorrowers.Add(entity);
    //        await _context.SaveChangesAsync();
    //        return true;
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine($"Error in AddAsync: {ex.Message}");
    //        return false;
    //    }
    //}
    public async Task<bool> AddAsync(BookBorrower entity)
    {
        try
        {
            var book = await _context.Books.FindAsync(entity.BookId);
            if (book == null)
                throw new Exception("The book does not exist.");

            if (book.CopiesAvailable <= 0)
                throw new Exception("No available copies of the book.");

            book.CopiesAvailable -= 1;
            _context.Books.Update(book);
            _context.BookBorrowers.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in AddAsync: {ex.Message}");
            throw; // Ensure the exception is propagated
        }
    }

    public async Task<bool> UpdateAsync(BookBorrower entity)
    {
        try
        {
            var existingEntity = await _context.BookBorrowers
                .Include(bb => bb.Book)
                .FirstOrDefaultAsync(bb => bb.Id == entity.Id);

            if (existingEntity == null)
                throw new Exception("The loan record does not exist.");

            var book = existingEntity.Book;

            if (entity.IsReturned && !existingEntity.IsReturned)
            {
                existingEntity.IsReturned = true;
                existingEntity.ReturnedDate = entity.ReturnedDate ?? DateTime.Now;
                book.CopiesAvailable += 1;
            }
            else if (!entity.IsReturned && existingEntity.IsReturned)
            {
                existingEntity.IsReturned = false;
                existingEntity.ReturnedDate = null;
                book.CopiesAvailable -= 1;
            }
            else
            {
                existingEntity.IsReturned = entity.IsReturned;
                existingEntity.ReturnedDate = entity.ReturnedDate;
            }

            _context.Books.Update(book);
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in UpdateAsync: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var existingEntity = await _context.BookBorrowers
                .Include(bb => bb.Book)
                .FirstOrDefaultAsync(bb => bb.Id == id);

            if (existingEntity == null)
                throw new Exception("The loan record does not exist.");

            var book = existingEntity.Book;

            if (!existingEntity.IsReturned)
            {
                book.CopiesAvailable += 1;
            }

            _context.Books.Update(book);
            _context.BookBorrowers.Remove(existingEntity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in DeleteAsync: {ex.Message}");
            return false;
        }
    }

    public async Task<IEnumerable<BookBorrower>> GetBorrowedBooksByBorrowerAsync(int borrowerId)
    {
        return await _context.BookBorrowers
            .Where(bb => bb.BorrowerId == borrowerId)
            .Include(bb => bb.Book)
            .ToListAsync();
    }

    public async Task<IEnumerable<BookBorrower>> GetBorrowersByBookAsync(int bookId)
    {
        return await _context.BookBorrowers
            .Where(bb => bb.BookId == bookId)
            .Include(bb => bb.Borrower)
            .ToListAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.BookBorrowers.AnyAsync(bb => bb.Id == id);
    }

    public async Task<BookBorrower> GetByBookAndBorrowerAsync(int bookId, int borrowerId)
    {
        return await _context.BookBorrowers
            .Include(bb => bb.Book)
            .Include(bb => bb.Borrower)
            .FirstOrDefaultAsync(bb => bb.BookId == bookId && bb.BorrowerId == borrowerId);
    }

    public async Task<IEnumerable<BookBorrower>> GetOverdueBooksAsync()
    {
        return await _context.BookBorrowers
            .Where(bb => !bb.IsReturned && bb.DueDate < DateTime.Now)
            .ToListAsync();
    }
}
