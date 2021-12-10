using Microsoft.EntityFrameworkCore.Migrations;

namespace Agenda.Infrastructure.Migrations
{
    public partial class SeedsForTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Interaction Types",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.InsertData(
                table: "Interaction Types",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 5, "Create User" },
                    { 6, "Update User" },
                    { 7, "Remove User" },
                    { 9, "User Login" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Interaction Types",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Interaction Types",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Interaction Types",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Interaction Types",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.InsertData(
                table: "Interaction Types",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Contact Selected" });
        }
    }
}
