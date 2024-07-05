using CoffeeWebApp.Models;

namespace CoffeeWebApp.Interfaces
{
    public interface IAdminRepository
    {
        Task<List<UserModel>> GetAllUsers();
        Task<List<ProductModel>> GetAllProducts();
        Task<List<CategoryModel>> GetAllCategories();

    }
}
