using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExaminationSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOtpPurpose : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Purpose",
                table: "OTPs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "AccountVerification");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Purpose",
                table: "OTPs");
        }
    }
}
