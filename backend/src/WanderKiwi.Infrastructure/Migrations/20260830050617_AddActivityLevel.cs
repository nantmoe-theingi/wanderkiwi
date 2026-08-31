using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WanderKiwi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddActivityLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActivityLevel",
                table: "Attractions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 1,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 2,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 3,
                column: "ActivityLevel",
                value: "Moderate");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 4,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 5,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 6,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 7,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 8,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 9,
                column: "ActivityLevel",
                value: "Challenging");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 10,
                column: "ActivityLevel",
                value: "Challenging");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 11,
                column: "ActivityLevel",
                value: "Moderate");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 12,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 13,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 14,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 15,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 16,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 17,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 18,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 19,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 20,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 21,
                column: "ActivityLevel",
                value: "Moderate");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 22,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 23,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 24,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 25,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 26,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 27,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 28,
                column: "ActivityLevel",
                value: "Challenging");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 29,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 30,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 31,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 32,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 33,
                column: "ActivityLevel",
                value: "Moderate");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 34,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 35,
                column: "ActivityLevel",
                value: "Moderate");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 36,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 37,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 38,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 39,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 40,
                column: "ActivityLevel",
                value: "Moderate");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 41,
                column: "ActivityLevel",
                value: "Moderate");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 42,
                column: "ActivityLevel",
                value: "Moderate");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 43,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 44,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 45,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 46,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 47,
                column: "ActivityLevel",
                value: "Challenging");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 48,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 49,
                column: "ActivityLevel",
                value: "Easy");

            migrationBuilder.UpdateData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 50,
                column: "ActivityLevel",
                value: "Easy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivityLevel",
                table: "Attractions");
        }
    }
}
