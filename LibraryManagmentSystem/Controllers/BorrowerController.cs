using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using LibraryManagmentSystem.Core.DTOs;
using LibraryManagmentSystem.Models;
using LibraryManagmentSystem.Core.Models;
using LibraryManagmentSystem.Core.Interfaces.Services;

namespace LibraryManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BorrowersController : ControllerBase
    {
        private readonly IBorrowerService _borrowerService;
        private readonly IMapper _mapper;

        public BorrowersController(IBorrowerService borrowerService, IMapper mapper)
        {
            _borrowerService = borrowerService ?? throw new ArgumentNullException(nameof(borrowerService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBorrowerById(int id)
        {
            try
            {
                var borrower = await _borrowerService.GetBorrowerByIdAsync(id);
                if (borrower == null)
                {
                    return NotFound(new { Message = "Borrower not found." });
                }

                var borrowerDto = _mapper.Map<BorrowerDto>(borrower);
                return Ok(borrowerDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBorrowers()
        {
            try
            {
                var borrowers = await _borrowerService.GetAllBorrowersAsync();
                var borrowersDto = _mapper.Map<IEnumerable<BorrowerDto>>(borrowers);
                return Ok(borrowersDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBorrower([FromBody] BorrowerPostModel borrowerPostModel)
        {
            if (borrowerPostModel == null)
            {
                return BadRequest(new { Message = "Borrower cannot be null." });
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
                var borrowerEntity = _mapper.Map<Borrower>(borrowerPostModel);
                var result = await _borrowerService.AddBorrowerAsync(borrowerEntity);

                if (result)
                {
                    var borrowerDto = _mapper.Map<BorrowerDto>(borrowerEntity);
                    return CreatedAtAction(nameof(GetBorrowerById), new { id = borrowerEntity.Id }, borrowerDto);
                }
                else
                {
                    return Conflict(new { Message = "A borrower with the same identity or phone number already exists." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBorrower(int id, [FromBody] BorrowerPostModel borrowerPostModel)
        {
            if (borrowerPostModel == null)
            {
                return BadRequest(new { Message = "Borrower data is invalid." });
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
                var existingBorrower = await _borrowerService.GetBorrowerByIdAsync(id);
                if (existingBorrower == null)
                {
                    return NotFound(new { Message = "Borrower not found." });
                }

                var borrowerEntity = _mapper.Map<Borrower>(borrowerPostModel);
                borrowerEntity.Id = id;

                var result = await _borrowerService.UpdateBorrowerAsync(borrowerEntity);

                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "Failed to update the borrower." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorrower(int id)
        {
            try
            {
                var exists = await _borrowerService.BorrowerExistsAsync(id);
                if (!exists)
                {
                    return NotFound(new { Message = "Borrower not found." });
                }

                var result = await _borrowerService.DeleteBorrowerAsync(id);
                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "Failed to delete the borrower." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpGet("identity/{identity}")]
        public async Task<IActionResult> GetBorrowerByIdentity(string identity)
        {
            if (string.IsNullOrEmpty(identity))
            {
                return BadRequest(new { Message = "Identity cannot be null or empty." });
            }

            try
            {
                var borrower = await _borrowerService.GetBorrowerByIdentityAsync(identity);
                if (borrower == null)
                {
                    return NotFound(new { Message = "Borrower not found." });
                }

                var borrowerDto = _mapper.Map<BorrowerDto>(borrower);
                return Ok(borrowerDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpGet("phone/{phoneNumber}")]
        public async Task<IActionResult> GetBorrowerByPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return BadRequest(new { Message = "Phone number cannot be null or empty." });
            }

            try
            {
                var borrower = await _borrowerService.GetBorrowerByPhoneNumberAsync(phoneNumber);
                if (borrower == null)
                {
                    return NotFound(new { Message = "Borrower not found." });
                }

                var borrowerDto = _mapper.Map<BorrowerDto>(borrower);
                return Ok(borrowerDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }
    }
}
