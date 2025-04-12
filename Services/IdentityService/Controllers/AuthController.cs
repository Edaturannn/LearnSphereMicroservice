using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IdentityService.Data;
using IdentityService.Entity;
using IdentityService.Models;
using IdentityService.Jwt;

namespace IdentityService.Controllers
{
   [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthController(AppDbContext context, IConfiguration config, JwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _config = config;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Bu email ile kayıtlı kullanıcı zaten var.");

            var user = new User
            {
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Kayıt başarılı.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Email veya şifre hatalı.");

            var token = _jwtTokenGenerator.GenerateJwtToken(user);
            return Ok(new { token });
        }
    }
}
