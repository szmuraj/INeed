using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INeed.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLanguage3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DecryptionLongEN",
                table: "Forms",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DecryptionShortEN",
                table: "Forms",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DecryptionLongEN",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "DecryptionShortEN",
                table: "Forms");
        }
    }
}
