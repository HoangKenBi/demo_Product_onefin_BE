using AutoMapper;
using demo_product.Data;
using demo_product.Entity;
using demo_product.Interface;
using demo_product.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace demo_product.Services
{
    public class AccountService : IAccountRespository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;
        public static int PAGE_SIZE { get; set; } = 6;
        public AccountService(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //Thêm
        public async Task<int> AddAccountAsync(AccountModel model)
        {
            var newAccount = _mapper.Map<Account>(model);
            _context.Accounts.Add(newAccount);
            await _context.SaveChangesAsync();
            return newAccount.idAccount;
        }
        // Xóa
        public async Task<bool> DeleteAccountAsync(int id)
        {
            var deleteAccount = _context.Accounts.SingleOrDefault(a => a.idAccount == id);
            if (deleteAccount == null)
            {
                return false; // Tài Khoản không tồn tại
            }
            _context.Accounts.Remove(deleteAccount);
            await _context.SaveChangesAsync();
            return true; // Xóa thành công
        }
        // Lấy ra 1
        public async Task<AccountModel> GetAccountAsync(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            return _mapper.Map<AccountModel>(account);
        }
        // Lấy tất cả
        public async Task<List<AccountModel>> GetAllAccountAsync()
        {
            var account = await _context.Accounts.ToListAsync();
            return _mapper.Map<List<AccountModel>>(account);
        }
        // Sửa
        public async Task<bool> UpdateAccountAsync(int id, AccountModel model)
        {
            var updateAccount = await _context.Accounts.FindAsync(id);
            if (updateAccount == null)
            {
                return false; // Tài Khoản không tồn tại
            }
            // Map dữ liệu từ model vào updateAccount 
            _mapper.Map(model, updateAccount);
            // Cập nhật
            _context.Accounts.Update(updateAccount);
            await _context.SaveChangesAsync();
            return true;// Cập nhật thành công
        }
        // Search
        public List<AccountModel> GetSearchAccount(string? search, string? sortBy, int page = 1)
        {
            var allACcount = _context.Accounts.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                allACcount = allACcount.Where(w => (w.fullNameAccount != null ? w.fullNameAccount : "").Contains(search));
            }

            allACcount = allACcount.OrderBy(w => w.fullNameAccount);
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "a_desc": allACcount = allACcount.OrderByDescending(a => a.fullNameAccount); break;
                }
            }

            allACcount = allACcount.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);
            var result = allACcount.Select(a => new AccountModel
            {
                idAccount = a.idAccount,
                fullNameAccount = a.fullNameAccount,
                emailAccount = a.emailAccount,
                userNameAccount = a.userNameAccount,
                passwordAccount = a.passwordAccount,
                Role = a.Role
            });
            return result.ToList();
        }
    }
}
