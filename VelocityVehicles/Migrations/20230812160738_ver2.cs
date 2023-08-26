using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VelocityVehicles.Migrations
{
    /// <inheritdoc />
    public partial class ver2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Automobiles_Brands_BrandId",
                table: "Automobiles");

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "Automobiles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Automobiles_Brands_BrandId",
                table: "Automobiles",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Automobiles_Brands_BrandId",
                table: "Automobiles");

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "Automobiles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Automobiles_Brands_BrandId",
                table: "Automobiles",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id");
        }
    }
}
