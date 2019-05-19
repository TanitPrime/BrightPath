using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BrightPathDev.Migrations
{
    public partial class changeddateofdeleterequesttypetostring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DateOfRequest",
                table: "DeleteLists",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfRequest",
                table: "DeleteLists",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
