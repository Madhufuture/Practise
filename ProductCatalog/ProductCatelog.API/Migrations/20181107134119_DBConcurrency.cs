using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductCatalog.API.Migrations
{
    public partial class DBConcurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "product",
                rowVersion: true,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "Image", "LastUpdated" },
                values: new object[] { new byte[] { 84, 101, 115, 116 }, new DateTime(2018, 11, 7, 8, 41, 18, 963, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "product");

            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "Image", "LastUpdated" },
                values: new object[] { new byte[] { 84, 101, 115, 116 }, new DateTime(2018, 11, 3, 13, 48, 15, 886, DateTimeKind.Local) });
        }
    }
}
