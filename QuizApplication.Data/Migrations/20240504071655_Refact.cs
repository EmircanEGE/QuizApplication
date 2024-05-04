using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class Refact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectAnswer",
                table: "Questions");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_CreatedBy",
                table: "Quizzes",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Users_CreatedBy",
                table: "Quizzes",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Users_CreatedBy",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_CreatedBy",
                table: "Quizzes");

            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswer",
                table: "Questions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
