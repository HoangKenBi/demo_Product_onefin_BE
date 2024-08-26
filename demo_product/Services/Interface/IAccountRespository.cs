using demo_product.Models;

namespace demo_product.Interface
{
    public interface IAccountRespository
    {
        public Task<List<AccountModel>> GetAllAccountAsync();
        public Task<AccountModel> GetAccountAsync(int id);
        public Task<int> AddAccountAsync(AccountModel model);
        public Task<bool> UpdateAccountAsync(int id, AccountModel model);
        public Task<bool> DeleteAccountAsync(int id);

        List<AccountModel> GetSearchAccount(string? search, string? sortBy, int page = 1);
    }
}
