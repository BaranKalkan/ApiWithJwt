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
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace jwtProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("MyPolicy")]
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
        [Route("CurrentPage")]
        public async Task<IActionResult> CurrentPage(int BookId)
        {
            //Find User
            var userIdentity = (System.Security.Claims.ClaimsIdentity)User.Identity;
            var userId = userIdentity.FindFirst("Id");
            var user = await _userManager.FindByIdAsync(userId.Value);

            var current = -1;
            await _apiDbContext.AllUserBooks.Include(x => x.book).ForEachAsync(x =>
            {
                if (x.userid == userId.Value)
                {
                    if (x.book.Id == BookId)
                    {
                         current = x.CurrentPage;
                    }
                }
            });

            return Ok(current);
        }

        [HttpPost]
        [Route("CurrentPage")]
        public async Task<IActionResult> CurrentPage(int BookId, int Current)
        {
            var userIdentity = (System.Security.Claims.ClaimsIdentity)User.Identity;
            var userId = userIdentity.FindFirst("Id");
            var user = await _userManager.FindByIdAsync(userId.Value);

            bool success = false;
            await _apiDbContext.AllUserBooks.Include(x => x.book).ForEachAsync(x =>
            {
                if (x.userid == userId.Value)
                {
                    if (x.book.Id == BookId)
                    {
                        x.CurrentPage=Current;
                        success = true;
                    }
                }
            });

            try
            {
                _apiDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return Ok(success);
        }
    }
}
