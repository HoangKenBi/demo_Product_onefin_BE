using demo_product.Entity;
using demo_product.Interface;
using demo_product.Models;
using demo_product.RabbitMQ;
using demo_product.RabbitMQ.Interface;
using demo_product.Services;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace demo_product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailRespository _repo;
        private readonly IRabbitMQRespository _rabbitMQService;

        public OrderDetailsController(IOrderDetailRespository repo, IRabbitMQRespository rabbitMQService)
        {
            _repo = repo;
            _rabbitMQService = rabbitMQService;
        }
        [HttpGet]
        [Route("view")]
        public async Task<IActionResult> GetAllOrderDetailView()
        {
            try
            {
                var result = await _repo.GetAllOrderDetailViewAsync();
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("send")]
        public IActionResult SendOrderDetail([FromBody] OrderDetailVIew orderDetail)
        {
            if (orderDetail == null)
            {
                return BadRequest("OrderDetail cannot be null.");
            }
            var request = new OrderDetail()
            {
                idOrder = orderDetail.idOrder,
                idOrderDetail = orderDetail.idOrderDetail,
                idProduct = orderDetail.idProduct,
                totalPrice = orderDetail.totalPrice,
                totalQuantity = orderDetail.totalQuantity,
                statusOrderDetail = orderDetail.statusOrderDetail,
                Order = orderDetail.Order
            };
            _rabbitMQService.SendOrderDetail(request);
            return Ok(request);
        }
        //[HttpGet("listen")]
        //public async Task<IActionResult> StartListening()
        //{
        //    try
        //    {

        //            using (var cts = new CancellationTokenSource())
        //            {
        //                // Truyền cancellationToken vào
        //                await _rabbitMQService.StartListeningAsync(cts.Token);
        //            }

        //        return Ok("Success");
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest();
        //    }
        //}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetail(int id)
        {
            try
            {
                var orderDetail = await _repo.GetOrderDetailByIdViewAsync(id);
                if (orderDetail == null)
                {
                    return NotFound(); // Trả về 404 nếu không tìm thấy
                }
                return Ok(orderDetail); // Trả về 200 OK với thông tin chi tiết đơn hàng
            }
            catch (Exception)
            {
                // Log the exception (optional)
                return StatusCode(500, "Internal server error"); // Trả về 500 nếu có lỗi server
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderDetail(OrderDetailModelView model, int id)
        {
            if (await _repo.UpdateOrderDetailAsync(id, model))
            {
                return Ok();
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            if (id == 0)
            {
                return BadRequest("ID cannot be zzero!");
            }
            try
            {
                var isDelete = await _repo.DeleteOrderDetailAsync(id);
                if (!isDelete)
                {
                    return NotFound();
                }
                return Ok();
            }catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpGet("search")]
        public IActionResult GetSearchView(int page = 1, int pageSize = 8)
        {
            var result = _repo.GetSearchView(page, pageSize);
            return Ok(result);
        }
    }
}
