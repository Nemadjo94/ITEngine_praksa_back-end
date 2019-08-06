using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Praksa2.Repo.Migrations
{
    public partial class InitialPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "SuperAdmins");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Users",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "SuperAdmins",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "SuperAdmins",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.UpdateData(
                table: "SuperAdmins",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 158, 162, 67, 46, 15, 142, 70, 196, 59, 243, 116, 49, 15, 146, 93, 121, 247, 106, 22, 11, 218, 124, 20, 72, 134, 140, 209, 252, 165, 189, 53, 211, 82, 148, 92, 112, 152, 152, 238, 16, 81, 65, 73, 36, 241, 60, 212, 177, 66, 82, 115, 210, 229, 249, 122, 145, 253, 31, 81, 246, 3, 8, 166, 241 }, new byte[] { 8, 4, 83, 40, 159, 89, 74, 67, 70, 12, 206, 88, 153, 78, 190, 246, 16, 149, 11, 223, 102, 35, 131, 183, 105, 227, 12, 123, 135, 115, 0, 139, 12, 137, 71, 52, 245, 184, 224, 169, 46, 89, 194, 163, 73, 186, 117, 84, 119, 161, 191, 245, 212, 41, 150, 175, 255, 122, 144, 116, 88, 164, 251, 240, 66, 252, 100, 103, 77, 130, 110, 52, 13, 210, 156, 7, 125, 93, 116, 183, 42, 131, 241, 140, 54, 93, 30, 250, 71, 77, 73, 95, 151, 60, 139, 231, 73, 242, 46, 214, 235, 20, 135, 86, 164, 144, 21, 251, 50, 2, 168, 8, 88, 24, 237, 245, 228, 60, 2, 255, 136, 218, 194, 228, 13, 13, 183, 221 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "SuperAdmins");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "SuperAdmins");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "SuperAdmins",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "SuperAdmins",
                keyColumn: "ID",
                keyValue: 1,
                column: "Password",
                value: "admin123");
        }
    }
}
