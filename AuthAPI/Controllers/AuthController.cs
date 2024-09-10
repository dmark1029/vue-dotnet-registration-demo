using Microsoft.AspNetCore.Mvc;
using AuthAPI.Models;
using AuthAPI.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly InMemoryUserRepository _userRepository;

        public AuthController()
        {
            _userRepository = new InMemoryUserRepository();
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User model)
        {
            if (_userRepository.UserExists(model.Username))
            {
                return BadRequest("User already exists.");
            }

            // Hash the password before storing it
            model.Password = HashPassword(model.Password);

            _userRepository.AddUser(model);

            return Ok(new { message = "Registration successful." });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User model)
        {
            var user = _userRepository.GetUserByUsername(model.Username);

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