using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : Controller
    {
        private UserManager<AspNetUsers> _userManager;


        public UserProfileController(UserManager<AspNetUsers> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        //[Authorize]
        //GET: /api/UserProfile
        public async Task<Object> GetUserProfile()
        {
            string userID = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userID);

            return new
            {
                user.FullName,
                user.Email,
                user.UserName
            };
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("ForAdmin")]
        public string GetForAdmin()
        {
            return "Web method for Admin";
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("ForUser")]
        public string GetForUser()
        {
            return "Web method for User";
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        [Route("ForAdminAndUser")]
        public string GetForAdminAndUser()
        {
            return "Web method for Admin and User";
        }

    }
}