using System.ComponentModel.DataAnnotations;

namespace demo_product.Models
{
    public class OrderModel
    {
        public int idOrder { get; set; }
        public string? nameOrder { get; set; }
        public string? phoneOrder { get; set; }
        public string? emailOrder { get; set; }
        public string? addressOrder { get; set; }
    }
}
