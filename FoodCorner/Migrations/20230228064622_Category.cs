using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodCorner.Migrations
{
    /// <inheritdoc />
    public partial class Category : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoryies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoryies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categoryies_Categoryies_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categoryies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductCategoryies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CatagoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategoryies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCategoryies_Categoryies_CatagoryId",
                        column: x => x.CatagoryId,
                        principalTable: "Categoryies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategoryies_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categoryies_ParentId",
                table: "Categoryies",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategoryies_CatagoryId",
                table: "ProductCategoryies",
                column: "CatagoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategoryies_ProductId",
                table: "ProductCategoryies",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCategoryies");

            migrationBuilder.DropTable(
                name: "Categoryies");
        }
    }
}
