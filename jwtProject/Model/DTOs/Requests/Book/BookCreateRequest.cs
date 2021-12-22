using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace jwtProject.Model.DTOs.Requests
{
    public class BookCreateRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string URL { get; set; }
    }
}
