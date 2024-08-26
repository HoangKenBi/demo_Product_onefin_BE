using demo_product.Interface;
using demo_product.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace demo_product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRespository _repo;

        public OrdersController(IOrderRespository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            try
            {
                return Ok(await _repo.GetAllOrderAsync());
            }catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id) 
        {
            var order = await _repo.GetOrderAsync(id);
            return order == null ? NotFound() : Ok(order);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderModel model)
        {
            try
            {
                var neworder = await _repo.AddOrderAsync(model);
                var order = await _repo.GetOrderAsync(neworder);
                return order == null ? NotFound() : Ok(order);
            }catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, OrderModel model)
        {
            if (id != model.idOrder)
            {
                return BadRequest();
            }
            var isUpdate = await _repo.UpdateOrderAsync(id, model);
            if (!isUpdate)
            {
                return NotFound();
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (id == 0)
            {
                return BadRequest("ID cannot be zero");
            }
            try
            {
                var isDelete = await _repo.DeleteOrderAsync(id);
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
    }
}
