using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WAZOI.Backend.DAL.Migrations
{
    public partial class examstudents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RandomizedQuestionsOrder",
                table: "Exam",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RandomizedQuestionsOrder",
                table: "Exam");
        }
    }
}
