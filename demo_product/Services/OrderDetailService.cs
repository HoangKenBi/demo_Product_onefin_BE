using AutoMapper;
using demo_product.Data;
using demo_product.Entity;
using demo_product.Interface;
using demo_product.Models;
using demo_product.RabbitMQ;
using demo_product.RabbitMQ.Interface;
using Microsoft.EntityFrameworkCore;

namespace demo_product.Services
{
    public class OrderDetailService : IOrderDetailRespository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRabbitMQRespository _rabbitMQRespository;
        

        public OrderDetailService(MyDbContext context, IMapper mapper, IRabbitMQRespository rabbitMQRespository)
        {
            _context = context;
            _mapper = mapper;
            _rabbitMQRespository = rabbitMQRespository;
            
        }
        // Xóa
        public async Task<bool> DeleteOrderDetailAsync(int id)
        {
            var deleteOrderDetail = _context.OrderDetails.SingleOrDefault(or => or.idOrderDetail == id);
            if (deleteOrderDetail == null)
            {
                return false; // Không tồn tại
            }
            _context.OrderDetails.Remove(deleteOrderDetail);
            await _context.SaveChangesAsync();
            return true; // Xóa Thành Công
        }
        // lấy tất cả
        public async Task<List<OrderDetailModelView>> GetAllOrderDetailViewAsync()
        {
            var orderDetails = await(from od in _context.OrderDetails
                                     join p in _context.Products on od.idProduct equals p.idProduct
                                     join o in _context.Orders on od.idOrder equals o.idOrder
                                     select new OrderDetailModelView
                                     {
                                         idOrderDetail = od.idOrderDetail,
                                         nameProduct = p.nameProduct,
                                         priceProduct = (double)p.priceProduct,
                                         quantityProduct = od.totalQuantity,
                                         imgProduct = p.imgProduct,
                                         nameOrder = o.nameOrder,
                                         phoneOrder = o.phoneOrder,
                                         emailOrder = o.emailOrder,
                                         addressOrder = o.addressOrder,
                                         totalQuantity = od.totalQuantity,
                                         totalPrice = od.totalPrice,
                                         statusOrderDetail = od.statusOrderDetail
                                     }).ToListAsync();

            return orderDetails;
        }
        // Sửa
        public async Task<bool> UpdateOrderDetailAsync(int id, OrderDetailModelView model)
        {
            // Tìm OrderDetail theo id
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return false; // Không tìm thấy OrderDetail
            }

            // Tìm Product theo idProduct
            var product = await _context.Products.FindAsync(orderDetail.idProduct);
            if (product == null)
            {
                return false; // Không tìm thấy Product
            }

            // Tìm Order theo idOrder
            var order = await _context.Orders.FindAsync(orderDetail.idOrder);
            if (order == null)
            {
                return false; // Không tìm thấy Order
            }

            // Cập nhật thông tin từ model vào các entity tương ứng
            // Cập nhật OrderDetail
            orderDetail.totalQuantity = model.totalQuantity;
            orderDetail.totalPrice = model.totalPrice;
            orderDetail.statusOrderDetail = model.statusOrderDetail;

            // Cập nhật Product
            product.nameProduct = model.nameProduct;
            product.priceProduct = model.priceProduct;
            product.imgProduct = model.imgProduct;

            // Cập nhật Order
            order.nameOrder = model.nameOrder;
            order.phoneOrder = model.phoneOrder;
            order.emailOrder = model.emailOrder;
            order.addressOrder = model.addressOrder;

            // Lưu thay đổi vào cơ sở dữ liệu
            _context.OrderDetails.Update(orderDetail);
            _context.Products.Update(product);
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return true; // Cập nhật thành công
        }
        // Lấy 1
        public async Task<OrderDetailModelView> GetOrderDetailByIdViewAsync(int id)
        {
            return await (from od in _context.OrderDetails
                          join p in _context.Products on od.idProduct equals p.idProduct
                          join o in _context.Orders on od.idOrder equals o.idOrder
                          where od.idOrderDetail == id
                          select new OrderDetailModelView
                          {
                              idOrderDetail = od.idOrderDetail,
                              nameProduct = p.nameProduct,
                              priceProduct = (double)p.priceProduct,
                              quantityProduct = od.totalQuantity,
                              imgProduct = p.imgProduct,
                              nameOrder = o.nameOrder,
                              phoneOrder = o.phoneOrder,
                              emailOrder = o.emailOrder,
                              addressOrder = o.addressOrder,
                              totalQuantity = od.totalQuantity,
                              totalPrice = od.totalPrice,
                              statusOrderDetail = od.statusOrderDetail
                          }).FirstOrDefaultAsync();
        }
        // paging
        public List<OrderDetailModelView> GetSearchView(int page = 1, int pageSize = 8)
        {
            var orderDetails = _context.OrderDetails
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(od => new OrderDetailModelView
                {
                    idOrderDetail = od.idOrderDetail,
                    nameProduct = od.Product.nameProduct,
                    priceProduct = (double)od.Product.priceProduct,
                    imgProduct = od.Product.imgProduct,
                    nameOrder = od.Order.nameOrder,
                    phoneOrder = od.Order.phoneOrder,
                    emailOrder = od.Order.emailOrder,
                    addressOrder = od.Order.addressOrder,
                    totalQuantity = od.totalQuantity,
                    totalPrice = od.totalPrice,
                    statusOrderDetail = od.statusOrderDetail
                })
                .ToList();

            return orderDetails;
        }
    }
}
