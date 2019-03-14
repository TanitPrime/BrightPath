using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BrightPath.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsRoot = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    AdminPw = table.Column<string>(type: "nvarchar(150)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    ArticleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArticleTitle = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    desc_mini = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    desc = table.Column<string>(type: "nvarchar(750)", nullable: true),
                    ArticleAdress = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Articlecoor = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    ArticleContact = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvachar(max)", nullable: false),
                    ImageName = table.Column<string>(type:"nvarchar(100)",nullable:true)

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ArticleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    UserLastName = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    UserEmail = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    UserPw = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
