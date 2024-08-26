using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace demo_product.Entity
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int idOrder { get; set; }
        [Required]
        [MaxLength(150)]
        public string? nameOrder { get; set; }
        [Required]
        [MaxLength(10)]
        public string? phoneOrder { get; set; }
        [Required]
        [MaxLength(50)]
        public string? emailOrder { get; set; }
        [Required]
        [MaxLength(500)]
        public string? addressOrder { get; set; }
        //public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
