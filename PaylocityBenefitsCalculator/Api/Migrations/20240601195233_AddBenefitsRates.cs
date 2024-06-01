using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    public partial class AddBenefitsRates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BenefitsRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PaychecksPerYear = table.Column<int>(type: "INTEGER", nullable: false),
                    MonthlyBaseCost = table.Column<decimal>(type: "TEXT", nullable: false),
                    MonthlyDependentCost = table.Column<decimal>(type: "TEXT", nullable: false),
                    HighSalaryLimit = table.Column<decimal>(type: "TEXT", nullable: false),
                    HighSalaryCost = table.Column<decimal>(type: "TEXT", nullable: false),
                    OldDependentAgeLimit = table.Column<int>(type: "INTEGER", nullable: false),
                    OldDependentCost = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenefitsRates", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BenefitsRates",
                columns: new[] { "Id", "HighSalaryCost", "HighSalaryLimit", "MonthlyBaseCost", "MonthlyDependentCost", "OldDependentAgeLimit", "OldDependentCost", "PaychecksPerYear" },
                values: new object[] { 1, 0.02m, 80000.00m, 1000.00m, 600.00m, 50, 200.00m, 26 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BenefitsRates");
        }
    }
}
