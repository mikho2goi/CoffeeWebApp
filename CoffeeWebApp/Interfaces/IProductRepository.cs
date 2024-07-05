using CoffeeWebApp.Models;

namespace CoffeeWebApp.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductModel>> GetAll();
        Task<IEnumerable<ProductModel>> GetByCategoryId(int id);
       Task <ProductModel> GetById(string id);
        Task<IEnumerable<ProductModel>> GetByNameAsync(string name);
        Task<IEnumerable<ProductModel>> GetByPrice(decimal price);
        bool Add(ProductModel product);
        bool Update(ProductModel product);
        bool Delete(ProductModel product);

        bool Save();
    }
}
