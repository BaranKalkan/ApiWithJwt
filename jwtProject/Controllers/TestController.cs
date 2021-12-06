using jwtProject.Data;
using jwtProject.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class TestController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly ApiDbContext _apiDbContext;

        public TestController(
            UserManager<ApiUser> userManager,
            ApiDbContext apiDbContext)
        {
            _userManager = userManager;
            _apiDbContext = apiDbContext;
        }

    
        [HttpGet]
        [Route("GetCurrentPage/{bookId}")]
        public async Task<IActionResult> GetCurrentPage(int id)
        {
            var user = (System.Security.Claims.ClaimsIdentity)User.Identity;
            var userId = user.FindFirst("Id");

            var count = await _apiDbContext.Users.FirstAsync(a => a.Id == userId.Value);
            var book = count.Books.First(x => x.book.Id == id).CurrentPage;

            return Ok(new List<string>
            {
               book.ToString()
            });
        }
    }
}
