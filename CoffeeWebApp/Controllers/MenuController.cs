using CoffeeWebApp.Interfaces;
using CoffeeWebApp.Repository;
using CoffeeWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;

namespace CoffeeWebApp.Controllers
{
    public class MenuController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public MenuController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetAll();
            var products = await _productRepository.GetAll();

            var productsByCategory = products.GroupBy(p => p.CategoryId)
                                             .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var category in categories)
            {
                if (productsByCategory.TryGetValue(category.CategoryId, out var productList))
                {
                    category.Products.AddRange(productList);
                }
            }

            ListCategoryViewModel listCategoryViewModel = new ListCategoryViewModel
            {
                Categories = categories
            };

            return View(listCategoryViewModel);
        }

    }
}
