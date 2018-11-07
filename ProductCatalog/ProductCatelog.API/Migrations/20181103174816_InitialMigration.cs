using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductCatalog.API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductName = table.Column<string>(maxLength: 50, nullable: false),
                    ProductPrice = table.Column<int>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    Image = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "product",
                columns: new[] { "ProductId", "Image", "LastUpdated", "ProductName", "ProductPrice" },
                values: new object[] { 1, new byte[] { 84, 101, 115, 116 }, new DateTime(2018, 11, 3, 13, 48, 15, 886, DateTimeKind.Local), "Sample Product", 10 });

            migrationBuilder.CreateIndex(
                name: "IX_product_ProductName",
                table: "product",
                column: "ProductName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product");
        }
    }
}
