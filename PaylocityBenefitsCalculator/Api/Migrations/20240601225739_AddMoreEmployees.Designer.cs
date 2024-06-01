﻿// <auto-generated />
using System;
using Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Api.Migrations
{
    [DbContext(typeof(PaylocityDbContext))]
    [Migration("20240601225739_AddMoreEmployees")]
    partial class AddMoreEmployees
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.31");

            modelBuilder.Entity("Api.Models.BenefitsRates", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("HighSalaryCost")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("HighSalaryLimit")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("MonthlyBaseCost")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("MonthlyDependentCost")
                        .HasColumnType("TEXT");

                    b.Property<int>("OldDependentAgeLimit")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("OldDependentCost")
                        .HasColumnType("TEXT");

                    b.Property<int>("PaychecksPerYear")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("BenefitsRates");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            HighSalaryCost = 0.02m,
                            HighSalaryLimit = 80000.00m,
                            MonthlyBaseCost = 1000.00m,
                            MonthlyDependentCost = 600.00m,
                            OldDependentAgeLimit = 50,
                            OldDependentCost = 200.00m,
                            PaychecksPerYear = 26
                        });
                });

            modelBuilder.Entity("Api.Models.Dependent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<int>("Relationship")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Dependents");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateOfBirth = new DateTime(1998, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeId = 2,
                            FirstName = "Spouse",
                            LastName = "Morant",
                            Relationship = 1
                        },
                        new
                        {
                            Id = 2,
                            DateOfBirth = new DateTime(2020, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeId = 2,
                            FirstName = "Child1",
                            LastName = "Morant",
                            Relationship = 3
                        },
                        new
                        {
                            Id = 3,
                            DateOfBirth = new DateTime(2021, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeId = 2,
                            FirstName = "Child2",
                            LastName = "Morant",
                            Relationship = 3
                        },
                        new
                        {
                            Id = 4,
                            DateOfBirth = new DateTime(1974, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeId = 3,
                            FirstName = "DP",
                            LastName = "Jordan",
                            Relationship = 2
                        },
                        new
                        {
                            Id = 5,
                            DateOfBirth = new DateTime(1982, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeId = 4,
                            FirstName = "Wife",
                            LastName = "One",
                            Relationship = 1
                        },
                        new
                        {
                            Id = 6,
                            DateOfBirth = new DateTime(1976, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeId = 4,
                            FirstName = "Wife",
                            LastName = "Two",
                            Relationship = 1
                        },
                        new
                        {
                            Id = 7,
                            DateOfBirth = new DateTime(1982, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeId = 5,
                            FirstName = "Domestic",
                            LastName = "Partner",
                            Relationship = 2
                        },
                        new
                        {
                            Id = 8,
                            DateOfBirth = new DateTime(1976, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeId = 5,
                            FirstName = "Spouse",
                            LastName = "LikeAHouse",
                            Relationship = 1
                        });
                });

            modelBuilder.Entity("Api.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Salary")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateOfBirth = new DateTime(1984, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "LeBron",
                            LastName = "James",
                            Salary = 75420.99m
                        },
                        new
                        {
                            Id = 2,
                            DateOfBirth = new DateTime(1999, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Ja",
                            LastName = "Morant",
                            Salary = 92365.22m
                        },
                        new
                        {
                            Id = 3,
                            DateOfBirth = new DateTime(1963, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Michael",
                            LastName = "Jordan",
                            Salary = 143211.12m
                        },
                        new
                        {
                            Id = 4,
                            DateOfBirth = new DateTime(1963, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Poly",
                            LastName = "Gamist",
                            Salary = 145876.12m
                        },
                        new
                        {
                            Id = 5,
                            DateOfBirth = new DateTime(1963, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Rule",
                            LastName = "Breaker",
                            Salary = 145876.12m
                        });
                });

            modelBuilder.Entity("Api.Models.Dependent", b =>
                {
                    b.HasOne("Api.Models.Employee", "Employee")
                        .WithMany("Dependents")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Api.Models.Employee", b =>
                {
                    b.Navigation("Dependents");
                });
#pragma warning restore 612, 618
        }
    }
}
