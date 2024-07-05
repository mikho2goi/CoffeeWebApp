using CoffeeWebApp.Models;

namespace CoffeeWebApp.ViewModels
{
    public class ListProductViewModel
    {
        public IEnumerable<ProductModel> Products { get; set; }
    }
}
