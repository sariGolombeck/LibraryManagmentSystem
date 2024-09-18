using LibraryManagmentSystem.Core.Interfaces.Repositories;
using LibraryManagmentSystem.Core.Interfaces.Services;
using LibraryManagmentSystem.Core.Models;

namespace LibraryManagmentSystem.Core.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                throw new KeyNotFoundException($"No book found with ID {id}");
            return book;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<bool> AddBookAsync(Book book)
        {
            await ValidateBookAsync(book);
            await EnsureUniqueBookAsync(book);

            return await _bookRepository.AddAsync(book);
        }

        public async Task<bool> UpdateBookAsync(int id, Book book)
        {
            var existingBook = await _bookRepository.GetByIdAsync(id);
            if (existingBook == null)
                throw new KeyNotFoundException($"No book found with ID {id}");

            await ValidateBookAsync(book);
            await EnsureUniqueBookAsync(book, id);

            existingBook.Title = book.Title;
            existingBook.AuthorId = book.AuthorId;
            existingBook.ISBN = book.ISBN;
            existingBook.PublishDate = book.PublishDate;
            existingBook.Genre = book.Genre;
            existingBook.CopiesAvailable = book.CopiesAvailable;
            existingBook.TotalCopies = book.TotalCopies;
            existingBook.Language = book.Language;
            existingBook.Description = book.Description;

            return await _bookRepository.UpdateAsync(existingBook);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            return await _bookRepository.DeleteAsync(id);
        }

        public async Task<bool> BookExistsAsync(int id)
        {
            return await _bookRepository.ExistsAsync(id);
        }

        public async Task<IEnumerable<Book>> SearchByTitleAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty or whitespace.", nameof(title));

            return await _bookRepository.SearchByTitleAsync(title);
        }

        public async Task<IEnumerable<Book>> SearchByAuthorAsync(string authorName)
        {
            if (string.IsNullOrWhiteSpace(authorName))
                throw new ArgumentException("Author name cannot be empty or whitespace.", nameof(authorName));

            return await _bookRepository.SearchByAuthorAsync(authorName);
        }

        private async Task ValidateBookAsync(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            var authorExists = await _authorRepository.ExistsAsync(book.AuthorId);
            if (!authorExists)
                throw new KeyNotFoundException($"Author with ID {book.AuthorId} does not exist.");
        }

        private async Task EnsureUniqueBookAsync(Book book, int? bookId = null)
        {
            var existingBooks = await _bookRepository.GetAllAsync();
            if (existingBooks.Any(b => b.ISBN == book.ISBN && (!bookId.HasValue || b.Id != bookId.Value)))
                throw new InvalidOperationException($"A book with ISBN '{book.ISBN}' already exists.");

            if (existingBooks.Any(b => b.Title == book.Title && b.AuthorId == book.AuthorId && (!bookId.HasValue || b.Id != bookId.Value)))
                throw new InvalidOperationException($"A book with the title '{book.Title}' by author with ID {book.AuthorId} already exists.");
        }
    }
}
