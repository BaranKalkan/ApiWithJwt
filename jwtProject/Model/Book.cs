using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwtProject.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TotalPage { get; set; }
    }
}
