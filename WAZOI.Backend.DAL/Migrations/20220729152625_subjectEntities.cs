using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WAZOI.Backend.DAL.Migrations
{
    public partial class subjectEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectTeachers",
                table: "SubjectTeachers");

            migrationBuilder.DropIndex(
                name: "IX_SubjectTeachers_SubjectId",
                table: "SubjectTeachers");

            migrationBuilder.DropIndex(
                name: "IX_SubjectTeachers_UserId",
                table: "SubjectTeachers");

            migrationBuilder.DropIndex(
                name: "IX_SubjectStudents_UserId",
                table: "SubjectStudents");

            migrationBuilder.DropColumn(
                name: "id",
                table: "SubjectTeachers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectTeachers",
                table: "SubjectTeachers",
                columns: new[] { "SubjectId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeachers_UserId",
                table: "SubjectTeachers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectStudents_UserId",
                table: "SubjectStudents",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectTeachers",
                table: "SubjectTeachers");

            migrationBuilder.DropIndex(
                name: "IX_SubjectTeachers_UserId",
                table: "SubjectTeachers");

            migrationBuilder.DropIndex(
                name: "IX_SubjectStudents_UserId",
                table: "SubjectStudents");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "SubjectTeachers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectTeachers",
                table: "SubjectTeachers",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeachers_SubjectId",
                table: "SubjectTeachers",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeachers_UserId",
                table: "SubjectTeachers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubjectStudents_UserId",
                table: "SubjectStudents",
                column: "UserId",
                unique: true);
        }
    }
}
