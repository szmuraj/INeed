using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace INeed.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Categories_CategoryId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_CategoryId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "Id");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Code", "Name", "StenNormsFemale", "StenNormsMale" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "ACHIEVEMENT", "Potrzeba Osiągnięć", "14,15,17,18,19,21,22,23,24,25", "12,15,17,18,19,21,22,23,24,25" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "AFFILIATION", "Potrzeba Afiliacji", "6,8,10,11,12,13,14,16,17,20", "6,8,9,10,11,13,14,15,18,20" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "AUTONOMY", "Potrzeba Autonomii", "14,15,17,18,19,21,22,23,24,25", "13,16,17,18,20,21,22,23,24,25" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "DOMINANCE", "Potrzeba Dominacji", "7,9,11,13,14,16,18,21,22,25", "8,11,12,14,16,17,19,21,23,25" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "CategoryId");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Questions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CategoryId",
                table: "Questions",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Categories_CategoryId",
                table: "Questions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId");
        }
    }
}
