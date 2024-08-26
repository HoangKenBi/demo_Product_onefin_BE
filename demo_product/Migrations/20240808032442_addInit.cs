using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace demo_product.Migrations
{
    /// <inheritdoc />
    public partial class addInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    idProduct = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameProduct = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    priceProduct = table.Column<double>(type: "float", nullable: false),
                    quantityProduct = table.Column<int>(type: "int", nullable: false),
                    imgProduct = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    statusProduct = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.idProduct);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
