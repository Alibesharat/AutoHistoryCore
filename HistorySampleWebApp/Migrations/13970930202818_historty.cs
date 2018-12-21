using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HistorySampleWebApp.Migrations
{
    public partial class historty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CrearedDateTime",
                table: "students",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDatTime",
                table: "students",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "students",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastAccesDevice",
                table: "students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastAccessIp",
                table: "students",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEditedDateTime",
                table: "students",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CrearedDateTime",
                table: "students");

            migrationBuilder.DropColumn(
                name: "DeletedDatTime",
                table: "students");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "students");

            migrationBuilder.DropColumn(
                name: "LastAccesDevice",
                table: "students");

            migrationBuilder.DropColumn(
                name: "LastAccessIp",
                table: "students");

            migrationBuilder.DropColumn(
                name: "LastEditedDateTime",
                table: "students");
        }
    }
}
