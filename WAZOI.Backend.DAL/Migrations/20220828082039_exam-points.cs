using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WAZOI.Backend.DAL.Migrations
{
    public partial class exampoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalScore",
                table: "SolvedExams",
                newName: "ScoredPercentage");

            migrationBuilder.AddColumn<int>(
                name: "MaximumPoints",
                table: "SolvedExams",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ScoredPoints",
                table: "SolvedExams",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaximumPoints",
                table: "SolvedExams");

            migrationBuilder.DropColumn(
                name: "ScoredPoints",
                table: "SolvedExams");

            migrationBuilder.RenameColumn(
                name: "ScoredPercentage",
                table: "SolvedExams",
                newName: "TotalScore");
        }
    }
}
