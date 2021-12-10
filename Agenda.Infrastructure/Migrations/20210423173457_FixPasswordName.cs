using Microsoft.EntityFrameworkCore.Migrations;

namespace Agenda.Infrastructure.Migrations
{
    public partial class FixPasswordName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Senha",
                table: "Users",
                newName: "Password");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "Senha");
        }
    }
}
