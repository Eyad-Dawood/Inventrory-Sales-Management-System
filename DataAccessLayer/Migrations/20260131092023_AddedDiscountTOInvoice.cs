using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedDiscountTOInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Additional",
                table: "Invoices",
                newName: "Discount");

            migrationBuilder.RenameColumn(
                name: "AdditionNotes",
                table: "Invoices",
                newName: "Notes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Invoices",
                newName: "AdditionNotes");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "Invoices",
                newName: "Additional");
        }
    }
}
