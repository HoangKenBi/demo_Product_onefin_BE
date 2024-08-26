using demo_product.Models;

namespace demo_product.Interface
{
    public interface IProductRespository
    {
        public Task<List<ProductModel>> GetAllProductAsync();
        public Task<ProductModel> GetProductAsync(int id);
        public Task<int> AddProductAsync(ProductModel model);
        public Task<bool> UpdateProductAsync(int id, ProductModel model);
        public Task<bool> DeleteProductAsync(int id);

        List<ProductModel> GetSearchProduct(string? search, string? sortBy, int page = 1);
    }
}
