using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyAspire.Database.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Phases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoundId = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Phases",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Phase 1" },
                    { 2, "Phase 2" },
                    { 3, "Phase 3" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Answer", "RoundId", "Text" },
                values: new object[,]
                {
                    { 1, 8, 1, "What is 5 + 3?" },
                    { 2, 8, 1, "What is 12 - 4?" },
                    { 3, 42, 1, "What is 7 * 6?" },
                    { 4, 9, 1, "What is 81 / 9?" },
                    { 5, 8, 1, "What is the square root of 64?" },
                    { 6, 3, 1, "What is 15 % 4?" },
                    { 7, 8, 1, "What is 2^3?" },
                    { 8, 55, 1, "What is 100 - 45?" },
                    { 9, 41, 1, "What is 14 + 27?" },
                    { 10, 81, 1, "What is 9 * 9?" },
                    { 11, 42, 2, "What is 25 + 17?" },
                    { 12, 12, 2, "What is 144 / 12?" },
                    { 13, 54, 2, "What is 18 * 3?" },
                    { 14, 66, 2, "What is 99 - 33?" },
                    { 15, 121, 2, "What is 11^2?" },
                    { 16, 3, 2, "What is the cube root of 27?" },
                    { 17, 56, 2, "What is 7 * 8?" },
                    { 18, 1, 2, "What is 50 % 7?" },
                    { 19, 8, 2, "What is 64 / 8?" },
                    { 20, 100, 2, "What is 123 - 23?" },
                    { 21, 81, 3, "What is 3^4?" },
                    { 22, 16, 3, "What is 256 / 16?" },
                    { 23, 90, 3, "What is 45 * 2?" },
                    { 24, 125, 3, "What is 200 - 75?" },
                    { 25, 11, 3, "What is the square root of 121?" },
                    { 26, 1000, 3, "What is 10^3?" },
                    { 27, 4, 3, "What is 81 % 7?" },
                    { 28, 225, 3, "What is 15 * 15?" },
                    { 29, 20, 3, "What is 500 / 25?" },
                    { 30, 200, 3, "What is 99 + 101?" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Phases");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Answers");
        }
    }
}
