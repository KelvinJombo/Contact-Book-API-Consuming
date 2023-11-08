using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyContactBookAPI.Core.Interfaces;
using MyContactBookAPI.Core.RepoServices;
using MyContactBookAPI.Models.Domain;
using MyContactBookAPI.Models.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyContactBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;    
        private readonly IRegister register;         
        private readonly SignInManager<User> signInManager;
        private readonly IConfiguration config;

        public AuthController(UserManager<User> userManager, IRegister register, SignInManager<User> signInManager, IConfiguration config)
        {
            this.userManager = userManager;          
            this.register = register;             
            this.signInManager = signInManager;
            this.config = config;
        }




        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return NotFound(new { Message = "User not found. Please check your credentials and try again." });

            }
            var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return Unauthorized(new { Message = "Invalid credentials" });
            }
            var role = (await userManager.GetRolesAsync(user)).FirstOrDefault();
            var token = GenerateJwtToken(user, role);
            // return Ok("Login-Successful");
            return Ok(new { Token = token });
        }


        private string GenerateJwtToken(User user, string roles)
        {
            var jwtSettings = config.GetSection("JWT");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, roles)
            };

            // Set a default expiration in minutes if "AccessTokenExpiration" is missing or not a valid numeric value.
            if (!double.TryParse(jwtSettings["AccessTokenExpiration"], out double accessTokenExpirationMinutes))
            {
                accessTokenExpirationMinutes = 30; // Default expiration of 30 minutes.
            }

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(accessTokenExpirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    



        //POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto model, string role)
        {

            var registerResult = await register.RegisterUserAsync(model, ModelState, role);

            if (!registerResult)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(new
                {
                    Message = "user registration successful"
                });
            }


        }






    }
}
