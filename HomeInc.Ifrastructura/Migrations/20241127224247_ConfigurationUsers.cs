using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeInc.Ifrastructura.Migrations
{
    public partial class ConfigurationUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "users",
                newName: "UserName");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfCreate",
                table: "products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "users");

            migrationBuilder.DropColumn(
                name: "DateOfCreate",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "users",
                newName: "Name");
        }
    }
}
