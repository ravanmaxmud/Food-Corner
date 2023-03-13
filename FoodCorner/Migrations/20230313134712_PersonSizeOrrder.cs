using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodCorner.Migrations
{
    /// <inheritdoc />
    public partial class PersonSizeOrrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "OrderProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_SizeId",
                table: "OrderProducts",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Sizes_SizeId",
                table: "OrderProducts",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Sizes_SizeId",
                table: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_SizeId",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "OrderProducts");
        }
    }
}
