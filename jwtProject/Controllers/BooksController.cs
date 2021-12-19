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
                            "Book doesn't exist"
                        },
                    Success = false,
                });
            }

            return Json(books);
        }

        // GET: Book/5
        [HttpGet]
        [Route("{bookId}")]
        public IActionResult Book(int bookId)
        {
            var book = _apiDbContext.AllBooks.FirstOrDefault(x => x.Id == bookId);
            if ( book == null)
            {
                return BadRequest(new GeneralResponse()
                {
                    Errors = new List<string>()
                        {
                            "Book doesn't exist"
                        },
                    Success = false,
                });
            }
        
            return Json(book);
        }

        // Post: Book/Create
        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] BookCreateRequest bookToCreate)
        {
            if (!ModelState.IsValid) return BadRequest(new GeneralResponse { Errors = new List<string> { "Invalid parameters" }, Success = false });

            var bookOnDb = _apiDbContext.AllBooks.Any(x => x.Id == bookToCreate.Id);

            if (bookOnDb) return BadRequest(new GeneralResponse { Errors = new List<string> { "Book with the same id already exist" }, Success = false });

            var book = new Model.Book()
            {
                Id = bookToCreate.Id,
                Title = bookToCreate.Title,
                Description = bookToCreate.Description,
                TotalPage = bookToCreate.TotalPage,
                Author = bookToCreate.Author
            };
            _apiDbContext.AllBooks.Add(book);

            try
            {
                _apiDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(new GeneralResponse
                {
                    Errors = new List<string>
                    {
                        "Unable to save changes -> ",
                        e.Message // destroy this line
                    },
                    Success = false,
                });
            }

            return Json(book);
        }

        [HttpDelete]
        [Route("Details/{bookId}/RemoveFromAllBooks")] 
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            //Finds book from AllBooks
            var existItem = await _apiDbContext.AllBooks.FirstOrDefaultAsync(x => x.Id == bookId);

            //null catch
            if (existItem == null)
                return NotFound();

            var existUserBook = await _apiDbContext.AllUserBooks.FirstOrDefaultAsync(x => x.book == existItem);
            //var existUserBooka = _apiDbContext.AllUserBooks.ForEachAsync;

            //Removes all same book from UserBooks
            await _apiDbContext.AllUserBooks.Include(x => x.book).ForEachAsync<UserBook>(x =>
            {
                if (x.book == existItem)
                    _apiDbContext.AllUserBooks.Remove(x);
            });
            //Removes from all books
            _apiDbContext.AllBooks.Remove(existItem);

            //Save changes on DB
            await _apiDbContext.SaveChangesAsync();

            //Sildiğin kitaba son bir kez dön bak istedim
            return Ok(existItem);
        }

        [HttpDelete]
        [Route("Details/{bookId}/DeleteFromUserBooks")]
        public async Task<IActionResult> DeleteUserBook(int bookId)
        {
            var userIdentity = (System.Security.Claims.ClaimsIdentity)User.Identity;
            var userId = userIdentity.FindFirst("Id");
            var user = await _userManager.FindByIdAsync(userId.Value);
            
            var existItem = await _apiDbContext.AllUserBooks.FirstOrDefaultAsync(x => x.Id == bookId);

            if (existItem == null)
                return NotFound();

            _apiDbContext.AllUserBooks.Remove(existItem);
            await _apiDbContext.SaveChangesAsync();

            return Ok(existItem);
        }

        // Put: Book/Edit
        [HttpPut]
        public ActionResult Edit([FromBody] BookEditRequest newBookInfo)
        {
            if (!ModelState.IsValid) return BadRequest();

            var bookOnDb = _apiDbContext.AllBooks.FirstOrDefault(dbBook => dbBook.Id == newBookInfo.Id);

            if (bookOnDb == null)
                return BadRequest(
                    new GeneralResponse
                    {
                        Errors = new List<string>
                        {
                            "Book doesnt exist"
                        },
                        Success = false
                    });

            _apiDbContext.Entry(bookOnDb).CurrentValues.SetValues(newBookInfo);

            try
            {
                _apiDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(new GeneralResponse
                {
                    Errors = new List<string>
                    {
                        "Unable to save changes -> ",
                        e.Message // destroy this line
                    },
                    Success = false,
                });
            }

            return Json(bookOnDb);
        }

        // DELETE: Book/5
        [HttpDelete]
        [Route("{bookId}")]
        public IActionResult Delete(int bookId)
        {
            return Json("Delete func is not implemented yet");
        }
    }
}
