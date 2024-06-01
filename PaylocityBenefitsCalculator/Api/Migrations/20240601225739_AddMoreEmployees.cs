using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    public partial class AddMoreEmployees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "Salary" },
                values: new object[] { 4, new DateTime(1963, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Poly", "Gamist", 145876.12m });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "Salary" },
                values: new object[] { 5, new DateTime(1963, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rule", "Breaker", 145876.12m });

            migrationBuilder.InsertData(
                table: "Dependents",
                columns: new[] { "Id", "DateOfBirth", "EmployeeId", "FirstName", "LastName", "Relationship" },
                values: new object[] { 5, new DateTime(1982, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Wife", "One", 1 });

            migrationBuilder.InsertData(
                table: "Dependents",
                columns: new[] { "Id", "DateOfBirth", "EmployeeId", "FirstName", "LastName", "Relationship" },
                values: new object[] { 6, new DateTime(1976, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Wife", "Two", 1 });

            migrationBuilder.InsertData(
                table: "Dependents",
                columns: new[] { "Id", "DateOfBirth", "EmployeeId", "FirstName", "LastName", "Relationship" },
                values: new object[] { 7, new DateTime(1982, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Domestic", "Partner", 2 });

            migrationBuilder.InsertData(
                table: "Dependents",
                columns: new[] { "Id", "DateOfBirth", "EmployeeId", "FirstName", "LastName", "Relationship" },
                values: new object[] { 8, new DateTime(1976, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Spouse", "LikeAHouse", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Dependents",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Dependents",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Dependents",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Dependents",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
