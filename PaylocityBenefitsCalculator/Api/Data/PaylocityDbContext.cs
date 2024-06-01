using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class PaylocityDbContext : DbContext
    {
        public PaylocityDbContext(DbContextOptions<PaylocityDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Dependent> Dependents { get; set; } = null!;
        public DbSet<BenefitsRates> BenefitsRates { get; set; } = null!;

        // Seed the local db
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    FirstName = "LeBron",
                    LastName = "James",
                    Salary = 75420.99m,
                    DateOfBirth = new DateTime(1984, 12, 30)
                },
                new Employee
                {
                    Id = 2,
                    FirstName = "Ja",
                    LastName = "Morant",
                    Salary = 92365.22m,
                    DateOfBirth = new DateTime(1999, 8, 10)
                },
                new Employee
                {
                    Id = 3,
                    FirstName = "Michael",
                    LastName = "Jordan",
                    Salary = 143211.12m,
                    DateOfBirth = new DateTime(1963, 2, 17)
                }
            );

            modelBuilder.Entity<Dependent>().HasData(
                new Dependent
                {
                    Id = 1,
                    FirstName = "Spouse",
                    LastName = "Morant",
                    Relationship = Relationship.Spouse,
                    DateOfBirth = new DateTime(1998, 3, 3),
                    EmployeeId = 2
                },
                new Dependent
                {
                    Id = 2,
                    FirstName = "Child1",
                    LastName = "Morant",
                    Relationship = Relationship.Child,
                    DateOfBirth = new DateTime(2020, 6, 23),
                    EmployeeId = 2
                },
                new Dependent
                {
                    Id = 3,
                    FirstName = "Child2",
                    LastName = "Morant",
                    Relationship = Relationship.Child,
                    DateOfBirth = new DateTime(2021, 5, 18),
                    EmployeeId = 2
                },
                new Dependent
                {
                    Id = 4,
                    FirstName = "DP",
                    LastName = "Jordan",
                    Relationship = Relationship.DomesticPartner,
                    DateOfBirth = new DateTime(1974, 1, 2),
                    EmployeeId = 3
                }
            );

            // I think this could conceptually be a keyless entry, since there's really only one rate for benefits every year, but EFCore was having issues adding keyless entity types and PK is best practices anyways
            modelBuilder.Entity<BenefitsRates>().HasData(
                new BenefitsRates
                {
                    Id = 1,
                    PaychecksPerYear = 26, // Bi-Weekly pay
                    MonthlyBaseCost = 1000.00m,
                    MonthlyDependentCost = 600.00m,
                    HighSalaryLimit = 80000.00m,
                    HighSalaryCost = 0.02m, // 2%
                    OldDependentAgeLimit = 50,
                    OldDependentCost = 200.00m
                }
            );
        }
    }
}
