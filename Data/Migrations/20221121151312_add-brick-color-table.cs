using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Programming_Project.Data.Migrations
{
    public partial class addbrickcolortable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BrickColor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrickColorName = table.Column<string>(type: "text", nullable: false),
                    BrickColorRed = table.Column<int>(type: "int", nullable: false),
                    BrickColorGreen = table.Column<int>(type: "int", nullable: false),
                    BrickColorBlue = table.Column<int>(type: "int", nullable: false),
                    BrickColorAlpha = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrickColor", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrickColor");
        }
    }
}
