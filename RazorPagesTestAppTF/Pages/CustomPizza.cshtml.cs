using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesTestAppTF.Data;
using RazorPagesTestAppTF.Data.DbModels;

namespace RazorPagesTestAppTF.Pages
{
    public class CustomPizzaModel : PageModel
    {
        [BindProperty]
        public PizzasModel Pizza { get; set; }
        public float PizzaPrice { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            PizzaPrice = Pizza.BasePrice;

            if (Pizza.TomatoSauce) PizzaPrice += 1;
            if (Pizza.Cheese) PizzaPrice += 1;
            if (Pizza.Pepperoni) PizzaPrice += 1;
            if (Pizza.Mushroom) PizzaPrice += 1;
            if (Pizza.Tuna) PizzaPrice += 1;
            if (Pizza.Pineapple) PizzaPrice += 10;
            if (Pizza.Ham) PizzaPrice += 1;
            if (Pizza.Beef) PizzaPrice += 1;

            var PizzaOrder = new PizzaOrder
            {
                PizzaName = Pizza.PizzaName,
                Price = PizzaPrice,
                TomatoSauce = Pizza.TomatoSauce,
                Cheese = Pizza.Cheese,
                Pepperoni = Pizza.Pepperoni,
                Mushroom = Pizza.Mushroom,
                Tuna = Pizza.Tuna,
                Ham = Pizza.Ham,
                Beef = Pizza.Beef
            };

            return RedirectToPage("Checkout", new
            {
                PizzaOrder.PizzaName,
                PizzaOrder.Price,
                PizzaOrder.TomatoSauce,
                PizzaOrder.Cheese,
                PizzaOrder.Pepperoni,
                PizzaOrder.Mushroom,
                PizzaOrder.Tuna,
                PizzaOrder.Ham,
                PizzaOrder.Beef
            });
        }
    }
}
