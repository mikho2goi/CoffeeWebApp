using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeWebApp.Models
{
    public class ProductModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public string NameProduct { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set;}

        public string? Description { get; set;}
        public string? UrlImage { get; set;}
        public int CategoryId { get; set; }
        public CategoryModel? Category { get; set; }

        public ICollection<BillModel>? BillModels { get; set; }
    }
}
