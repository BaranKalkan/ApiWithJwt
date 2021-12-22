using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace jwtProject.Model
{
    public class UserBook
    {
        
        public int Id { get; set; }
        public string userid { get; set; }
        public int CurrentPage { get; set; }
        public Book book { get; set; }

        public UserBook()
        {
            
        }
    }
}
