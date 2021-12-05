using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace jwtProject.Model
{
    public class UserBook
    {
        //public int userid { get; set; }
        public int Id { get; set; }
        public string Password { get; set; } //Lazimsa
        
        public int CurrentPage { get; set; } //CurrentBookun Current pagesi 
        public Book[] book { get; set; }
        //public List<Book> book { get; set; } Array vs List
        public Book CurrentBook { get; set; } 
        
        public UserBook()
        {

        }

        public int getPagesLeft()
        {
            this.CurrentBook.TotalPage - this.CurrentPage;
        }
    }
}
