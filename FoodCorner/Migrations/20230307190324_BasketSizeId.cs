using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodCorner.Migrations
{
    /// <inheritdoc />
    public partial class BasketSizeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "basket-products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_basket-products_SizeId",
                table: "basket-products",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_basket-products_Sizes_SizeId",
                table: "basket-products",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_basket-products_Sizes_SizeId",
                table: "basket-products");

            migrationBuilder.DropIndex(
                name: "IX_basket-products_SizeId",
                table: "basket-products");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "basket-products");
        }
    }
}
