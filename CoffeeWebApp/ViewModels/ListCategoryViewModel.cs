using CoffeeWebApp.Models;

namespace CoffeeWebApp.ViewModels
{
    public class ListCategoryViewModel
    {
        public IEnumerable<CategoryModel> Categories { get; set; }
        public CategoryModel Category { get; set; } = new CategoryModel();
        public IEnumerable<ProductModel> Products { get; set; }
        public ProductViewModel Product { get; set; } = new ProductViewModel();
    }
}
