using demo_product.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace demo_product.Entity
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int idProduct { get; set; }
        [Required]
        [MaxLength(100)]
        public string? nameProduct { get; set; }
        [Required]
        public double? priceProduct { get; set; }
        [Required]
        public int quantityProduct { get; set; }
        [Required]
        public string? imgProduct { get; set; }
        [Required]
        public StatusProduct statusProduct { get; set; }

        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
