using demo_product.Data;
using demo_product.Interface;
using demo_product.Models;
using Microsoft.EntityFrameworkCore;

namespace demo_product.Services
{
    // login
    public class AuthService : IAuthService
    {
        private readonly MyDbContext _context;
        public AuthService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<AuthResponse> AuthenticateAsync(string username, string password)
        {
            var user = await _context.Accounts
                .Where(a => a.userNameAccount == username && a.passwordAccount == password)
                .FirstOrDefaultAsync();

            if (user == null)
                return null;

            // Trả về thông tin người dùng hoặc token
            return new AuthResponse
            {
                Username = user.userNameAccount,
                Role = user.Role.ToString()
            };
        }
    }
}
