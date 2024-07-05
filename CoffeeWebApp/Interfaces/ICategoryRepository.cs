using CoffeeWebApp.Models;

namespace CoffeeWebApp.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryModel>> GetAll();
        Task<CategoryModel> GetByIdAsync(int id);
        Task<IEnumerable<CategoryModel>> GetByNameAsync(string name);
        bool Add(CategoryModel category);
        bool Update(CategoryModel category);
        bool Delete(CategoryModel category);

        bool Save();
    }
}
