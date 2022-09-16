using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WAZOI.Backend.DAL.Migrations
{
    public partial class subjectnotices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectTeachers",
                table: "SubjectTeachers");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "SubjectTeachers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectTeachers",
                table: "SubjectTeachers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SubjectNotices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectNotices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectNotices_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectNotices_SubjectTeachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "SubjectTeachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeachers_SubjectId",
                table: "SubjectTeachers",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectNotices_SubjectId",
                table: "SubjectNotices",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectNotices_TeacherId",
                table: "SubjectNotices",
                column: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectNotices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectTeachers",
                table: "SubjectTeachers");

            migrationBuilder.DropIndex(
                name: "IX_SubjectTeachers_SubjectId",
                table: "SubjectTeachers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SubjectTeachers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectTeachers",
                table: "SubjectTeachers",
                columns: new[] { "SubjectId", "UserId" });
        }
    }
}
