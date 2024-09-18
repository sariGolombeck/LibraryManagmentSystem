using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibraryManagementSystem.Core.Services;
using LibraryManagmentSystem.Core.DTOs;
using LibraryManagmentSystem.Models;
using LibraryManagmentSystem.Core.Interfaces.Services;
using LibraryManagmentSystem.Core.Models;

namespace LibraryManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookBorrowerController : ControllerBase
    {
        private readonly IBookBorrowerService _bookBorrowerService;
        private readonly IMapper _mapper;

        public BookBorrowerController(IBookBorrowerService bookBorrowerService, IMapper mapper)
        {
            _bookBorrowerService = bookBorrowerService ?? throw new ArgumentNullException(nameof(bookBorrowerService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookBorrowerDto>> GetBookBorrowerByIdAsync(int id)
        {
            try
            {
                var bookBorrowerEntity = await _bookBorrowerService.GetBookBorrowerByIdAsync(id);
                if (bookBorrowerEntity == null)
                {
                    return NotFound($"BookBorrower with ID {id} not found.");
                }

                var bookBorrowerDto = _mapper.Map<BookBorrowerDto>(bookBorrowerEntity);
                return Ok(bookBorrowerDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookBorrowerDto>>> GetAllBookBorrowersAsync()
        {
            var bookBorrowerEntities = await _bookBorrowerService.GetAllBookBorrowersAsync();
            var bookBorrowerDtos = _mapper.Map<IEnumerable<BookBorrowerDto>>(bookBorrowerEntities);
            return Ok(bookBorrowerDtos);
        }

        [HttpPost]
        public async Task<ActionResult> AddBookBorrowerAsync([FromBody] BookBorrowerPostModel bookBorrowerPostModel)
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
                var bookBorrowerEntity = _mapper.Map<BookBorrower>(bookBorrowerPostModel);
                var result = await _bookBorrowerService.AddBookBorrowerAsync(bookBorrowerEntity);

                if (!result)
                {
                    return StatusCode(500, "Failed to add the BookBorrower.");
                }
                return Ok(_mapper.Map<BookBorrowerDto>(bookBorrowerEntity));

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBookBorrowerAsync(int id, [FromBody] BookBorrowerPostModel bookBorrowerPostModel)
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
                var existingBookBorrowerEntity = await _bookBorrowerService.GetBookBorrowerByIdAsync(id);
                if (existingBookBorrowerEntity == null)
                {
                    return NotFound($"BookBorrower with ID {id} not found.");
                }

                var bookBorrowerEntity = _mapper.Map<BookBorrower>(bookBorrowerPostModel);
                var result = await _bookBorrowerService.UpdateBookBorrowerAsync(bookBorrowerEntity, id);

                if (!result)
                {
                    return StatusCode(500, "Failed to update the BookBorrower.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBookBorrowerAsync(int id)
        {
            try
            {
                var result = await _bookBorrowerService.DeleteBookBorrowerAsync(id);
                if (!result)
                {
                    return NotFound($"BookBorrower with ID {id} not found.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("borrower/{borrowerId}")]
        public async Task<ActionResult<IEnumerable<BookBorrowerDto>>> GetBorrowedBooksByBorrowerAsync(int borrowerId)
        {
            var bookBorrowerEntities = await _bookBorrowerService.GetBorrowedBooksByBorrowerAsync(borrowerId);
            var bookBorrowerDtos = _mapper.Map<IEnumerable<BookBorrowerDto>>(bookBorrowerEntities);
            return Ok(bookBorrowerDtos);
        }

        [HttpGet("book/{bookId}")]
        public async Task<ActionResult<IEnumerable<BookBorrowerDto>>> GetBorrowersByBookAsync(int bookId)
        {
            var bookBorrowerEntities = await _bookBorrowerService.GetBorrowersByBookAsync(bookId);
            var bookBorrowerDtos = _mapper.Map<IEnumerable<BookBorrowerDto>>(bookBorrowerEntities);
            return Ok(bookBorrowerDtos);
        }

        [HttpGet("overdue")]
        public async Task<ActionResult<IEnumerable<BookBorrowerDto>>> GetOverdueBooksAsync()
        {
            try
            {
                var overdueBookBorrowerEntities = await _bookBorrowerService.GetOverdueBooksAsync();
                var overdueBookBorrowerDtos = _mapper.Map<IEnumerable<BookBorrowerDto>>(overdueBookBorrowerEntities);
                return Ok(overdueBookBorrowerDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
