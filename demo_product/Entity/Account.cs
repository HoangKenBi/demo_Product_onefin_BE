using demo_product.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace demo_product.Entity
{
    [Table("Account")]
    public class Account
    {
        [Key]
        public int idAccount { get; set; }
        [Required]
        [MaxLength(250)]
        public string? fullNameAccount { get; set; }
        [Required]
        [MaxLength(100)]
        public string? emailAccount { get; set; }
        [Required]
        [MaxLength(50)]
        public string? userNameAccount { get; set; }
        [Required]
        [MaxLength(50)]
        public string? passwordAccount { get; set; }
        [Required]
        public RoleAccount Role { get; set; }
    }
}
