using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwtProject.Model
{
    public class GutendexRootResponse
    {
        public class Author
        {
            public string name { get; set; }
        }


        public class Result
        {
            public int id { get; set; }
            public string title { get; set; }
            public List<Author> authors { get; set; }
            public List<object> translators { get; set; }
            public List<string> subjects { get; set; }
        }

        public class Root
        {
            public int count { get; set; }
            public List<Result> results { get; set; }
        }
    }
}
