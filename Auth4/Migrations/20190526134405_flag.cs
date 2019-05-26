using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BrightPathDev.Migrations
{
    public partial class flag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlagLists");

            migrationBuilder.CreateTable(
                name: "Flags",
                columns: table => new
                {
                    FlagId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArticleId = table.Column<int>(nullable: false),
                    CommentId = table.Column<int>(nullable: false),
                    FlaggerName = table.Column<int>(nullable: false),
                    FlaggerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flags", x => x.FlagId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flags");

            migrationBuilder.CreateTable(
                name: "FlagLists",
                columns: table => new
                {
                    FListId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArticleId = table.Column<int>(nullable: false),
                    CommentId = table.Column<int>(nullable: false),
                    FlaggerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlagLists", x => x.FListId);
                });
        }
    }
}
