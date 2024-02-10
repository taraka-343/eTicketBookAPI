using eBooksAPI.Data;
using eBooksAPI.Models;
using eBooksAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBooksAPI.Services
{
    public class authorService
    {
        private appDbContext _context;
        public authorService(appDbContext context)
        {
            _context = context;
        }
        public void addTheAuthor(AuthorVM bk)
        {
            var _author = new Author()
            {
                authorName = bk.authorName
            };
            _context.Authors.Add(_author);
            _context.SaveChanges();
        }
        public AuthorVMWithBookNames getAuthorWithBookname(int authId)
        {
            var _author = _context.Authors.Where(a => a.id == authId).Select(m => new AuthorVMWithBookNames()
            {
                authorName = m.authorName,
                bookNames = m.Book_Author.Select(m => m.book.Title).ToList()
            }).FirstOrDefault();
            return _author;
        }

    }
}
