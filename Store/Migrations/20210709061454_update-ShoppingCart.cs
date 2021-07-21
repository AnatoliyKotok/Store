using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Migrations
{
    public partial class updateShoppingCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_Products_CategoryId",
                table: "ShoppingCart");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCart_CategoryId",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ShoppingCart");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_ProductId",
                table: "ShoppingCart",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_Products_ProductId",
                table: "ShoppingCart",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_Products_ProductId",
                table: "ShoppingCart");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCart_ProductId",
                table: "ShoppingCart");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ShoppingCart",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_CategoryId",
                table: "ShoppingCart",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_Products_CategoryId",
                table: "ShoppingCart",
                column: "CategoryId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
