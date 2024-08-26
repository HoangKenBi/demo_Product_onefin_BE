namespace demo_product.Models
{
    // login: chứa thông tin về người dùng sau đăng nhập thành công.
    public class AuthResponse
    {
        public string? Username { get; set; }
        public string? Role { get; set; }
    }
}
