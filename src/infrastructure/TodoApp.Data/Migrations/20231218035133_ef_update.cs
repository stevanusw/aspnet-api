using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ef_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2b07d854-aaae-41aa-9b19-e9c63feafd85",
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40c8001d-6cc3-4219-8415-b1919e4035d3",
                column: "ConcurrencyStamp",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2b07d854-aaae-41aa-9b19-e9c63feafd85",
                column: "ConcurrencyStamp",
                value: "1c594fbb-c957-4858-b1eb-6e3b5a472901");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40c8001d-6cc3-4219-8415-b1919e4035d3",
                column: "ConcurrencyStamp",
                value: "a6deecca-9d6b-436c-b5a8-215238315fdb");
        }
    }
}
