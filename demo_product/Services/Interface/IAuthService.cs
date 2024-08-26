using demo_product.Models;

namespace demo_product.Interface
{
    // login
    public interface IAuthService
    {
        Task<AuthResponse> AuthenticateAsync(string username, string password);
    }
}
