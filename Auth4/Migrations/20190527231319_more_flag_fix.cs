using Microsoft.EntityFrameworkCore.Migrations;

namespace BrightPathDev.Migrations
{
    public partial class more_flag_fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommentText",
                table: "Flags",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentText",
                table: "Flags");
        }
    }
}
