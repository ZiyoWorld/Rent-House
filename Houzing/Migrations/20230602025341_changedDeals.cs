using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Houzing.Migrations
{
    /// <inheritdoc />
    public partial class changedDeals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Deal");

            migrationBuilder.RenameColumn(
                name: "Summ",
                table: "Deal",
                newName: "PayType");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ToDeal",
                table: "Deal",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FromDeal",
                table: "Deal",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateDeal",
                table: "Deal",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Summa",
                table: "Deal",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Summa",
                table: "Deal");

            migrationBuilder.RenameColumn(
                name: "PayType",
                table: "Deal",
                newName: "Summ");

            migrationBuilder.AlterColumn<string>(
                name: "ToDeal",
                table: "Deal",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FromDeal",
                table: "Deal",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DateDeal",
                table: "Deal",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Deal",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
