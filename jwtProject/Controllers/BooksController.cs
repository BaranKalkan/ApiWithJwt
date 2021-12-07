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

namespace jwtProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BooksController : Controller
    {
        private readonly ApiDbContext _apiDbContext;
        public BooksController(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        // GET: Books/
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

        // GET: Books/5
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

        // Post: Books/
        [HttpPost]
        public IActionResult Create([FromBody] BookCreateRequest bookToCreate)
        {
            if (!ModelState.IsValid) return BadRequest(new GeneralResponse { Errors = new List<string> { "Invalid parameters" }, Success = false });

            var bookOnDb = _apiDbContext.AllBooks.Any(x => x.Id == bookToCreate.Id);

            if (bookOnDb) return BadRequest(new GeneralResponse { Errors = new List<string> { "Book with the same id already exist" }, Success = false });

            var book = _apiDbContext.AllBooks.Add(new Model.Book()
            {
                Id = bookToCreate.Id,
                Title = bookToCreate.Title,
                Description = bookToCreate.Description,
                TotalPage = bookToCreate.TotalPage,
                Author = bookToCreate.Author
            });

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

        // Put: Books/
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

        // DELETE: Books/5
        [HttpDelete]
        [Route("{bookId}")]
        public IActionResult Delete(int bookId)
        {
            return Json("Delete func is not implemented yet");
        }
    }
}
