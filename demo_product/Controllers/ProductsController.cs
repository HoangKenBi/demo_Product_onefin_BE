using demo_product.Interface;
using demo_product.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace demo_product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRespository _repo;

        public ProductsController(IProductRespository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            try
            {
                return Ok(await _repo.GetAllProductAsync());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _repo.GetProductAsync(id);
            return product == null ? NotFound() : Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductModel model)
        {
            try
            {
               var newproduct = await _repo.AddProductAsync(model);
               var product = await _repo.GetProductAsync(newproduct);
               return product == null ? NotFound() : Ok(product);
            }catch (Exception)
            {
               return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductModel model)
        {
            if(id != model.idProduct)
            {
                return BadRequest("ID mismatch"); // Trả về BadRequest nếu ID không khớp
            }
            var isUpdate = await _repo.UpdateProductAsync(id, model);
            if (!isUpdate)
            {
                return NotFound(); // Trả về NotFound nếu sản phẩm không tồn tại
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id == 0)
            {
                return BadRequest("ID cannot be zero"); // Trả về BadRequest nếu ID là 0
            }
            try
            {
                var isDeleted = await _repo.DeleteProductAsync(id);
                if (!isDeleted)
                {
                    return NotFound(); // Trả về NotFound nếu sản phẩm không tồn tại
                }
                return Ok(); // Trả về Ok nếu xóa thành công
            }
            catch (Exception)
            {
                return StatusCode(500); // Trả về lỗi máy chủ nếu có ngoại lệ
            }
        }
        [HttpGet("search")]
        public IActionResult GetAllSearchProduct(string? search, string? sortBy, int page = 1)
        {
            try
            {
                var result = _repo.GetSearchProduct(search, sortBy, page);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
