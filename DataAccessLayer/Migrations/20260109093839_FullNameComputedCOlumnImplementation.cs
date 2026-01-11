using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class FullNameComputedCOlumnImplementation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "People",
                type: "nvarchar(450)",
                nullable: false,
                computedColumnSql: "CONCAT_WS(' ', [FirstName], [SecondName], [ThirdName], [FourthName])",
                stored: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_FullName",
                table: "People",
                column: "FullName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_People_FullName",
                table: "People");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "People");
        }
    }
}
