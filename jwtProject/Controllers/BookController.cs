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
using System.Threading.Tasks;

namespace jwtProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BookController : Controller
    {
        private readonly ApiDbContext _apiDbContext;
        public BookController(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        // GET: BookController/Details/5
        [HttpGet]
        [Route("Details/{bookId}")]
        public IActionResult Details(int bookId)
        {
            var book = _apiDbContext.AllBooks.First(x => x.Id == bookId);
            if ( book == null)
            {
                return BadRequest(new RegistrationResponse()
                {
                    Errors = new List<string>()
                        {
                            "Book doesn't exist"
                        },
                    Success = false
                });
            }

            return Ok(new List<string>
            {
                book.Id.ToString(),
                book.Title,
                book.Description,
                book.TotalPage.ToString()
            });
        }

        // GET: BookController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("Create")]
        public IActionResult Create([FromBody] BookCreateRequest bookToCreate)
        {
            if(!ModelState.IsValid) return BadRequest(new GeneralResponse { Errors = new List<string> { "Invalid parameters" }, Success = false });

            var bookOnDb = _apiDbContext.AllBooks.FirstOrDefault(dbBook => dbBook.Id == bookToCreate.Id);

            if (bookOnDb != null) return BadRequest(new GeneralResponse { Errors = new List<string> { "Book with the same id already exist"}, Success = false });

            var book = _apiDbContext.AllBooks.Add(new Model.Book()
            {
                Id = bookToCreate.Id,
                Title = bookToCreate.Title,
                Description = bookToCreate.Description,
                TotalPage = bookToCreate.TotalPage
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
            

            return Ok();         
        }


        // GET: BookController/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("Edit")]
        public ActionResult Edit([FromBody] BookEditRequest newBookInfo)
        {
            if (!ModelState.IsValid) return BadRequest(new GeneralResponse { Errors = new List<string> { "Invalid parameters" }, Success = false });

            var bookOnDb = _apiDbContext.AllBooks.FirstOrDefault(dbBook => dbBook.Id == newBookInfo.Id);

            if (bookOnDb == null) return BadRequest(new GeneralResponse { Errors = new List<string> { "Book doesn't exist" }, Success = false });

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


            return Ok();
        }


        // GET: BookController/Delete/5
        [HttpGet]
        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            return null;
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [Route("Delete/{id}/{collection}")]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            return null;
        }
    }
}
