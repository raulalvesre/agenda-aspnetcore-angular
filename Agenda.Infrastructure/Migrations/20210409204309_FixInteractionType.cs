using Microsoft.EntityFrameworkCore.Migrations;

namespace Agenda.Infrastructure.Migrations
{
    public partial class FixInteractionType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactTelephones_Contacts_ContactId",
                table: "ContactTelephones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactTelephones",
                table: "ContactTelephones");

            migrationBuilder.RenameTable(
                name: "ContactTelephones",
                newName: "Contact Telephones");

            migrationBuilder.RenameIndex(
                name: "IX_ContactTelephones_ContactId",
                table: "Contact Telephones",
                newName: "IX_Contact Telephones_ContactId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Interaction Types",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contact Telephones",
                table: "Contact Telephones",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Interaction Types",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Create Contact" });

            migrationBuilder.InsertData(
                table: "Interaction Types",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Update Contact" });

            migrationBuilder.InsertData(
                table: "Interaction Types",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Remove Contact" });

            migrationBuilder.AddForeignKey(
                name: "FK_Contact Telephones_Contacts_ContactId",
                table: "Contact Telephones",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact Telephones_Contacts_ContactId",
                table: "Contact Telephones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contact Telephones",
                table: "Contact Telephones");

            migrationBuilder.DeleteData(
                table: "Interaction Types",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Interaction Types",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Interaction Types",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameTable(
                name: "Contact Telephones",
                newName: "ContactTelephones");

            migrationBuilder.RenameIndex(
                name: "IX_Contact Telephones_ContactId",
                table: "ContactTelephones",
                newName: "IX_ContactTelephones_ContactId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Interaction Types",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactTelephones",
                table: "ContactTelephones",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactTelephones_Contacts_ContactId",
                table: "ContactTelephones",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
