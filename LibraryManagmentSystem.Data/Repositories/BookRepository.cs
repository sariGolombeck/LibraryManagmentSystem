using LibraryManagmentSystem.Core.Interfaces.Repositories;
using LibraryManagmentSystem.Core.Models;
using LibraryManagmentSystem.Data;
using Microsoft.EntityFrameworkCore;

public class BookRepository : IBookRepository
{
    private readonly DataContext _context;

    public BookRepository(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Book> GetByIdAsync(int id)
    {
        return await _context.Books
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _context.Books
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> AddAsync(Book book)
    {
        try
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new Exception($"Failed to add the book: {ex.Message}", ex);
        }
    }

    public async Task<bool> UpdateAsync(Book book)
    {
        try
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new Exception($"Concurrency error: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var book = await GetByIdAsync(id);
        if (book == null)
            throw new KeyNotFoundException($"No book found with ID {id}");

        try
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new Exception($"Error deleting book with ID {id}: {ex.Message}", ex);
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Books
            .AnyAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<Book>> SearchByTitleAsync(string title)
    {
        return await _context.Books
            .Where(b => b.Title.Contains(title))
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Book>> SearchByAuthorAsync(string authorName)
    {
        return await _context.Books
            .Where(b => _context.Authors.Any(a => a.Id == b.AuthorId && (a.FirstName + " " + a.LastName).Contains(authorName)))
            .AsNoTracking()
            .ToListAsync();
    }
}
