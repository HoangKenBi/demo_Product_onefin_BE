using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace demo_product.Migrations
{
    /// <inheritdoc />
    public partial class addTbAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    idAccount = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fullNameAccount = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    emailAccount = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    userNameAccount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    passwordAccount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.idAccount);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
