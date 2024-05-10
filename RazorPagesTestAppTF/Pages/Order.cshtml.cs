using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesTestAppTF.Data.DbModels;
using RazorPagesTestAppTF.Data;
using System.Text;

namespace RazorPagesTestAppTF.Pages
{
    public class OrderModel : PageModel
    {
        public List<PizzaOrder> PizzaOrders { get; set; }
        public List<PizzaOrderViewModel> FilteredPizzaOrders { get; set; }

        private readonly ApplicationDbContext _db;

        public OrderModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public class PizzaOrderViewModel
        {
            public int Id { get; set; }
            public string PizzaName { get; set; }
            public float PizzaPrice { get; set; }
            public StringBuilder Ingredients { get; set; }
        }

        public IActionResult OnGet(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString) && searchString.Length < 2)
            {
                ViewData["SearchError"] = "Please enter at least 2 letters for search.";
                return Page();
            }
            PizzaOrders = _db.PizzaOrders.ToList();
            FilteredPizzaOrders = GetPizzaOrderViewModel(PizzaOrders);

            if (FilteredPizzaOrders == null)
            {
                FilteredPizzaOrders = new List<PizzaOrderViewModel>();
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                FilteredPizzaOrders = FilteredPizzaOrders
                    .Where(p => p.PizzaName.Contains(searchString, System.StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            ViewData["FilteredPizzaOrders"] = FilteredPizzaOrders;
            return Page();
        }

        public IActionResult OnPostDelete(int orderId)
        {
            var orderToDelete = _db.PizzaOrders.FirstOrDefault(o => o.Id == orderId);
            if (orderToDelete == null)
            {
                return RedirectToPage("/NotFoundPage");
            }

            _db.PizzaOrders.Remove(orderToDelete);
            _db.SaveChanges();

            return RedirectToPage("/Order");
        }

        public IActionResult OnGetUpdateOrder(int orderId)
        {
            return RedirectToPage("./UpdateOrder", new { orderId = orderId });
        }





        public List<PizzaOrderViewModel> GetPizzaOrderViewModel(List<PizzaOrder> pizzaOrders)
        {
            var pizzaOrderViewModels = new List<PizzaOrderViewModel>();

            var ingredientProperties = typeof(PizzaOrder).GetProperties().Where(p => p.PropertyType == typeof(bool));

            foreach (var order in pizzaOrders)
            {
                var pizzaModel = new PizzaOrderViewModel()
                {
                    Id = order.Id,
                    PizzaPrice = (float)order.Price,
                    PizzaName = order.PizzaName,
                    Ingredients = new StringBuilder()
                };

                foreach (var prop in ingredientProperties)
                {
                    var propValue = prop.GetValue(order);
                    if (propValue != null && (bool)propValue)
                    {
                        pizzaModel.Ingredients.Append($"{prop.Name}, ");
                    }
                }

                if(pizzaModel.Ingredients.Length > 0)
                {
                    pizzaModel.Ingredients.Length -= 2;
                }

                pizzaOrderViewModels.Add(pizzaModel);
            }

            return pizzaOrderViewModels;
        }
    }
}
