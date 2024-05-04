using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace QuizApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeleteUserResponse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserResponses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AnswerId = table.Column<int>(type: "integer", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    QuizResultId = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserResponses_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserResponses_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserResponses_QuizResults_QuizResultId",
                        column: x => x.QuizResultId,
                        principalTable: "QuizResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserResponses_AnswerId",
                table: "UserResponses",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserResponses_QuestionId",
                table: "UserResponses",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserResponses_QuizResultId",
                table: "UserResponses",
                column: "QuizResultId");
        }
    }
}
