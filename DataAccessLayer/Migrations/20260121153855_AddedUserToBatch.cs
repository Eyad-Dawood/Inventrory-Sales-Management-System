using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserToBatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TakeBatches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TakeBatches_UserId",
                table: "TakeBatches",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TakeBatches_Users_UserId",
                table: "TakeBatches",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TakeBatches_Users_UserId",
                table: "TakeBatches");

            migrationBuilder.DropIndex(
                name: "IX_TakeBatches_UserId",
                table: "TakeBatches");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TakeBatches");
        }
    }
}
