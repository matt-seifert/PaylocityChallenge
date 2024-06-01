using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Xunit;

namespace ApiTests.IntegrationTests;

public class EmployeeIntegrationTests : IntegrationTest
{
    [Fact]
    // PASSED
    public async Task WhenAskedForAllEmployees_ShouldReturnAllEmployees()
    {
        var response = await HttpClient.GetAsync("/api/v1/employees");
        var employees = new List<GetEmployeeDto>
        {
            new()
            {
                Id = 1,
                FirstName = "LeBron",
                LastName = "James",
                Salary = 75420.99m,
                DateOfBirth = new DateTime(1984, 12, 30)
            },
            new()
            {
                Id = 2,
                FirstName = "Ja",
                LastName = "Morant",
                Salary = 92365.22m,
                DateOfBirth = new DateTime(1999, 8, 10),
                Dependents = new List<GetDependentDto>
                {
                    new()
                    {
                        Id = 1,
                        FirstName = "Spouse",
                        LastName = "Morant",
                        Relationship = Relationship.Spouse,
                        DateOfBirth = new DateTime(1998, 3, 3)
                    },
                    new()
                    {
                        Id = 2,
                        FirstName = "Child1",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2020, 6, 23)
                    },
                    new()
                    {
                        Id = 3,
                        FirstName = "Child2",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2021, 5, 18)
                    }
                }
            },
            new()
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Jordan",
                Salary = 143211.12m,
                DateOfBirth = new DateTime(1963, 2, 17),
                Dependents = new List<GetDependentDto>
                {
                    new()
                    {
                        Id = 4,
                        FirstName = "DP",
                        LastName = "Jordan",
                        Relationship = Relationship.DomesticPartner,
                        DateOfBirth = new DateTime(1974, 1, 2)
                    }
                }
            }
        };
        await response.ShouldReturn(HttpStatusCode.OK, employees);
    }

    [Fact]
    //task: make test pass -- PASSED
    public async Task WhenAskedForAnEmployee_ShouldReturnCorrectEmployee()
    {
        var response = await HttpClient.GetAsync("/api/v1/employees/1");
        var employee = new GetEmployeeDto
        {
            Id = 1,
            FirstName = "LeBron",
            LastName = "James",
            Salary = 75420.99m,
            DateOfBirth = new DateTime(1984, 12, 30)
        };
        await response.ShouldReturn(HttpStatusCode.OK, employee);
    }
    
    [Fact]
    //task: make test pass -- PASSED
    public async Task WhenAskedForANonexistentEmployee_ShouldReturn404()
    {
        var response = await HttpClient.GetAsync($"/api/v1/employees/{int.MinValue}");
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }

    [Fact]
    // PASSED!
    public async Task WhenAskedForEmployeePaycheck_ShouldReturnCorrectPaycheck()
    {
        var response = await HttpClient.GetAsync($"/api/v1/employees/1/paycheck");

        // Do calculations based on King James' salary
        var expectedSalary = 75420.99m;
        var expectedGrossPay = expectedSalary / 26; // Bi-weekly gross pay
        var expectedMonthlyBaseCost = 1000;
        var expectedMonthlyDependentCost = 0; // No dependents in this case
        var expectedAdditionalOldDependentCost = 0; // No dependents over 50
        var expectedHighSalaryCost = 0; // Salary is below 80,000

        var expectedMonthlyDeductions = expectedMonthlyBaseCost + expectedMonthlyDependentCost + expectedAdditionalOldDependentCost + expectedHighSalaryCost;
        var expectedBiWeeklyDeductions = expectedMonthlyDeductions / 2;
        var expectedNetPay = expectedGrossPay - expectedBiWeeklyDeductions;

        await response.ShouldReturn(HttpStatusCode.OK, expectedNetPay);
    }

    [Fact]
    // PASSED!
    public async Task WhenEmployeeHasMoreThanOneSpouseOrSignificantOther_ShouldReturn400()
    {
        var response = await HttpClient.GetAsync("/api/v1/employees/4");
        await response.ShouldReturn(HttpStatusCode.BadRequest);
    }
}

