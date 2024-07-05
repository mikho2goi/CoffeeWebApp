using CoffeeWebApp.Data;
using CoffeeWebApp.Interfaces;
using CoffeeWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeWebApp.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(ProductModel product)
        {
            _context.Add(product);
            return Save();
        }

        public bool Delete(ProductModel product)
        {
            _context.Remove(product);
            return Save();
        }
        public async Task<IEnumerable<ProductModel>> GetAll()
        {
            return await _context.products.ToListAsync();
        }

        public async Task<ProductModel> GetById(string id)
        {
            return await _context.products.Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductModel>> GetByNameAsync(string name)
        {
            return await _context.products.Where(n => n.NameProduct == name).ToListAsync();
        }

        public async Task<IEnumerable<ProductModel>> GetByPrice(decimal price)
        {
            return await _context.products.Where(p => p.Price.Equals(price)).ToListAsync();
        }

    
        public bool Update(ProductModel product)
        {
            _context.Update(product);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<ProductModel>> GetByCategoryId(int id)
        {
            return await _context.products.Where(i => i.CategoryId == id).ToListAsync();
        }
    }
}
