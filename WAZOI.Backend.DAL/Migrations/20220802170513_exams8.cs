using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WAZOI.Backend.DAL.Migrations
{
    public partial class exams8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjectRequests_Subjects_SubjectId",
                table: "StudentSubjectRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjectRequests_Users_UserId",
                table: "StudentSubjectRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSubjectRequests",
                table: "StudentSubjectRequests");

            migrationBuilder.RenameTable(
                name: "StudentSubjectRequests",
                newName: "UserSubjectJoinRequests");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubjectRequests_UserId",
                table: "UserSubjectJoinRequests",
                newName: "IX_UserSubjectJoinRequests_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubjectRequests_SubjectId",
                table: "UserSubjectJoinRequests",
                newName: "IX_UserSubjectJoinRequests_SubjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSubjectJoinRequests",
                table: "UserSubjectJoinRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSubjectJoinRequests_Subjects_SubjectId",
                table: "UserSubjectJoinRequests",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSubjectJoinRequests_Users_UserId",
                table: "UserSubjectJoinRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSubjectJoinRequests_Subjects_SubjectId",
                table: "UserSubjectJoinRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSubjectJoinRequests_Users_UserId",
                table: "UserSubjectJoinRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSubjectJoinRequests",
                table: "UserSubjectJoinRequests");

            migrationBuilder.RenameTable(
                name: "UserSubjectJoinRequests",
                newName: "StudentSubjectRequests");

            migrationBuilder.RenameIndex(
                name: "IX_UserSubjectJoinRequests_UserId",
                table: "StudentSubjectRequests",
                newName: "IX_StudentSubjectRequests_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSubjectJoinRequests_SubjectId",
                table: "StudentSubjectRequests",
                newName: "IX_StudentSubjectRequests_SubjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSubjectRequests",
                table: "StudentSubjectRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjectRequests_Subjects_SubjectId",
                table: "StudentSubjectRequests",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjectRequests_Users_UserId",
                table: "StudentSubjectRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
