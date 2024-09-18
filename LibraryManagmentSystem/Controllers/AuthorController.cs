
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagmentSystem.Core.DTOs;
using LibraryManagmentSystem.Models;
using System.Linq;
using LibraryManagmentSystem.Core.Models;
using LibraryManagmentSystem.Core.Interfaces.Services;

namespace LibraryManagmentSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }

        // GET: api/authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAllAuthors()
        {
            try
            {
                var authors = await _authorService.GetAllAuthorsAsync();
                return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authors));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/authors/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthorById(int id)
        {
            try
            {
                var author = await _authorService.GetAuthorByIdAsync(id);
                return Ok(_mapper.Map<AuthorDto>(author));
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Author with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/authors
        [HttpPost]
        public async Task<ActionResult<AuthorDto>> AddAuthor([FromBody] AuthorPostModel authorPostModel)
        {
            if (authorPostModel == null)
            {
                return BadRequest("Author data is null.");
            }

            if (!ModelState.IsValid)
            {
                // Handle ModelState validation errors
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
                var authorEntity = _mapper.Map<Author>(authorPostModel);
                var result = await _authorService.AddAuthorAsync(authorEntity);
                if (result)
                {
                    var createdAuthorDto = _mapper.Map<AuthorDto>(authorEntity);
                    return CreatedAtAction(nameof(GetAuthorById), new { id = createdAuthorDto.Id }, createdAuthorDto);
                }
                else
                {
                    return BadRequest("Failed to add the author.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/authors/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthor(int id, [FromBody] AuthorPostModel authorPostModel)
        {
            if (authorPostModel == null)
            {
                return BadRequest("Author data is null.");
            }

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
                var authorEntity = _mapper.Map<Author>(authorPostModel);
                var result = await _authorService.UpdateAuthorAsync(id, authorEntity);
                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound($"Author with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/authors/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            try
            {
                var result = await _authorService.DeleteAuthorAsync(id);
                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound($"Author with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/authors/exists/{id}
        [HttpGet("exists/{id}")]
        public async Task<ActionResult<bool>> AuthorExists(int id)
        {
            try
            {
                var exists = await _authorService.AuthorExistsAsync(id);
                return Ok(exists);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
