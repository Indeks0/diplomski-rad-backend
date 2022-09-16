using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WAZOI.Backend.DAL.Migrations
{
    public partial class gradingcriteria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Grade",
                table: "SolvedExams",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectGradingCriteriaId",
                table: "Exam",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "SubjectGradingCriteria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    GradeA = table.Column<float>(type: "real", nullable: false),
                    GradeB = table.Column<float>(type: "real", nullable: false),
                    GradeC = table.Column<float>(type: "real", nullable: false),
                    GradeD = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectGradingCriteria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectGradingCriteria_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exam_SubjectGradingCriteriaId",
                table: "Exam",
                column: "SubjectGradingCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectGradingCriteria_SubjectId",
                table: "SubjectGradingCriteria",
                column: "SubjectId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Exam_SubjectGradingCriteria_SubjectGradingCriteriaId",
                table: "Exam",
                column: "SubjectGradingCriteriaId",
                principalTable: "SubjectGradingCriteria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exam_SubjectGradingCriteria_SubjectGradingCriteriaId",
                table: "Exam");

            migrationBuilder.DropTable(
                name: "SubjectGradingCriteria");

            migrationBuilder.DropIndex(
                name: "IX_Exam_SubjectGradingCriteriaId",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "SolvedExams");

            migrationBuilder.DropColumn(
                name: "SubjectGradingCriteriaId",
                table: "Exam");
        }
    }
}
