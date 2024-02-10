using eBooksAPI.Data;
using eBooksAPI.Models;
using eBooksAPI.ViewModels.authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eBooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly appDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthenticationController(UserManager<ApplicationUser> userManager,
                                        RoleManager<IdentityRole> roleManager,
                                        appDbContext context,
                                        IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;

        }
        [HttpPost("Register-NewUser")]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterVm payload)
        {
            
            var userExits = await _userManager.FindByEmailAsync(payload.UserEmail);
            if (userExits==null)
            {
                ApplicationUser appUser = new ApplicationUser()
                {
                    Email = payload.UserEmail,
                    UserName = payload.UserName,
                    SecurityStamp = Guid.NewGuid().ToString()

                };
                var result = await _userManager.CreateAsync(appUser, payload.password);
                if (!result.Succeeded)
                {
                    return BadRequest($"User {payload.UserEmail} could not created");
                }
                return Created(nameof(RegisterUser), $"User {payload.UserEmail} Created Sucessfully");
            }
            else
            {
                return BadRequest($"User {payload.UserEmail} already Exist");
            }
           
            
        }
        [HttpPost("Login-user")]
        public async Task<IActionResult> login([FromBody]loginVM payload)
        {
            var user = await _userManager.FindByEmailAsync(payload.UserEmail);
            if (user != null && await _userManager.CheckPasswordAsync(user,payload.password))
            {
                var tokenValue = generateJWTtoken(user);
                return Ok(tokenValue);
            }
            else
            {
                return Unauthorized();
            }
        }
        private authResponceVM generateJWTtoken(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenString = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires:DateTime.UtcNow.AddMinutes(1),
                signingCredentials: credentials
                );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(tokenString);
            var refreshToken = new refreshTokens()
            {
                UserId = user.Id,
                JwtId = tokenString.Id,
                IsRevoked = false,
                DateAdded = DateTime.UtcNow,
                DateExpires = DateTime.UtcNow.AddMonths(6),
                Token = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString()

            };
            _context.RefreshTokens.Add(refreshToken);
            _context.SaveChanges();
            var response = new authResponceVM()
            {
                Token = jwtToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = tokenString.ValidTo
            };
            return response;



        }
    }
}
