using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwtProject.Model.DTOs.Responses
{
    public class GeneralResponse
    {
        public string Result { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
