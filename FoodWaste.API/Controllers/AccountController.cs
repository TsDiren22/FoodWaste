using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using FoodWaste.API.Models;

namespace FoodWaste.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userMgr;
        private readonly SignInManager<IdentityUser> _signInMgr;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<IdentityUser> userMgr,
            SignInManager<IdentityUser> signInMgr, IConfiguration configuration)
        {
            _userMgr = userMgr;
            _signInMgr = signInMgr;
            _configuration = configuration;
        }

        [HttpPost("/signin")]
        public async Task<IActionResult> SignIn([FromBody] AccountModel authenticationCredentials)
        {
            var user = await _userMgr.FindByEmailAsync(authenticationCredentials.Email);
            if (user != null)
            {
                if ((await _signInMgr.PasswordSignInAsync(user,
                    authenticationCredentials.Password, false, false)).Succeeded)
                {
                    var securityTokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = (await _signInMgr.CreateUserPrincipalAsync(user)).Identities.First(),
                        Expires = DateTime.Now.AddMinutes(int.Parse(_configuration["BearerTokens:ExpiryMinutes"])),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["BearerTokens:Key"])), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var handler = new JwtSecurityTokenHandler();
                    var securityToken = new JwtSecurityTokenHandler().CreateToken(securityTokenDescriptor);

                    return Ok(new { Succes = true, Token = handler.WriteToken(securityToken) });
                }
            }

            return BadRequest();
        }

        [HttpPost("/signout")]
        public async Task<IActionResult> SignOut()
        {
            await _signInMgr.SignOutAsync();
            return Ok();
        }
    }
}
