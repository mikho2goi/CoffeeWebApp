using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeWebApp.Models
{
    public class PaymentModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }

        public string PaymentMethod { get; set; }
        
        public string PaymentStatus { get; set; }

        public BillModel Bill { get; set;}
        public UserModel User { get; set; }

    }
}
