using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBooksAPI.Models
{
    public class Author
    {
        public int id { get; set; }
        public string authorName { get; set; }
        //navigation properties
        public List<book_author> Book_Author { get; set; }
    }
}
