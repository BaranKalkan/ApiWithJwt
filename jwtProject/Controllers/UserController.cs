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

            if (BookList.Count == 0)
            {
                return BadRequest(new GeneralResponse()
                {
                    Errors = new List<string>()
                        {
                            "No Books on User"
                        },
                    Success = false,
                });
            }
            return Json(BookList); 
        }
 
        [HttpPost]
        [Route("Details/{bookId}/AddToUserBooks")]
        public async Task<IActionResult> AddToUserBooks(int bookId)
        {
            //Find User
            var userIdentity = (System.Security.Claims.ClaimsIdentity)User.Identity;
            var userId = userIdentity.FindFirst("Id");
            var user = await _userManager.FindByIdAsync(userId.Value);

            //Find book
            var book = _apiDbContext.AllBooks.FirstOrDefault(x => x.Id == bookId);
            if (book == null)
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

            var BookList = new List<UserBook>();
            await _apiDbContext.AllUserBooks.Include(x => x.book).ForEachAsync<UserBook>(x =>
            {
                user.Books.ForEach(y =>
                {
                    if (y.Id == x.Id) BookList.Add(x);
                });
            });

            var existBook = BookList.FirstOrDefault(x => x.book.Id == bookId);
            if (existBook != null)
            {
                return BadRequest(new GeneralResponse()
                {
                    Errors = new List<string>()
                        {
                            "Already added this book!"
                        },
                    Success = false,
                });
            }

            //Create UserBook
            var UBook = new UserBook
            {
                userid = userId.Value,
                book = book,
                CurrentPage = 0
            };

            //Add to DB
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

            //Adds all favourites to list
            var BookList = new List<UserBook>();
            await _apiDbContext.AllUserBooks.Include(x => x.book).ForEachAsync<UserBook>(x =>
            {
                user.FavouriteBooks.ForEach(y =>
                {
                    if (y.Id == x.Id) BookList.Add(x);
                });
            });

            //If favourites is empty returns error
            if(BookList.Count==0)
            {
                return BadRequest(new GeneralResponse()
                {
                    Errors = new List<string>()
                        {
                            "No Favourites"
                        },
                        Success = false,
                });
            }
            
            return Json(BookList);
        }

        [HttpPost]
        [Route("Details/{bookId}/AddToFavBooks")]
        public async Task<IActionResult> AddToFavourites(int bookId)
        {
            //Find User
            var userIdentity = (System.Security.Claims.ClaimsIdentity)User.Identity;
            var userId = userIdentity.FindFirst("Id");
            var user = await _userManager.FindByIdAsync(userId.Value);
            
            //Find book
            var book = _apiDbContext.AllBooks.FirstOrDefault(x => x.Id == bookId);
            //Error if book doesnt exist
            if (book == null)
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

            //Control if book is exists in favourites
            var BookList = new List<UserBook>();
            await _apiDbContext.AllUserBooks.Include(x => x.book).ForEachAsync<UserBook>(x =>
            {
                user.FavouriteBooks.ForEach(y =>
                {
                    if (y.Id == x.Id) BookList.Add(x);
                });
            });

            var existBook = BookList.FirstOrDefault(x => x.book.Id == bookId);
            if (existBook != null)
            {
                return BadRequest(new GeneralResponse()
                {
                    Errors = new List<string>()
                        {
                            "Already favourited this book!"
                        },
                    Success = false,
                });
            }
            else
            {
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
            
            //User can only delete his book!
            if(existItem.userid != userId.Value)
                return NotFound();

            _apiDbContext.AllUserBooks.Remove(existItem);
            await _apiDbContext.SaveChangesAsync();

            return Ok(existItem);
        }

        [HttpDelete]
        [Route("Favourites/{bookId}/RemoveFromFavourites")]
        public async Task<IActionResult> DeleteFavBook(int bookId)
        {
            //Find User
            var userIdentity = (System.Security.Claims.ClaimsIdentity)User.Identity;
            var userId = userIdentity.FindFirst("Id");
            var user = await _userManager.FindByIdAsync(userId.Value);

            //Finds All Favourites (Why TF)
            var BookList = new List<UserBook>();
            await _apiDbContext.AllUserBooks.Include(x => x.book).ForEachAsync<UserBook>(x =>
            {
                user.FavouriteBooks.ForEach(y =>
                {
                    if (y.Id == x.Id) BookList.Add(x);
                });
            });

            //Find book
            var book = _apiDbContext.AllBooks.FirstOrDefault(x => x.Id == bookId);
            //Error if book doesnt exist
            if (book == null)
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

            //ENTER BOOK ID
            var existItem = user.FavouriteBooks.FirstOrDefault(x => x.book.Id == bookId);
            //Control if book is favourited
            if (existItem == null)
                return NotFound();
            
            //DB actios
            _apiDbContext.Update(user);
            try
            {
                user.FavouriteBooks.Remove(existItem);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                await _apiDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return Ok(existItem);
        }
    }
}
