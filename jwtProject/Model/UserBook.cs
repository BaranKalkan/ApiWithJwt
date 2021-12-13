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
        public int CurrentPage { get; set; }
        public Book book { get; set; }
        
        
        //First Creation CTOR (thisCtor might change) 
        public UserBook(Book book)
        {
            this.Id = 1; //will change the id (test purposes)
            this.CurrentPage = 0;
            this.book = book;
        }
    }
}
