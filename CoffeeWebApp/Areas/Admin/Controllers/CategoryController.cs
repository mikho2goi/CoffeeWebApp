using CoffeeWebApp.Interfaces;
using CoffeeWebApp.Models;
using CoffeeWebApp.Repository;
using CoffeeWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public CategoryController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<CategoryModel> categories = await _categoryRepository.GetAll();
            

            foreach (var category in categories)
            {
                IEnumerable<ProductModel> productCountById = await _productRepository.GetByCategoryId(category.CategoryId);
                category.CategoryQuantity = productCountById.Count();
            }

            var listCategoryViewModel = new ListCategoryViewModel
            {
                Categories = categories,
            };

                return View(listCategoryViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> CreateCategory(ListCategoryViewModel listCategoryViewModel)
        {
            var checkDupLicate = await _categoryRepository.GetByNameAsync(listCategoryViewModel.Category.CategoryName);

            if (checkDupLicate.Count() == 0)
            {
                _categoryRepository.Add(listCategoryViewModel.Category);
                TempData["Success"] = "Thêm Danh Mục Thành Công";
            }
            else
            {
                TempData["Error"] = "Đã Tồn Tại Tên Loại Này";
            }

            return RedirectToAction("Index", "Category");
        }


        [HttpPost]
        public async Task<IActionResult> UpdateCategory(ListCategoryViewModel listCategoryViewModel)
        {
            var checkDupLicate = await _categoryRepository.GetByNameAsync(listCategoryViewModel.Category.CategoryName);
            if (checkDupLicate.Count() == 0)
            {
                _categoryRepository.Update(listCategoryViewModel.Category);
                TempData["Success"] = "Sửa Tên Loại Thành Công";
            }
            else
            {
                TempData["Error"] = "Đã Tồn Tại Tên Loại Này";
            }

            return RedirectToAction("Index", "Category");
        }

      
        [HttpPost]
        public IActionResult DeleteCategory(ListCategoryViewModel listCategoryViewModel)
        {
            _categoryRepository.Delete(listCategoryViewModel.Category);

            return RedirectToAction("Index","Category");
        }


    }
}
