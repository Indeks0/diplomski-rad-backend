using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WAZOI.Backend.DAL.Migrations
{
    public partial class gradingcriteria1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SubjectGradingCriteria_SubjectId",
                table: "SubjectGradingCriteria");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectGradingCriteria_SubjectId",
                table: "SubjectGradingCriteria",
                column: "SubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SubjectGradingCriteria_SubjectId",
                table: "SubjectGradingCriteria");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectGradingCriteria_SubjectId",
                table: "SubjectGradingCriteria",
                column: "SubjectId",
                unique: true);
        }
    }
}
