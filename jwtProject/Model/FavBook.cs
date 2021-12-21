using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwtProject.Model
{
    public class FavBook
    {


        public int Id { get; set; }
        public string userid { get; set; }
        public Book book { get; set; }
        public string URL { get; set; }
        public FavBook()
        {

        }

        /*//First Creation CTOR (this Ctor might change) 
        public FavBook(Book book)
        {
            this.CurrentPage = 0;
            this.book = book;
        }*/
    }
}
