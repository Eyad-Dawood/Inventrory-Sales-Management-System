using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedProductsAndLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_People_PersonId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_People_Towns_TownID",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_People_PersonId",
                table: "Workers");

            migrationBuilder.CreateTable(
                name: "MasurementUnits",
                columns: table => new
                {
                    MasurementUnitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasurementUnits", x => x.MasurementUnitId);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    ProductTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductTypeName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.ProductTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyingPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SellingPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    QuantityInStorage = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    MasurementUnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_MasurementUnits_MasurementUnitId",
                        column: x => x.MasurementUnitId,
                        principalTable: "MasurementUnits",
                        principalColumn: "MasurementUnitId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "ProductTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductPricesLog",
                columns: table => new
                {
                    ProductPriceLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OldBuyingPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    NewBuyingPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    OldSellingPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    NewSellingPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    LogDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPricesLog", x => x.ProductPriceLogId);
                    table.ForeignKey(
                        name: "FK_ProductPricesLog_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductPricesLog_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "productStockMovmentsLog",
                columns: table => new
                {
                    ProductStockMovementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OldQuantity = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    NewQuantity = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    Reason = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    LogDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productStockMovmentsLog", x => x.ProductStockMovementId);
                    table.ForeignKey(
                        name: "FK_productStockMovmentsLog_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_productStockMovmentsLog_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPricesLog_CreatedByUserId",
                table: "ProductPricesLog",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPricesLog_ProductId",
                table: "ProductPricesLog",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_MasurementUnitId",
                table: "Products",
                column: "MasurementUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_productStockMovmentsLog_CreatedByUserId",
                table: "productStockMovmentsLog",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_productStockMovmentsLog_ProductId",
                table: "productStockMovmentsLog",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_People_PersonId",
                table: "Customers",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_People_Towns_TownID",
                table: "People",
                column: "TownID",
                principalTable: "Towns",
                principalColumn: "TownID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_People_PersonId",
                table: "Workers",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_People_PersonId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_People_Towns_TownID",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_People_PersonId",
                table: "Workers");

            migrationBuilder.DropTable(
                name: "ProductPricesLog");

            migrationBuilder.DropTable(
                name: "productStockMovmentsLog");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "MasurementUnits");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_People_PersonId",
                table: "Customers",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_People_Towns_TownID",
                table: "People",
                column: "TownID",
                principalTable: "Towns",
                principalColumn: "TownID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_People_PersonId",
                table: "Workers",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
