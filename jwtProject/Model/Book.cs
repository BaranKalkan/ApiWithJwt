using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwtProject.Model
{
    public class Book
    {
        public int Id { get; set; }
        public int TotalPage { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public System.Drawing.Image CoverImage { get; set; } //resim icin baska datatipi bakabiliriz

        public Book()
        {

        }
    }
}
