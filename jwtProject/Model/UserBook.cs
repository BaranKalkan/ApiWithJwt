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
        public Book book { get; set; }
        public int CurrentPage { get; set; }

    }
}
