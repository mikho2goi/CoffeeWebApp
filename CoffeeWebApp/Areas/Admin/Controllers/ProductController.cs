using CoffeeWebApp.Interfaces;
using CoffeeWebApp.Models;
using CoffeeWebApp.Repository;
using CoffeeWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Numerics;

namespace CoffeeWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPhotoService _photoService;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IPhotoService photoService)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(ListCategoryViewModel listCategoryViewModel)
        {
            var updatedViewModel = await updateList(listCategoryViewModel);
            return View(updatedViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> test(ListCategoryViewModel listCategoryViewModel)
        {
            CategoryModel category;
            listCategoryViewModel.Products = await _productRepository.GetAll();
            foreach (var product in listCategoryViewModel.Products)
            {

                category = await _categoryRepository.GetByIdAsync(product.CategoryId);
                product.Category.CategoryName = category.CategoryName;
            }
            return View(listCategoryViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ListCategoryViewModel listCategoryViewModel)
        {
            var checkDupLicate = await _productRepository.GetByNameAsync(listCategoryViewModel.Product.NameProduct);

            if (!checkDupLicate.Any())
            {
                string imageUrl = string.Empty;
                if (listCategoryViewModel.Product.Image != null)
                {
                    var result = await _photoService.AddPhotoAsync(listCategoryViewModel.Product.Image);
                    imageUrl = result.Url.ToString();
                }
                ProductModel product = new ProductModel
                {
                    Price = listCategoryViewModel.Product.Price,
                    NameProduct = listCategoryViewModel.Product.NameProduct,
                    CategoryId = listCategoryViewModel.Product.CategoryId,
                    Description = listCategoryViewModel.Product.Description,
                    Quantity = listCategoryViewModel.Product.Quantity,
                    UrlImage = imageUrl,
                };
                _productRepository.Add(product);

                TempData["Success"] = "Thêm Sản Phẩm Thành Công";
            }
            else
            {
                TempData["Error"] = "Đã Tồn Tại Tên Sản Phẩm Này";
            }
            var updatedViewModel = await updateList(listCategoryViewModel);
            return View("~/Areas/Admin/Views/Product/Index.cshtml", updatedViewModel);

        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ListCategoryViewModel listCategoryViewModel)
        {
            string imageUrl = string.Empty;
            if (listCategoryViewModel.Product.Image != null)
            {
                var result = await _photoService.AddPhotoAsync(listCategoryViewModel.Product.Image);
                imageUrl = result.Url.ToString();
            }
            else
            {
                imageUrl = listCategoryViewModel.Product.UrlImage;
            }

            var product = await _productRepository.GetById(listCategoryViewModel.Product.Id);
            product.Quantity = listCategoryViewModel.Product.Quantity;
            product.Price = listCategoryViewModel.Product.Price;    
            product.NameProduct = listCategoryViewModel.Product.NameProduct;
            product.UrlImage = imageUrl;
            product.CategoryId = listCategoryViewModel.Product.CategoryId;
            product.Description = listCategoryViewModel.Product.Description;
            bool updateState = _productRepository.Update(product);
            if (updateState)
            {
                TempData["Success"] = "Cập Nhật Sản Phẩm Thành Công";
            }
            else
            {
                TempData["Error"] = "Cập Nhật Sản Phẩm Thất Bại";
            }
            var updatedViewModel = await updateList(listCategoryViewModel);
            return View("~/Areas/Admin/Views/Product/Index.cshtml", updatedViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(ListCategoryViewModel listCategoryViewModel)
        {
            var product = await _productRepository.GetById(listCategoryViewModel.Product.Id);
            if (product != null)
            {
                _productRepository.Delete(product);
                if (!string.IsNullOrEmpty(product.UrlImage))
                {
                    await _photoService.DeletePhotoAsync(listCategoryViewModel.Product.UrlImage);
                }
                TempData["Success"] = "Xóa Sản Phẩm Thành Công";
            }
            var updatedViewModel = await updateList(listCategoryViewModel);
            return View("~/Areas/Admin/Views/Product/Index.cshtml", updatedViewModel);
        }

        public async Task<ListCategoryViewModel> updateList(ListCategoryViewModel listCategoryViewModel)
        {
            listCategoryViewModel.Products = await _productRepository.GetByCategoryId(listCategoryViewModel.Category.CategoryId);
            listCategoryViewModel.Category = await _categoryRepository.GetByIdAsync(listCategoryViewModel.Category.CategoryId);
            return listCategoryViewModel;
        }

    }
}
