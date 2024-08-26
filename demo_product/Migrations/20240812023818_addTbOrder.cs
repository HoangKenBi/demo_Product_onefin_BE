using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace demo_product.Migrations
{
    /// <inheritdoc />
    public partial class addTbOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    idOrder = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameOrder = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    phoneOrder = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    emailOrder = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    addressOrder = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.idOrder);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    idOrderDetail = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idProduct = table.Column<int>(type: "int", nullable: false),
                    idOrder = table.Column<int>(type: "int", nullable: false),
                    totalQuantity = table.Column<int>(type: "int", nullable: false),
                    totalPrice = table.Column<double>(type: "float", nullable: false),
                    statusOrderDetail = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.idOrderDetail);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order_idOrder",
                        column: x => x.idOrder,
                        principalTable: "Order",
                        principalColumn: "idOrder",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_idOrder",
                table: "OrderDetail",
                column: "idOrder");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailProduct_idProduct",
                table: "OrderDetailProduct",
                column: "idProduct");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "Order");
        }
    }
}
