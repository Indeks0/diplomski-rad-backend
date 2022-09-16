using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WAZOI.Backend.DAL.Migrations
{
    public partial class examstart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateStart",
                table: "Exam",
                newName: "DateOpenStart");

            migrationBuilder.RenameColumn(
                name: "DateApplicationsDue",
                table: "Exam",
                newName: "DateOpenEnd");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOpenStart",
                table: "Exam",
                newName: "DateStart");

            migrationBuilder.RenameColumn(
                name: "DateOpenEnd",
                table: "Exam",
                newName: "DateApplicationsDue");
        }
    }
}
