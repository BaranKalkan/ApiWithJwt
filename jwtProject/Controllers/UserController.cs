using jwtProject.Data;
using jwtProject.Model;
using jwtProject.Model.DTOs.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace jwtProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : Controller
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly ApiDbContext _apiDbContext;

        public UserController(
            UserManager<ApiUser> userManager,
            ApiDbContext apiDbContext)
        {
            _userManager = userManager;
            _apiDbContext = apiDbContext;
        }

        [HttpGet]
        [Route("UserBooks")]
        public async Task<IActionResult> UserBooks()
        {
            var userIdentity = (System.Security.Claims.ClaimsIdentity)User.Identity;
            var userId = userIdentity.FindFirst("Id");
            var user = await _userManager.FindByIdAsync(userId.ToString());

            // if (bookOnDb == null) return BadRequest(new GeneralResponse { Errors = new List<string> { "Book doesn't exist" }, Success = false });

            return new OkObjectResult(user.Books); ;

        }

    
    }
}
