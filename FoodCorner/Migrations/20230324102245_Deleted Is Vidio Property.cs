using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodCorner.Migrations
{
    /// <inheritdoc />
    public partial class DeletedIsVidioProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsImage",
                table: "BlogFiles");

            migrationBuilder.DropColumn(
                name: "IsVidio",
                table: "BlogFiles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsImage",
                table: "BlogFiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVidio",
                table: "BlogFiles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
