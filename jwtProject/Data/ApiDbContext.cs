using jwtProject.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace jwtProject.Data
{
    public class ApiDbContext : IdentityDbContext<ApiUser>
    {
        public virtual DbSet<Book> AllBooks { get; set; }
        public virtual DbSet<UserBook> AllUserBooks { get; set; }
        public virtual DbSet<FavBook> AllFavbooks { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
            this.Database.Migrate();
        }
    }
}
