using demo_product.Models;

namespace demo_product.Interface
{
    public interface IOrderDetailRespository
    {
        public Task<bool> UpdateOrderDetailAsync(int id, OrderDetailModelView model);
        public Task<bool> DeleteOrderDetailAsync(int id);
        public Task<List<OrderDetailModelView>> GetAllOrderDetailViewAsync();
        public Task<OrderDetailModelView> GetOrderDetailByIdViewAsync(int id);

        List<OrderDetailModelView> GetSearchView(int page = 1, int pageSize = 8);
    }
}
