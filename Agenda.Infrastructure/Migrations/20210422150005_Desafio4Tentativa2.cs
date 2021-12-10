using Microsoft.EntityFrameworkCore.Migrations;

namespace Agenda.Infrastructure.Migrations
{
    public partial class Desafio4Tentativa2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact Telephones_TelephoneType_TelephoneTypeId",
                table: "Contact Telephones");

            migrationBuilder.DropForeignKey(
                name: "FK_Interactions_InteractionType_InteractionTypeId",
                table: "Interactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserRole_RoleId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TelephoneType",
                table: "TelephoneType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InteractionType",
                table: "InteractionType");

            migrationBuilder.RenameTable(
                name: "UserRole",
                newName: "User Roles");

            migrationBuilder.RenameTable(
                name: "TelephoneType",
                newName: "Telephone Types");

            migrationBuilder.RenameTable(
                name: "InteractionType",
                newName: "Interaction Types");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User Roles",
                table: "User Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Telephone Types",
                table: "Telephone Types",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Interaction Types",
                table: "Interaction Types",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact Telephones_Telephone Types_TelephoneTypeId",
                table: "Contact Telephones",
                column: "TelephoneTypeId",
                principalTable: "Telephone Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interactions_Interaction Types_InteractionTypeId",
                table: "Interactions",
                column: "InteractionTypeId",
                principalTable: "Interaction Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_User Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "User Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact Telephones_Telephone Types_TelephoneTypeId",
                table: "Contact Telephones");

            migrationBuilder.DropForeignKey(
                name: "FK_Interactions_Interaction Types_InteractionTypeId",
                table: "Interactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_User Roles_RoleId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User Roles",
                table: "User Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Telephone Types",
                table: "Telephone Types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Interaction Types",
                table: "Interaction Types");

            migrationBuilder.RenameTable(
                name: "User Roles",
                newName: "UserRole");

            migrationBuilder.RenameTable(
                name: "Telephone Types",
                newName: "TelephoneType");

            migrationBuilder.RenameTable(
                name: "Interaction Types",
                newName: "InteractionType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TelephoneType",
                table: "TelephoneType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InteractionType",
                table: "InteractionType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact Telephones_TelephoneType_TelephoneTypeId",
                table: "Contact Telephones",
                column: "TelephoneTypeId",
                principalTable: "TelephoneType",
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
                name: "FK_Users_UserRole_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "UserRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
