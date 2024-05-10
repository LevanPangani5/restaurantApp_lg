using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RazorPagesTestAppTF.Data.DbModels;

namespace RazorPagesTestAppTF.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<PizzaOrder> PizzaOrders { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
