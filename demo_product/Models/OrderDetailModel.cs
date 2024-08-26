using demo_product.Entity;
using demo_product.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace demo_product.Models
{
    public class OrderDetailModel
    {
        public int idOrderDetail { get; set; }
        public int idProduct { get; set; }
        public int idOrder { get; set; }
        public int totalQuantity { get; set; }
        public double totalPrice { get; set; }
        public StatusOrderDetail statusOrderDetail { get; set; }
    }

    public class OrderDetailModelView
    {
        public int idOrderDetail { get; set; }
        public string? nameProduct { get; set; }
        public double priceProduct { get; set; }
        public int quantityProduct { get; set; }
        public string? imgProduct { get; set; }
        public string? nameOrder { get; set; }
        public string? phoneOrder { get; set; }
        public string? emailOrder { get; set; }
        public string? addressOrder { get; set; }
        public int totalQuantity { get; set; }
        public double totalPrice { get; set; }
        public StatusOrderDetail statusOrderDetail { get; set; }
    }
}
