using demo_product.Models;

namespace demo_product.Interface
{
    public interface IOrderRespository
    {
        public Task<List<OrderModel>> GetAllOrderAsync();
        public Task<OrderModel> GetOrderAsync(int id);
        public Task<int> AddOrderAsync(OrderModel model);
        public Task<bool> UpdateOrderAsync(int id, OrderModel model);
        public Task<bool> DeleteOrderAsync(int id);
    }
}
