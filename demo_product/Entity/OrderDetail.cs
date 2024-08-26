using demo_product.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace demo_product.Entity
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        [Key]
        public int idOrderDetail { get; set; }
        public int idProduct { get; set; }
        [ForeignKey("idProduct")]
        public Product? Product { get; set; } = null;
        public int idOrder { get; set; }
        [ForeignKey("idOrder")]
        public Order? Order { get; set; } = null;

        public int totalQuantity { get; set; }
        public double totalPrice { get; set; }
        [Required]
        public StatusOrderDetail statusOrderDetail { get; set; }
    }
    public class OrderDetailVIew
    {   
        public int idOrderDetail { get; set; }
        public int idProduct { get; set; }
        public int idOrder { get; set; }
        public int totalQuantity { get; set; }
        public double totalPrice { get; set; }
        public Order? Order { get; set; } = null;
        public StatusOrderDetail statusOrderDetail { get; set; }

    }
}
