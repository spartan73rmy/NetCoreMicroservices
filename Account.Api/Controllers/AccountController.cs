using Account.Api.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Account.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[Action]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDTO register)
        {
            if (await userManager.Users.AnyAsync(x => x.UserName == register.UserName.ToLower()))
                return BadRequest("User already exist");

            var user = new IdentityUser
            {
                UserName = register.UserName.ToLower(),
                Email = register.Email
            };

            var result = await userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return new UserDto
            {
                UserName = user.UserName,
                Token = await GetToken(user)
            };
        }

        private async Task<string> GetToken(IdentityUser user)
        {
            var now = DateTime.UtcNow;
            var key = configuration.GetValue<string>("Identity:Key");

            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));

            var signinKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

            var roles = await userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = now.AddMinutes(20),
                SigningCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var encodedJwt = new JwtSecurityTokenHandler();

            var token = encodedJwt.CreateToken(tokenDesc);

            return encodedJwt.WriteToken(token);
        }
    }
}
