using CoffeeWebApp.Data;
using CoffeeWebApp.Interfaces;
using CoffeeWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeWebApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(CategoryModel category)
        {
            category.CategoryName = category.CategoryName.ToUpper();
            _context.Add(category);
            return Save();
    }
        public bool Delete(CategoryModel category)
        {
            _context.Remove(category);
            return Save();
        }

        public async Task<IEnumerable<CategoryModel>> GetAll()
        {
            return await _context.categories.ToListAsync();
        }

        public async Task<CategoryModel> GetByIdAsync(int id)
        {
            return await _context.categories.FindAsync(id);
        }

        public async Task<IEnumerable<CategoryModel>> GetByNameAsync(string name)
        {
            name = name.ToUpper(); 
            return await _context.categories.Where(n => n.CategoryName == name).ToListAsync();
        }
        public bool Update(CategoryModel category)
        {
            category.CategoryName = category.CategoryName.ToUpper();
            _context.Update(category);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
