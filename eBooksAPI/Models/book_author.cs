using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBooksAPI.Models
{
    public class book_author
    {
        //join table for many to many relation b/w book and author
        public int id { get; set; }
        public int bookId { get; set; }
        public book book { get; set; }
        public int authorId { get; set; }
        public Author Author { get; set; }
    }
}
