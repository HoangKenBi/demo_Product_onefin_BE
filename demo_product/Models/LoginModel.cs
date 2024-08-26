using System.ComponentModel.DataAnnotations;

namespace demo_product.Models
{
    // login: xác thực dữ liệu đầu vào của người dùng khi họ đăng nhập vào hệ thống
    public class LoginModel
    {
        [Required]
        [MaxLength(50)]
        public string? Username { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Password { get; set; }
    }
}
