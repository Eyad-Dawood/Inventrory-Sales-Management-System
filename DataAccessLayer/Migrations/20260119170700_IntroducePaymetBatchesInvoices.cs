using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class IntroducePaymetBatchesInvoices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productStockMovmentsLog_Products_ProductId",
                table: "productStockMovmentsLog");

            migrationBuilder.DropForeignKey(
                name: "FK_productStockMovmentsLog_Users_CreatedByUserId",
                table: "productStockMovmentsLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productStockMovmentsLog",
                table: "productStockMovmentsLog");

            migrationBuilder.RenameTable(
                name: "productStockMovmentsLog",
                newName: "ProductStockMovmentsLog");

            migrationBuilder.RenameIndex(
                name: "IX_productStockMovmentsLog_ProductId",
                table: "ProductStockMovmentsLog",
                newName: "IX_ProductStockMovmentsLog_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_productStockMovmentsLog_CreatedByUserId",
                table: "ProductStockMovmentsLog",
                newName: "IX_ProductStockMovmentsLog_CreatedByUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductStockMovmentsLog",
                table: "ProductStockMovmentsLog",
                column: "ProductStockMovementId");

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpenDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CloseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalSellingPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TotalBuyingPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TotalPaid = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    WorkerId = table.Column<int>(type: "int", nullable: true),
                    OpenUserId = table.Column<int>(type: "int", nullable: false),
                    CloseUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Users_CloseUserId",
                        column: x => x.CloseUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Users_OpenUserId",
                        column: x => x.OpenUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "WorkerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    PaymentReason = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TakeBatches",
                columns: table => new
                {
                    TakeBatchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TakeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TakeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TakeBatches", x => x.TakeBatchId);
                    table.ForeignKey(
                        name: "FK_TakeBatches_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BoughtProducts",
                columns: table => new
                {
                    BoughtProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyingPricePerUnit = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SellingPricePerUnit = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    TakeBatchId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CloseUserId",
                table: "Invoices",
                column: "CloseUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerId",
                table: "Invoices",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_OpenUserId",
                table: "Invoices",
                column: "OpenUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_WorkerId",
                table: "Invoices",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CustomerId",
                table: "Payments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_InvoiceId",
                table: "Payments",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentReason",
                table: "Payments",
                column: "PaymentReason");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserId",
                table: "Payments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TakeBatches_InvoiceId",
                table: "TakeBatches",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStockMovmentsLog_Products_ProductId",
                table: "ProductStockMovmentsLog",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStockMovmentsLog_Users_CreatedByUserId",
                table: "ProductStockMovmentsLog",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductStockMovmentsLog_Products_ProductId",
                table: "ProductStockMovmentsLog");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductStockMovmentsLog_Users_CreatedByUserId",
                table: "ProductStockMovmentsLog");

            migrationBuilder.DropTable(
                name: "BoughtProducts");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "TakeBatches");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductStockMovmentsLog",
                table: "ProductStockMovmentsLog");

            migrationBuilder.RenameTable(
                name: "ProductStockMovmentsLog",
                newName: "productStockMovmentsLog");

            migrationBuilder.RenameIndex(
                name: "IX_ProductStockMovmentsLog_ProductId",
                table: "productStockMovmentsLog",
                newName: "IX_productStockMovmentsLog_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductStockMovmentsLog_CreatedByUserId",
                table: "productStockMovmentsLog",
                newName: "IX_productStockMovmentsLog_CreatedByUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_productStockMovmentsLog",
                table: "productStockMovmentsLog",
                column: "ProductStockMovementId");

            migrationBuilder.AddForeignKey(
                name: "FK_productStockMovmentsLog_Products_ProductId",
                table: "productStockMovmentsLog",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_productStockMovmentsLog_Users_CreatedByUserId",
                table: "productStockMovmentsLog",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
