using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CoffeeWebApp.Models
{
    public class UserModel : IdentityUser
    {
        public DateTime BirthDay { get; set; }

        public ICollection<BillModel> BillModels { get; set; }
    }
}
