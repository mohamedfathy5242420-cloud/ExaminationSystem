using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExaminationSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddQuizInstructions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Instructions",
                table: "Quizzes",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "No instructions.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Instructions",
                table: "Quizzes");
        }
    }
}
