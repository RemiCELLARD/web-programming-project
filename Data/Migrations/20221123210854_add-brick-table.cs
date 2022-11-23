using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Programming_Project.Data.Migrations
{
    public partial class addbricktable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brick",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrickName = table.Column<string>(type: "text", nullable: false),
                    BrickColorId = table.Column<int>(type: "int", nullable: false),
                    BrickSize = table.Column<int>(type: "int", nullable: false),
                    BrickPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brick", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brick_BrickColor_BrickColorId",
                        column: x => x.BrickColorId,
                        principalTable: "BrickColor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brick_BrickColorId",
                table: "Brick",
                column: "BrickColorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brick");
        }
    }
}
