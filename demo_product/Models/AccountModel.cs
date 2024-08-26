using demo_product.Enum;
using System.ComponentModel.DataAnnotations;

namespace demo_product.Models
{
    public class AccountModel
    {
        public int idAccount { get; set; }
        public string? fullNameAccount { get; set; }
        public string? emailAccount { get; set; }
        public string? userNameAccount { get; set; }
        public string? passwordAccount { get; set; }
        public RoleAccount Role { get; set; }
    }
}
