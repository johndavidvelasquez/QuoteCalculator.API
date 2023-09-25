using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addloanapplicationdetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Repayment",
                table: "LoanApplications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalFees",
                table: "LoanApplications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalInterest",
                table: "LoanApplications",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Repayment",
                table: "LoanApplications");

            migrationBuilder.DropColumn(
                name: "TotalFees",
                table: "LoanApplications");

            migrationBuilder.DropColumn(
                name: "TotalInterest",
                table: "LoanApplications");
        }
    }
}
