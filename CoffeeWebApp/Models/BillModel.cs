using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeWebApp.Models
{
    public class BillModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillId {  get; set; }
        public int BillProductQuantity { get; set; }

        public decimal ToTalPrice { get; set; }

        public DateTime BillDate { get; set; } 

        public int PaymentId { get; set; }
        public PaymentModel Payment { get; set; }

        public UserModel User { get; set; }
        public ICollection<ProductModel> ProductModels { get; set; }
    }
}
