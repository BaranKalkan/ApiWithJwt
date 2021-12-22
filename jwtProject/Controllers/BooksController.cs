using jwtProject.Data;
using jwtProject.Model.DTOs.Requests;
using jwtProject.Model.DTOs.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using jwtProject.Model;
using Microsoft.AspNetCore.Identity;
using System.Net.Http;
using static jwtProject.Model.GutendexRootResponse;
using Newtonsoft.Json;

namespace jwtProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ApplicationUser")]
    public class BookController : Controller
    {
        private readonly ApiDbContext _apiDbContext;
        private readonly UserManager<ApiUser> _userManager;
        public BookController(ApiDbContext apiDbContext, UserManager<ApiUser> userManager)
        {
            _userManager = userManager;
            _apiDbContext = apiDbContext;
        }

        // GET: Book/
        [AllowAnonymous] // Allows users without login to look book details
        [HttpGet]
        public IActionResult Books()
        {
            var books = _apiDbContext.AllBooks.ToList();
            if (books == null)
            {
                return BadRequest(new GeneralResponse()
                {
                    Errors = new List<string>()
                        {
                            "Books doesn't exist"
                        },
                    Success = false,
                });
            }

            return Json(books);
        }

        // GET: Book/5
        [HttpGet]
        [Route("{bookId}")]
        public async Task<IActionResult> Book(int bookId)
        {
            Book book;
            try
            {
                book = await FindBookAsync(_apiDbContext, bookId);
            }
            catch
            {
                return BadRequest(new GeneralResponse()
                {
                    Errors = new List<string>()
                        {
                            "Veritabanı güncellenemedi"
                        },
                    Success = false,
                });
            }

            return Json(book);
        }

        public static async Task<Book> FindBookAsync(ApiDbContext apiDbContext, int bookId)
        {
            var book = apiDbContext.AllBooks.FirstOrDefault(x => x.Id == bookId);

            if (book == null)
            {
                string url = string.Format($"https://gutendex.com/books?ids={bookId}");

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var jsonString = await client.GetStringAsync(url);
                Root deserialized = JsonConvert.DeserializeObject<Root>(jsonString);
                
                var result0 = deserialized.results[0];
                var createdBook = new Model.Book()
                {
                    Id = bookId,
                    URL = $"https://www.gutenberg.org/files/{bookId}/{bookId}-h/{bookId}-h.htm",
                    Authors = result0.authors[0].name,
                    Subjects = $"{result0.subjects[0]},",
                    Title = result0.title
                };

                apiDbContext.AllBooks.Add(createdBook);
             
                apiDbContext.SaveChanges();
               
                return createdBook;
            }

            return book;
        }

        [HttpDelete]
        [Route("Details/{userBookId}/DeleteFromUserBooks")]
        public async Task<IActionResult> DeleteUserBook(int userBookId)
        {
            var userIdentity = (System.Security.Claims.ClaimsIdentity)User.Identity;
            var userId = userIdentity.FindFirst("Id");
            var user = await _userManager.FindByIdAsync(userId.Value);
            
            var existItem = await _apiDbContext.AllUserBooks.FirstOrDefaultAsync(x => x.Id == userBookId);

            if (existItem == null)
                return NotFound();

            _apiDbContext.AllUserBooks.Remove(existItem);

            try
            {
                _apiDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return BadRequest(new GeneralResponse
                {
                    Errors = new List<string>
                    {
                        "Unable to save changes"
                    },
                    Success = false,
                });
            }

            return Ok(existItem);
        }

    }
}
