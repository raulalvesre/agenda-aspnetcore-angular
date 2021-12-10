using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Agenda.Infrastructure.Migrations
{
    public partial class Desafio4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interactions_Interaction Types_InteractionTypeId",
                table: "Interactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Interaction Types",
                table: "Interaction Types");

            migrationBuilder.RenameTable(
                name: "Interaction Types",
                newName: "InteractionType");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Interactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TelephoneTypeId",
                table: "Contact Telephones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InteractionType",
                table: "InteractionType",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TelephoneType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelephoneType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "UserRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "InteractionType",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Contact Selected" });

            migrationBuilder.InsertData(
                table: "TelephoneType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Landline" },
                    { 2, "Commercial" },
                    { 3, "Cellphone" }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "ADMIN" },
                    { 2, "STANDARD USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_UserId",
                table: "Interactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_OwnerId",
                table: "Contacts",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact Telephones_TelephoneTypeId",
                table: "Contact Telephones",
                column: "TelephoneTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact Telephones_TelephoneType_TelephoneTypeId",
                table: "Contact Telephones",
                column: "TelephoneTypeId",
                principalTable: "TelephoneType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Users_OwnerId",
                table: "Contacts",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interactions_InteractionType_InteractionTypeId",
                table: "Interactions",
                column: "InteractionTypeId",
                principalTable: "InteractionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interactions_Users_UserId",
                table: "Interactions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact Telephones_TelephoneType_TelephoneTypeId",
                table: "Contact Telephones");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_OwnerId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Interactions_InteractionType_InteractionTypeId",
                table: "Interactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Interactions_Users_UserId",
                table: "Interactions");

            migrationBuilder.DropTable(
                name: "TelephoneType");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_Interactions_UserId",
                table: "Interactions");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_OwnerId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contact Telephones_TelephoneTypeId",
                table: "Contact Telephones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InteractionType",
                table: "InteractionType");

            migrationBuilder.DeleteData(
                table: "InteractionType",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Interactions");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "TelephoneTypeId",
                table: "Contact Telephones");

            migrationBuilder.RenameTable(
                name: "InteractionType",
                newName: "Interaction Types");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Interaction Types",
                table: "Interaction Types",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Interactions_Interaction Types_InteractionTypeId",
                table: "Interactions",
                column: "InteractionTypeId",
                principalTable: "Interaction Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
