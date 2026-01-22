using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class MakeBoughtProductToSoldProductSoiTsMoreUnderstandable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoughtProducts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TakeDate",
                table: "TakeBatches",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "SYSDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "SoldProducts",
                columns: table => new
                {
                    SoldProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyingPricePerUnit = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SellingPricePerUnit = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    TakeBatchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoldProducts", x => x.SoldProductId);
                    table.ForeignKey(
                        name: "FK_SoldProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SoldProducts_TakeBatches_TakeBatchId",
                        column: x => x.TakeBatchId,
                        principalTable: "TakeBatches",
                        principalColumn: "TakeBatchId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SoldProducts_ProductId",
                table: "SoldProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SoldProducts_TakeBatchId",
                table: "SoldProducts",
                column: "TakeBatchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SoldProducts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TakeDate",
                table: "TakeBatches",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "SYSDATETIME()");

            migrationBuilder.CreateTable(
                name: "BoughtProducts",
                columns: table => new
                {
                    BoughtProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    TakeBatchId = table.Column<int>(type: "int", nullable: false),
                    BuyingPricePerUnit = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    SellingPricePerUnit = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoughtProducts", x => x.BoughtProductId);
                    table.ForeignKey(
                        name: "FK_BoughtProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoughtProducts_TakeBatches_TakeBatchId",
                        column: x => x.TakeBatchId,
                        principalTable: "TakeBatches",
                        principalColumn: "TakeBatchId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoughtProducts_ProductId",
                table: "BoughtProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BoughtProducts_TakeBatchId",
                table: "BoughtProducts",
                column: "TakeBatchId");
        }
    }
}
