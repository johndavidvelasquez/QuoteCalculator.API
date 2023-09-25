using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addloanapplicationstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "LoanApplications");

            migrationBuilder.AddColumn<int>(
                name: "LoanApplicationStatus",
                table: "LoanApplications",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoanApplicationStatus",
                table: "LoanApplications");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "LoanApplications",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
