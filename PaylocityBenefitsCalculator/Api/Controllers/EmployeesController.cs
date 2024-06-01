using Api.Data;
using Api.Dtos.BenefitsRates;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly PaylocityDbContext _context;

    public EmployeesController(PaylocityDbContext context)
    {
        _context = context;
    }

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        try
        {
            // Fetch a specific employee by ID, including related Dependents entities
            var employee = await _context.Employees
                .Select(e => new GetEmployeeDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName, 
                    LastName = e.LastName, 
                    Salary = e.Salary, 
                    DateOfBirth = e.DateOfBirth, 
                    Dependents = e.Dependents.Select(d => new GetDependentDto
                    {
                        Id = d.Id,
                        FirstName = d.FirstName, 
                        LastName = d.LastName, 
                        DateOfBirth = d.DateOfBirth, 
                        Relationship = d.Relationship 
                    }).ToList() // Convert dependents to a list
                })
                .FirstOrDefaultAsync(e => e.Id == id); // Execute the query and get the first matching result

            // Return a not found response if the employee does not exist
            if (employee == null)
            {
                return NotFound(new ApiResponse<GetEmployeeDto>
                {
                    Data = null,
                    Message = "Employee not found",
                    Success = false
                });
            }

            // Check for multiple spouses or domestic partners
            if (employee.Dependents.Count(d => d.Relationship == Relationship.Spouse || d.Relationship == Relationship.DomesticPartner) > 1)
            {
                return BadRequest(new ApiResponse<GetEmployeeDto>
                {
                    Data = null,
                    Message = "An employee may only have 1 spouse or domestic partner (not both).",
                    Success = false
                });
            }

            // Return the employee data in an ApiResponse
            return Ok(new ApiResponse<GetEmployeeDto>
            {
                Data = employee,
                Message = "Success",
                Success = true
            });
        }
        catch (Exception ex)
        {
            // Return an error response in case of exception
            return StatusCode(500, new ApiResponse<GetEmployeeDto>
            {
                Data = null,
                Message = $"An error occurred: {ex.Message}",
                Success = false
            });
        }
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        try
        {
            // Fetch all employees from the database, including related Dependents entities
            var employees = await _context.Employees
                .Select(e => new GetEmployeeDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName, 
                    LastName = e.LastName, 
                    Salary = e.Salary, 
                    DateOfBirth = e.DateOfBirth, 
                    Dependents = e.Dependents.Select(d => new GetDependentDto
                    {
                        Id = d.Id,
                        FirstName = d.FirstName, 
                        LastName = d.LastName, 
                        DateOfBirth = d.DateOfBirth, 
                        Relationship = d.Relationship 
                    }).ToList() // Convert dependents to a list
                })
                .ToListAsync(); // Execute the query and convert the result to a list

            // Check for multiple spouses or domestic partners in each employee
            foreach (var employee in employees)
            {
                if (employee.Dependents.Count(d => d.Relationship == Relationship.Spouse || d.Relationship == Relationship.DomesticPartner) > 1)
                {
                    return BadRequest(new ApiResponse<List<GetEmployeeDto>>
                    {
                        Data = null,
                        Message = $"Employee with ID {employee.Id} has multiple spouses or domestic partners.",
                        Success = false
                    });
                }
            }

            // Return the list of employees in an ApiResponse
            return Ok(new ApiResponse<List<GetEmployeeDto>>
            {
                Data = employees,
                Message = "Success",
                Success = true
            });
        }
        catch (Exception ex)
        {
            // Return an error response in case of exception
            return StatusCode(500, new ApiResponse<List<GetEmployeeDto>>
            {
                Data = null,
                Message = $"An error occurred: {ex.Message}",
                Success = false
            });
        }
    }

    // GET: api/Employees/{id}/paycheck
    [HttpGet("{id}/paycheck")]
    public async Task<ActionResult<ApiResponse<decimal>>> GetPaycheck(int id)
    {
        try
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return NotFound(new ApiResponse<GetEmployeeDto>
                {
                    Data = null,
                    Message = "Employee not found",
                    Success = false
                });
            }

            // Calculate paycheck using a private method
            var paycheckDto = CalculatePaycheck(employee);

            return Ok(new ApiResponse<decimal>
            {
                Data = paycheckDto.Result,
                Message = "Success",
                Success = true
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<GetEmployeeDto>
            {
                Data = null,
                Message = $"An error occurred: {ex.Message}",
                Success = false
            });
        }
    }

    // I figured breaking this somewhat complex calculation into its own private method would help in keeping the controller methods clean and more maintainable.
    private async Task<decimal> CalculatePaycheck(Employee employee)
    {
        // Fetch the current Benefits Rates
        var benefitsRates = await _context.BenefitsRates
            .Select(e => new GetBenefitsRatesDto
            {
                Id = e.Id,
                PaychecksPerYear = e.PaychecksPerYear,
                MonthlyBaseCost = e.MonthlyBaseCost,
                MonthlyDependentCost = e.MonthlyDependentCost,
                HighSalaryLimit = e.HighSalaryLimit,
                HighSalaryCost = e.HighSalaryCost,
                OldDependentAgeLimit = e.OldDependentAgeLimit,
                OldDependentCost = e.OldDependentCost,
            })
            .FirstOrDefaultAsync(); // Execute the query. There will only be one entry here. See my comment in the DBContext 

        var grossPay = employee.Salary / benefitsRates.PaychecksPerYear; // Bi-weekly pay

        // Calculate deductions
        var monthlyDeductions = benefitsRates.MonthlyBaseCost;
        monthlyDeductions += employee.Dependents.Count * benefitsRates.MonthlyDependentCost;
        monthlyDeductions += employee.Dependents.Count(d => d.DateOfBirth.AddYears(benefitsRates.OldDependentAgeLimit) <= DateTime.Now) * benefitsRates.OldDependentCost;

        if (employee.Salary > benefitsRates.HighSalaryLimit)
        {
            monthlyDeductions += (employee.Salary * benefitsRates.HighSalaryCost) / 12;
        }

        var biWeeklyDeductions = monthlyDeductions / 2;
        var netPay = grossPay - biWeeklyDeductions;

        // Return the Net Pay
        return netPay;
    }
}
