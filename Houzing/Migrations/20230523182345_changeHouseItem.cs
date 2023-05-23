using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Houzing.Migrations
{
    /// <inheritdoc />
    public partial class changeHouseItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HouseImgs");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "HouseItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "HouseItems");

            migrationBuilder.CreateTable(
                name: "HouseImgs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HouseItemId = table.Column<int>(type: "int", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseImgs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HouseImgs_HouseItems_HouseItemId",
                        column: x => x.HouseItemId,
                        principalTable: "HouseItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HouseImgs_HouseItemId",
                table: "HouseImgs",
                column: "HouseItemId");
        }
    }
}
