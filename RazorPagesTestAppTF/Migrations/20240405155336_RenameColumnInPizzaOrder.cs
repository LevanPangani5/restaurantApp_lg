using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPagesTestAppTF.Migrations
{
    public partial class RenameColumnInPizzaOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BasePrice",
                table: "PizzaOrders",
                newName: "Price");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "PizzaOrders",
                newName: "BasePrice");
        }
    }
}
