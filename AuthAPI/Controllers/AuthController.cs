using Microsoft.AspNetCore.Mvc;
using AuthAPI.Models;
using AuthAPI.Repositories;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public AuthController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User model)
        {
            if (await _userRepository.UserExistsAsync(model.Username))
            {
                return BadRequest("User already exists.");
            }

            // Hash the password before storing it
            model.Password = HashPassword(model.Password);

            await _userRepository.AddUserAsync(model);

            return Ok(new { message = "Registration successful." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User model)
        {
			 Console.WriteLine($"Username: {model.Username}, Password: {model.Password}");
            // Find user by username
            var user = await _userRepository.GetUserByUsernameAsync(model.Username);

            if (user == null || !VerifyPassword(model.Password, user.Password))
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(new { message = "Login successful." });
        }

        // Utility method to hash the password
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        // Utility method to verify the hashed password
        private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            return HashPassword(enteredPassword) == storedPasswordHash;
        }
    }
}