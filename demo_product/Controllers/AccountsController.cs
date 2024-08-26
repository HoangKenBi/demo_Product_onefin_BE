using demo_product.Interface;
using demo_product.Models;
using demo_product.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace demo_product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRespository _repo;
        private readonly IAuthService _authService;
        public AccountsController(IAccountRespository repo, IAuthService authService)
        {
            _repo = repo;
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            // Kiểm tra xem dữ liệu đầu vào
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            //  kiểm tra username và password trong cơ sở dữ liệu
            var result = await _authService.AuthenticateAsync(model.Username!, model.Password!);

            if (result == null)
                return Unauthorized("Invalid username or password.");

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAccount()
        {
            try
            {
                return Ok(await _repo.GetAllAccountAsync());
            }catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(int id)
        {
            var account = await _repo.GetAccountAsync(id);
            return account == null ? NotFound() : Ok(account);
        }

        [HttpPost]
        public async Task<IActionResult> AddAccount(AccountModel model)
        {
            try
            {
                var newaccount = await _repo.AddAccountAsync(model);
                var account = await _repo.GetAccountAsync(newaccount);
                return account == null ? NotFound() : Ok(account);
            }catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount( int id, AccountModel model)
        {
            if (id != model.idAccount)
            {
                return BadRequest("ID mismatch"); // Trả về BadRequest nếu ID không khớp
            }
            var isUpdate = await _repo.UpdateAccountAsync(id, model);
            if (!isUpdate)
            {
                return NotFound(); // Trả về NotFound nếu tài khoản không tồn tại
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            if (id == 0)
            {
                return BadRequest("ID cannot be zero"); // Trả về BadRequest nếu ID là 0
            }
            try
            {
                var isDelete = await _repo.DeleteAccountAsync(id);
                if (!isDelete)
                {
                    return NotFound(); // Trả về NotFound nếu tài khoản không tồn tại
                }
                return Ok(); // Trả về Ok nếu xóa thành công
            }catch (Exception)
            {
                return StatusCode(500); // Trả về lỗi máy chủ nếu có ngoại lệ
            }
        }
        [HttpGet("search")]
        public IActionResult GetAllSearchAccount(string? search, string? sortBy, int page = 1)
        {
            try
            {
                var result = _repo.GetSearchAccount(search, sortBy, page);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
