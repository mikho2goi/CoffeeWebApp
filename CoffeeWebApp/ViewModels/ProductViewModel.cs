using CoffeeWebApp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace CoffeeWebApp.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public string NameProduct { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; } = null;
        public String? UrlImage { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel? Category { get; set; }

        public ICollection<BillModel> BillModels { get; set; }
    }
}
