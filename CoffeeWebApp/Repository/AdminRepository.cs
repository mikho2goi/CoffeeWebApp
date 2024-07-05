using CoffeeWebApp.Data;
using CoffeeWebApp.Interfaces;
using CoffeeWebApp.Models;

namespace CoffeeWebApp.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminRepository(ApplicationDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public Task<List<CategoryModel>> GetAllCategories()
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductModel>> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Task<List<UserModel>> GetAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}
