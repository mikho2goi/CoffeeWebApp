using CoffeeWebApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
           var products = await _productRepository.GetAll();
            // Lấy dữ liệu từ server

            return Json(products);
        }
    }
}
