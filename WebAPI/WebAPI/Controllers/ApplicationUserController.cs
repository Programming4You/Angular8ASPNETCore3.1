using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Models;
using WebAPI.ViewModel;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : Controller
    {
        private UserManager<AspNetUsers> _userManager;
        private readonly ApplicationSettings _appSettings;

        public ApplicationUserController(UserManager<AspNetUsers> userManager, IOptions<ApplicationSettings> appSettings)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }


        [HttpPost]
        [Route("Register")]
        //POST: /api/ApplicationUser/Register
        public async Task<Object> PostApplicationUser(ApplicationUserViewModel model)
        {
            List<string> role = new List<string>();
            role.Add("User");
            model.Roles = role;

            var applicationUser = new AspNetUsers()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName,
                Discriminator = "ApplicationUser"
            };

            try
            {
                var result = await _userManager.CreateAsync(applicationUser, model.Password);
                await _userManager.AddToRolesAsync(applicationUser, model.Roles);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        [Route("Login")]
        //POST: /api/ApplicationUser/Login
        public async Task<IActionResult> Login(Login model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var role = await _userManager.GetRolesAsync(user);
                IdentityOptions _options = new IdentityOptions();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID", user.Id.ToString()),
                        new Claim(_options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return Ok(new { token });
            }
            else
            {
                return BadRequest(new { message = "Username or password is incorrect." });
            }
        }

    }
}