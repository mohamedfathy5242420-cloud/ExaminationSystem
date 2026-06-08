using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExaminationSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefineDomainRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Diplomas_InstructorId",
                table: "Diplomas",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_AttemptAnswers_QuestionId",
                table: "AttemptAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AttemptAnswers_SelectedOptionId",
                table: "AttemptAnswers",
                column: "SelectedOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttemptAnswers_QuestionOptions_SelectedOptionId",
                table: "AttemptAnswers",
                column: "SelectedOptionId",
                principalTable: "QuestionOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AttemptAnswers_Questions_QuestionId",
                table: "AttemptAnswers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Diplomas_AspNetUsers_InstructorId",
                table: "Diplomas",
                column: "InstructorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizAttempts_AspNetUsers_StudentId",
                table: "QuizAttempts",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttemptAnswers_QuestionOptions_SelectedOptionId",
                table: "AttemptAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_AttemptAnswers_Questions_QuestionId",
                table: "AttemptAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_Diplomas_AspNetUsers_InstructorId",
                table: "Diplomas");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizAttempts_AspNetUsers_StudentId",
                table: "QuizAttempts");

            migrationBuilder.DropIndex(
                name: "IX_Diplomas_InstructorId",
                table: "Diplomas");

            migrationBuilder.DropIndex(
                name: "IX_AttemptAnswers_QuestionId",
                table: "AttemptAnswers");

            migrationBuilder.DropIndex(
                name: "IX_AttemptAnswers_SelectedOptionId",
                table: "AttemptAnswers");
        }
    }
}
