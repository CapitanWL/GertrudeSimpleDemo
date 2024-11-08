using Microsoft.AspNetCore.Mvc;
using server.Intarfaces;
using server.Models;
using server.DTOs;
using Swashbuckle.AspNetCore.Annotations;

namespace server.Controllers
{
    /// <summary>
    /// Контроллер, представляющий методы работы с действиями пользователя (регистрация и авторизация).
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Сервис пользователя.
        /// </summary>
        private readonly IUserService _userService;
        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Инициализация нового экземпляра <see cref="UserController"/> класса.
        /// </summary>
        /// <param name="userService">Сервис пользователя.</param>
        /// <param name="dbContext">Контекст базы данных.</param>
        public UserController(IUserService userService, ApplicationDbContext dbContext)
        {
            _userService = userService;
            _context = dbContext;
        }

        /// <summary>
        /// Регистрация специализированной DTO пользователя.
        /// </summary>
        /// <remarks>Позволяет зарегистрировать нового пользователя с уникальным никнеймом и адресом электронной почты.</remarks>
        /// <param name="registerDto">DTO регистрации пользователя.</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _userService.UserExistInSystemForNickname(registerDto.Nickname) == true)
            {
                return BadRequest("Данный никнейм занят. Попробуйте другой.");
            }

            if (await _userService.UserExistInSystemForEmail(registerDto.Email) == true)
            {
                return BadRequest("Данный почтовый адрес уже используется. Попробуйте войти или зарегистрировать другой.");
            }

            var user = new Models.User
            {
                Nickname = registerDto.Nickname,
                Email = registerDto.Email
            };

            bool result = await _userService.CreateAsync(user, registerDto.Password);

            if (result == true)
            {
                return Ok(new { Message = "Пользователь успешно зарегистрирован." });
            }

            return BadRequest();
        }

        /// <summary>
        /// Авторизация специализированной DTO пользователя.
        /// </summary>
        /// <remarks>Позволяет авторизировать пользователя с существующим в системе адресом электронной почты и соответсвующим зарегистрированному пользователю паролем.</remarks>
        /// <param name="authDto">DTO авторизации пользователя.</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost("autorization")]
        public async Task<IActionResult> Autorization([FromBody] AuthDto authDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool result = await _userService.VerifyAsync(authDto.Email, authDto.Password);

            if (result == true)
            {
                return Ok(new { Message = "Пользователь успешно авторизирован." });
            }

            return BadRequest("Неверный адрес электронной почты или пароль. Попробуйте снова.");
        }
    }
}
