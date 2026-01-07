using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedUniqeIndexesToSmallTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_ProductTypeName",
                table: "ProductTypes",
                column: "ProductTypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasurementUnits_UnitName",
                table: "MasurementUnits",
                column: "UnitName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductTypes_ProductTypeName",
                table: "ProductTypes");

            migrationBuilder.DropIndex(
                name: "IX_MasurementUnits_UnitName",
                table: "MasurementUnits");
        }
    }
}
