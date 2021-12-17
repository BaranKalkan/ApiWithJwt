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
            var user = await _userManager.FindByIdAsync(userId.Value);
            var BookList = new List<UserBook>();
            
            await _apiDbContext.AllUserBooks.Include(x=>x.book).ForEachAsync<UserBook>(x =>
            {
                if (x.userid == userId.Value) BookList.Add(x);
            });
            // kitap yoksa ağlamasın
            return Json(BookList); 
        }
 
        [HttpPost]
        [Route("Details/{bookId}/AddToBooks")]
        public async Task<IActionResult> AddToBooks(int bookId)
        {
            var userIdentity = (System.Security.Claims.ClaimsIdentity)User.Identity;
            var userId = userIdentity.FindFirst("Id");
            var user = await _userManager.FindByIdAsync(userId.Value);
            var book = _apiDbContext.AllBooks.FirstOrDefault(x => x.Id == bookId);

            var UBook = new UserBook
            {
                userid = userId.Value,
                book = book,
                CurrentPage = 0
            };

            user.Books.Add(UBook);
            _apiDbContext.Update(user);

            try
            {
                _apiDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return Json(UBook);
        }

        [HttpGet]
        [Route("UserFavBooks")]
        public async Task<IActionResult> UserFavBooks()
        {
            var userIdentity = (System.Security.Claims.ClaimsIdentity)User.Identity;
            var userId = userIdentity.FindFirst("Id");
            var user = await _userManager.FindByIdAsync(userId.Value);
            var BookList = new List<UserBook>();

            await _apiDbContext.AllUserBooks.Include(x => x.book).ForEachAsync<UserBook>(x =>
            {
                user.FavouriteBooks.ForEach(y =>
                {
                    if (y.Id == x.Id) BookList.Add(x);
                });
            });
            // kitap yoksa ağlamasın
            return Json(BookList);
        }

        [HttpPost]
        [Route("Details/{bookId}/AddToFavBooks")]
        public async Task<IActionResult> AddToFavourites(int bookId)
        {
            var userIdentity = (System.Security.Claims.ClaimsIdentity)User.Identity;
            var userId = userIdentity.FindFirst("Id");
            var user = await _userManager.FindByIdAsync(userId.Value);
            var book = _apiDbContext.AllBooks.FirstOrDefault(x => x.Id == bookId);

            var UBook = new UserBook
            {
                userid = userId.Value,
                book = book,
                CurrentPage = 0
            };

            user.FavouriteBooks.Add(UBook);
            _apiDbContext.Update(user);

            try
            {
                _apiDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return Json(UBook);
        }
    }
}
