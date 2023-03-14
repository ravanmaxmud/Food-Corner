using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodCorner.Migrations
{
    /// <inheritdoc />
    public partial class PasswordForget : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PasswordForgets_Users_UserId",
                table: "PasswordForgets");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "PasswordForgets");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "PasswordForgets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PasswordForgets_Users_UserId",
                table: "PasswordForgets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PasswordForgets_Users_UserId",
                table: "PasswordForgets");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "PasswordForgets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "PasswordForgets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_PasswordForgets_Users_UserId",
                table: "PasswordForgets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
