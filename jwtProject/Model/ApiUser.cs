using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace jwtProject.Model
{
    public class ApiUser : IdentityUser
    {
        public List<UserBook> Books { get; set; }
        public List<UserBook> FavouriteBooks { get; set; }

    }
}
