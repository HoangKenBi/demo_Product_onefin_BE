using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace demo_product.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_idProduct",
                table: "OrderDetail",
                column: "idProduct");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Product_idProduct",
                table: "OrderDetail",
                column: "idProduct",
                principalTable: "Product",
                principalColumn: "idProduct",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Product_idProduct",
                table: "OrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetail_idProduct",
                table: "OrderDetail");

          
        }
    }
}
