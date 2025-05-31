using BookReviewAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByEmail(loginDto.Email, cancellationToken);
            if (user == null || user.Password != loginDto.Password) // 🔐 на проде сравнение паролей должно быть через хэш
                return Unauthorized("Неверный email или пароль");

            return Ok(user); // или JWT, если есть авторизация
        }
    }
}
