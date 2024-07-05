using CoffeeWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoffeeWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserModel>
    {
       public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<BillModel> bills { get; set; }
        public DbSet<CategoryModel> categories { get; set; }
        public DbSet<PaymentModel> payments { get; set; }
        public DbSet<UserModel> users { get; set; }
        public DbSet<ProductModel> products { get; set; }
    }
}
