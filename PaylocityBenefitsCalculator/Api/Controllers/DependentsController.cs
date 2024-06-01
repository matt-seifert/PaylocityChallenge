using Api.Data;
using Api.Dtos.Dependent;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly PaylocityDbContext _context;

    public DependentsController(PaylocityDbContext context)
    {
        _context = context;
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        try
        {
            // Fetch a specific dependent by ID
            var dependent = await _context.Dependents
                .Select(d => new GetDependentDto
                {
                    // Map to fields in DTO
                    Id = d.Id,
                    FirstName = d.FirstName, 
                    LastName = d.LastName, 
                    DateOfBirth = d.DateOfBirth, 
                    Relationship = d.Relationship 
                })
                .FirstOrDefaultAsync(d => d.Id == id); // Execute the query and get the first matching result

            // Return a not found response if the dependent does not exist
            if (dependent == null)
            {
                return NotFound(new ApiResponse<GetDependentDto>
                {
                    Data = null,
                    Message = "Dependent not found",
                    Success = false
                });
            }

            // Return the dependent data in an ApiResponse
            return Ok(new ApiResponse<GetDependentDto>
            {
                Data = dependent,
                Message = "Success",
                Success = true
            });
        }
        catch (Exception ex)
        {
            // Return an error response in case of exception
            return StatusCode(500, new ApiResponse<GetDependentDto>
            {
                Data = null,
                Message = $"An error occurred: {ex.Message}",
                Success = false
            });
        }
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        try
        {
            // Fetch all dependents from the database
            var dependents = await _context.Dependents
                .Select(d => new GetDependentDto
                {
                    // Map to fields in DTO
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    DateOfBirth = d.DateOfBirth,
                    Relationship = d.Relationship
                })
                .ToListAsync(); // Execute the query and convert the result to a list

            // Return the list of dependents in an ApiResponse
            return Ok(new ApiResponse<List<GetDependentDto>>
            {
                Data = dependents,
                Message = "Success",
                Success = true
            });
        }
        catch (Exception ex)
        {
            // Return an error response in case of exception
            return StatusCode(500, new ApiResponse<List<GetDependentDto>>
            {
                Data = null,
                Message = $"An error occurred: {ex.Message}",
                Success = false
            });
        }
    }
}
