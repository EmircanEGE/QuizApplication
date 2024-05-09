using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatedByForAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Answers",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Answers");
        }
    }
}
