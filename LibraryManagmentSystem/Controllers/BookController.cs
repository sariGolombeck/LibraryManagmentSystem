using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using LibraryManagmentSystem.Core.DTOs;
using LibraryManagmentSystem.Models;
using LibraryManagmentSystem.Core.Models;
using LibraryManagmentSystem.Core.Interfaces.Services;

namespace LibraryManagmentSystem.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookByIdAsync(int id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                if (book == null)
                {
                    return NotFound("Book not found.");
                }

                var bookDto = _mapper.Map<BookDto>(book);
                return Ok(bookDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync();
                var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);
                return Ok(booksDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBookAsync([FromBody] BookPostModel bookPostModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(ms => ms.Value.Errors.Count > 0)
                    .Select(ms => new
                    {
                        Field = ms.Key,
                        Errors = ms.Value.Errors.Select(e => e.ErrorMessage)
                    });
                return BadRequest(new { Errors = errors });
            }

            try
            {
                var bookEntity = _mapper.Map<Book>(bookPostModel);
                var isAdded = await _bookService.AddBookAsync(bookEntity);

                if (isAdded)
                {
                    return Ok(_mapper.Map<BookDto>(bookEntity));
                }
                else
                {
                    return StatusCode(500, "Failed to add the book.");
                }
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, $"Failed to add the book: {ex.Message}");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookAsync(int id, [FromBody] BookPostModel bookPostModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(ms => ms.Value.Errors.Count > 0)
                    .Select(ms => new
                    {
                        Field = ms.Key,
                        Errors = ms.Value.Errors.Select(e => e.ErrorMessage)
                    });
                return BadRequest(new { Errors = errors });
            }

            try
            {
                var bookEntity = _mapper.Map<Book>(bookPostModel);
                var isUpdated = await _bookService.UpdateBookAsync(id, bookEntity);

                if (isUpdated)
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode(500, "Failed to update the book.");
                }
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookAsync(int id)
        {
            try
            {
                var isDeleted = await _bookService.DeleteBookAsync(id);

                if (isDeleted)
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode(500, "Failed to delete the book.");
                }
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("exists/{id}")]
        public async Task<IActionResult> BookExistsAsync(int id)
        {
            try
            {
                var exists = await _bookService.BookExistsAsync(id);
                return Ok(exists);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("searchByTitle")]
        public async Task<IActionResult> SearchBooksByTitleAsync([FromQuery] string title)
        {
            try
            {
                var books = await _bookService.SearchByTitleAsync(title);
                var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);
                return Ok(booksDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("searchByAuthor")]
        public async Task<IActionResult> SearchBooksByAuthorAsync([FromQuery] string authorName)
        {
            try
            {
                var books = await _bookService.SearchByAuthorAsync(authorName);
                var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);
                return Ok(booksDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
