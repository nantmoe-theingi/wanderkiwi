using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WanderKiwi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAttractionCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 7, 2 });

            migrationBuilder.InsertData(
                table: "AttractionCategories",
                columns: new[] { "AttractionId", "CategoryId" },
                values: new object[,]
                {
                    { 2, 3 },
                    { 2, 7 },
                    { 3, 2 },
                    { 4, 8 },
                    { 5, 1 },
                    { 5, 7 },
                    { 6, 4 },
                    { 7, 5 },
                    { 7, 7 },
                    { 8, 1 },
                    { 8, 7 },
                    { 9, 2 },
                    { 10, 1 },
                    { 10, 2 },
                    { 11, 1 },
                    { 11, 2 },
                    { 12, 1 },
                    { 12, 3 },
                    { 13, 1 },
                    { 13, 3 },
                    { 14, 3 },
                    { 14, 7 },
                    { 15, 1 },
                    { 15, 7 },
                    { 16, 1 },
                    { 16, 7 },
                    { 17, 3 },
                    { 17, 4 },
                    { 18, 3 },
                    { 19, 3 },
                    { 19, 4 },
                    { 20, 4 },
                    { 21, 1 },
                    { 21, 8 },
                    { 22, 4 },
                    { 22, 8 },
                    { 23, 3 },
                    { 23, 8 },
                    { 24, 3 },
                    { 24, 6 },
                    { 25, 1 },
                    { 25, 7 },
                    { 26, 3 },
                    { 26, 7 },
                    { 27, 5 },
                    { 27, 6 },
                    { 28, 1 },
                    { 28, 2 },
                    { 29, 4 },
                    { 30, 4 },
                    { 30, 6 },
                    { 31, 3 },
                    { 31, 6 },
                    { 32, 4 },
                    { 33, 8 },
                    { 34, 1 },
                    { 34, 5 },
                    { 35, 1 },
                    { 35, 2 },
                    { 36, 3 },
                    { 36, 8 },
                    { 37, 4 },
                    { 38, 3 },
                    { 38, 4 },
                    { 39, 4 },
                    { 39, 6 },
                    { 40, 1 },
                    { 40, 4 },
                    { 41, 3 },
                    { 41, 4 },
                    { 42, 1 },
                    { 42, 8 },
                    { 43, 6 },
                    { 43, 7 },
                    { 44, 1 },
                    { 44, 6 },
                    { 45, 3 },
                    { 45, 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 2, 7 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 4, 8 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 5, 7 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 6, 4 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 7, 5 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 7, 7 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 8, 1 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 8, 7 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 9, 2 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 10, 1 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 10, 2 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 11, 1 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 11, 2 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 12, 1 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 12, 3 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 13, 1 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 13, 3 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 14, 3 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 14, 7 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 15, 1 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 15, 7 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 16, 1 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 16, 7 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 17, 3 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 17, 4 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 18, 3 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 19, 3 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 19, 4 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 20, 4 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 21, 1 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 21, 8 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 22, 4 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 22, 8 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 23, 3 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 23, 8 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 24, 3 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 24, 6 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 25, 1 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 25, 7 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 26, 3 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 26, 7 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 27, 5 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 27, 6 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 28, 1 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 28, 2 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 29, 4 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 30, 4 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 30, 6 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 31, 3 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 31, 6 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 32, 4 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 33, 8 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 34, 1 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 34, 5 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 35, 1 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 35, 2 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 36, 3 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 36, 8 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 37, 4 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 38, 3 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 38, 4 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 39, 4 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 39, 6 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 40, 1 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 40, 4 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 41, 3 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 41, 4 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 42, 1 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 42, 8 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 43, 6 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 43, 7 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 44, 1 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 44, 6 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 45, 3 });

            migrationBuilder.DeleteData(
                table: "AttractionCategories",
                keyColumns: new[] { "AttractionId", "CategoryId" },
                keyValues: new object[] { 45, 4 });

            migrationBuilder.InsertData(
                table: "AttractionCategories",
                columns: new[] { "AttractionId", "CategoryId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 2, 2 },
                    { 4, 3 },
                    { 5, 3 },
                    { 5, 4 },
                    { 6, 1 },
                    { 7, 1 },
                    { 7, 2 }
                });
        }
    }
}
