using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Programming_Project.Data.Migrations
{
    public partial class addthemetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Theme",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThemeName = table.Column<string>(type: "text", nullable: false),
                    ThemeDescription = table.Column<string>(type: "text", nullable: false),
                    ThemeImgName = table.Column<string>(type: "text", nullable: true),
                    ThemeAgeCategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theme", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Theme");
        }
    }
}
