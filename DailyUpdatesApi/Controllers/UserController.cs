using Microsoft.AspNetCore.Mvc;
using DailyUpdates.Models;
using DailyUpdates.Services;

namespace DailyUpdates.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _configuration;

        public UsersController(UserService userService, IJwtService jwtService, IConfiguration configuration)
        {
            _userService = userService;
            _jwtService = jwtService;
            _configuration = configuration;
        }

        // Register
        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            if (request == null ||
                string.IsNullOrWhiteSpace(request.Username) ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Username, email, and password are required.");

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            string result = _userService.RegisterUser(request.Username, request.Email, hashedPassword);

            if (result != "User registered successfully")
                return BadRequest(result);

            return Ok(result);
        }

        // Login
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request == null ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Invalid login data.");

            if (!_userService.ValidateUserCredentials(request.Email, request.Password, out var user) || user == null)
                return Unauthorized("Invalid email or password.");

            var tokens = _jwtService.GenerateTokens(user);

            int refreshExpiryDays = _configuration.GetValue<int>("JwtSettings:RefreshTokenExpirationDays");

            _userService.SaveRefreshToken(user.Id, tokens.RefreshToken, DateTime.UtcNow.AddDays(refreshExpiryDays));

            return Ok(tokens);
        }
    }
}
