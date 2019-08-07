using Microsoft.EntityFrameworkCore.Migrations;

namespace Praksa2.Repo.Migrations
{
    public partial class PasswordSaltAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordSalt",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "AspNetUsers");
        }
    }
}
