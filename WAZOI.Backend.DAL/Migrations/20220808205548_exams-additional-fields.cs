using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WAZOI.Backend.DAL.Migrations
{
    public partial class examsadditionalfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrdinalNumber",
                table: "ExamQuestions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "ExamQuestions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DurationInMins",
                table: "Exam",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrdinalNumber",
                table: "ExamQuestions");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "ExamQuestions");

            migrationBuilder.DropColumn(
                name: "DurationInMins",
                table: "Exam");
        }
    }
}
