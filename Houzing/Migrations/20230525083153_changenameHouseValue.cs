using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Houzing.Migrations
{
    /// <inheritdoc />
    public partial class changenameHouseValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "HouseItems",
                newName: "ImagePath3");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath1",
                table: "HouseItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath2",
                table: "HouseItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath1",
                table: "HouseItems");

            migrationBuilder.DropColumn(
                name: "ImagePath2",
                table: "HouseItems");

            migrationBuilder.RenameColumn(
                name: "ImagePath3",
                table: "HouseItems",
                newName: "ImagePath");
        }
    }
}
