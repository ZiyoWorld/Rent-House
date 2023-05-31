using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Houzing.Migrations
{
    /// <inheritdoc />
    public partial class changecustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApartmentId",
                table: "Customer",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ApartmentId",
                table: "Customer",
                column: "ApartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Apartments_ApartmentId",
                table: "Customer",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Apartments_ApartmentId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_ApartmentId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ApartmentId",
                table: "Customer");
        }
    }
}
