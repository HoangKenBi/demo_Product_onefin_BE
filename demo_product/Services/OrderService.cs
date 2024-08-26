using AutoMapper;
using demo_product.Data;
using demo_product.Entity;
using demo_product.Interface;
using demo_product.Models;
using Microsoft.EntityFrameworkCore;

namespace demo_product.Services
{
    public class OrderService : IOrderRespository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public OrderService(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // Thêm
        public async Task<int> AddOrderAsync(OrderModel model)
        {
            var newOrder = _mapper.Map<Order>(model);
            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();
            return newOrder.idOrder;
        }
        // Xóa
        public async Task<bool> DeleteOrderAsync(int id)
        {
            var deleteOrder = _context.Orders.SingleOrDefault(o => o.idOrder == id);
            if (deleteOrder == null)
            {
                return false; // không tồn tại 
            }
            _context.Orders.Remove(deleteOrder);
            await _context.SaveChangesAsync();
            return true; //Xóa thành công
        }
        // Lấy tất cả
        public async Task<List<OrderModel>> GetAllOrderAsync()
        {
            var order = await _context.Orders.ToListAsync();
            return _mapper.Map<List<OrderModel>>(order);
        }
        // Lấy ra 1
        public async Task<OrderModel> GetOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            return _mapper.Map<OrderModel>(order);
        }
        // Sửa
        public async Task<bool> UpdateOrderAsync(int id, OrderModel model)
        {
            var updateOrder = await _context.Orders.FindAsync(id);
            if (updateOrder == null)
            {
                return false; // không tồn tại 
            }
            //Map dữ liệu từ model vào updateOrder
            _mapper.Map(model, updateOrder);
            // Cập nhật
            _context.Orders.Update(updateOrder);
            await _context.SaveChangesAsync();
            return true; //Cập nhật thành công
        }
    }
}
