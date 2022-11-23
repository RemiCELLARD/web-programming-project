using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Programming_Project.Data.Migrations
{
    public partial class addboxtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Box",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoxName = table.Column<string>(type: "text", nullable: false),
                    BoxThemeId = table.Column<int>(type: "int", nullable: false),
                    BoxAgeCategory = table.Column<int>(type: "int", nullable: false),
                    BoxImgName = table.Column<string>(type: "text", nullable: true),
                    BoxDescription = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Box", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Box_Theme_BoxThemeId",
                        column: x => x.BoxThemeId,
                        principalTable: "Theme",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BrickInBox",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrickInBoxBoxId = table.Column<int>(type: "int", nullable: false),
                    BrickInBoxBrickId = table.Column<int>(type: "int", nullable: false),
                    BrickInBoxQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrickInBox", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrickInBox_Box_BrickInBoxBoxId",
                        column: x => x.BrickInBoxBoxId,
                        principalTable: "Box",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrickInBox_Brick_BrickInBoxBrickId",
                        column: x => x.BrickInBoxBrickId,
                        principalTable: "Brick",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Box_BoxThemeId",
                table: "Box",
                column: "BoxThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_BrickInBox_BrickInBoxBoxId",
                table: "BrickInBox",
                column: "BrickInBoxBoxId");

            migrationBuilder.CreateIndex(
                name: "IX_BrickInBox_BrickInBoxBrickId",
                table: "BrickInBox",
                column: "BrickInBoxBrickId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrickInBox");

            migrationBuilder.DropTable(
                name: "Box");
        }
    }
}
