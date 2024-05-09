using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedByForAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Answers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Answers");
        }
    }
}
