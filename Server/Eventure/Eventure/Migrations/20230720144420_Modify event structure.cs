using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eventure.Migrations
{
    public partial class Modifyeventstructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Events",
                newName: "StartingDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndingDate",
                table: "Events",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndingDate",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "StartingDate",
                table: "Events",
                newName: "Date");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Events",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte>(
                name: "Visibility",
                table: "Events",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
