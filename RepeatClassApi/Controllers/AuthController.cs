using Microsoft.AspNetCore.Mvc;
using RepeatClassApi.Dtos;
using RepeatClassApi.Jwt;
using RepeatClassApi.Models;
using RepeatClassApi.Services;
using RegisterRequest = RepeatClassApi.Models.RegisterRequest;

namespace RepeatClassApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var user = new AddUserDto
            {
                Email = request.Email,
                Password = request.Password,
            };


            var result = await _userService.AddUser(user);

            if (result.IsSucceed)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }

        }


        [HttpPost("login")]

        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = new LoginUserDto
            {
                Email = request.Email,
                Password = request.Password,
            };

            var result = await _userService.LoginUser(user);

            if (!result.IsSucceed)
            {
                return BadRequest(result.Message);
            }
            // id yi emaili usertype çektik. Bunu tokente kullancaz
            var loginUser = result.Data;

            //appsettings gibi configuration ayarlarına ulaşmak için dependenctyInjection yapıldı.
            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var info = new JwtDto
            {
                Id = loginUser.Id,
                Email = loginUser.Email,
                UserType = loginUser.UserType,
                SecretKey = configuration["Jwt:SecretKey"]!,
                Issuer = configuration["Jwt:Issuer"]!,
                Audience = configuration["Jwt:Audience"]!,
                ExpireMinutes = int.Parse(configuration["Jwt:ExpireMinutes"]!)


            };

            var token = JwtHelper.GenerateJwtToken(info);

            return Ok(new LoginResponse
            {
                Token = token,
                Message = "Giriş Başarıyla tamamlandı"
            });


        }


    }
}
