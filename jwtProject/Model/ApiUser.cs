using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace jwtProject.Model
{
    public class ApiUser : IdentityUser
    {
        [ForeignKey("UsersBook")]
        public List<UserBook> Books { get; set; }
        [ForeignKey("UsersFavBook")]
        public List<UserBook> FavouriteBooks { get; set; }

        public ApiUser()
        {  
            Books = new List<UserBook>();
            FavouriteBooks = new List<UserBook>();
        }
    }
}
