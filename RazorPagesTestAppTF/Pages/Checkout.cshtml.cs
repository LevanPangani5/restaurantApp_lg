using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesTestAppTF.Data;
using RazorPagesTestAppTF.Data.DbModels;

namespace RazorPagesTestAppTF.Pages
{
    [BindProperties(SupportsGet = true)]
    public class CheckoutModel : PageModel
    {
        //public PizzaOrder PizzaOrder { get; set; }
        public string ImageTitle { get; set; }
        public float FinalPrice { get; set; }
        public string PizzaName { get; set; }


        private readonly ApplicationDbContext _db;

        public CheckoutModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            if (string.IsNullOrWhiteSpace(PizzaName))
            {
                PizzaName = "Custom";
            }

            if (string.IsNullOrWhiteSpace(ImageTitle))
            {
                ImageTitle = "Create";
            }

            PizzaOrder pizzaOrder = new PizzaOrder()
            {
                PizzaName = PizzaName, 
                Price = FinalPrice
            };


            _db.PizzaOrders.Add(pizzaOrder);
            _db.SaveChanges();
        }
    }
}
