using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPagesTestAppTF.Migrations
{
    public partial class AddIngredientBooleanPropertiesToPizzaOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Beef",
                table: "PizzaOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Cheese",
                table: "PizzaOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Ham",
                table: "PizzaOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Mushroom",
                table: "PizzaOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Pepperoni",
                table: "PizzaOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TomatoSauce",
                table: "PizzaOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Tuna",
                table: "PizzaOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Beef",
                table: "PizzaOrders");

            migrationBuilder.DropColumn(
                name: "Cheese",
                table: "PizzaOrders");

            migrationBuilder.DropColumn(
                name: "Ham",
                table: "PizzaOrders");

            migrationBuilder.DropColumn(
                name: "Mushroom",
                table: "PizzaOrders");

            migrationBuilder.DropColumn(
                name: "Pepperoni",
                table: "PizzaOrders");

            migrationBuilder.DropColumn(
                name: "TomatoSauce",
                table: "PizzaOrders");

            migrationBuilder.DropColumn(
                name: "Tuna",
                table: "PizzaOrders");
        }
    }
}
