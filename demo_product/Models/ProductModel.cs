using demo_product.Enum;
using System.ComponentModel.DataAnnotations;

namespace demo_product.Models
{
    public class ProductModel
    {
        public int idProduct { get; set; }
        public string? nameProduct { get; set; }
        public double priceProduct { get; set; }
        public int quantityProduct { get; set; }
        public string? imgProduct { get; set; }
        public StatusProduct statusProduct { get; set; }
    }
}
