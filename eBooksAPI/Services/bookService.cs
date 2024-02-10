using eBooksAPI.Data;
using eBooksAPI.Models;
using eBooksAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBooksAPI.Services
{
    public class bookService
    {
        private appDbContext _context;
        public bookService(appDbContext context)
        {
            _context = context;
        }
        public void addTheBook(BookVM bk)
        {
            var _book = new book()
            {
                Title = bk.Title,
                Description = bk.Description,
                IsRead = bk.IsRead,
                DateRead = bk.IsRead ? bk.DateRead : null,
                Rate = bk.IsRead ? bk.Rate : null,
                Genre = bk.Genre,
                CoverUrl = bk.CoverUrl,
                DateAdded = DateTime.Now,
                publisherId=bk.publisherId
            };
            _context.books.Add(_book);
            _context.SaveChanges();
            foreach (var n in bk.authorId)
            {
                var _book_author = new book_author()
                {
                    bookId = _book.Id,
                    authorId = n
                };
                _context.Book_Authors.Add(_book_author);
                _context.SaveChanges();

            }
        }
        public List<book> getAllBooks()
        {
            var result = _context.books.ToList();
            return result;
        }
        public book getBookById(int bookid)
        {
            var result = _context.books.Where(n => n.Id == bookid).FirstOrDefault();
            return result;
        }
        public void deleteBook(int bookid)
        {
            var result = _context.books.Where(n => n.Id == bookid).FirstOrDefault();
            if (result != null)
            {
                _context.books.Remove(result);
                _context.SaveChanges();
            }
        }
        public BookVMWithPublisherName getBookByIdWithPublisherName(int bookid)
        {
            return _context.books.Where(n => n.Id == bookid).Select(Book => new BookVMWithPublisherName() {
                Title = Book.Title,
                Description = Book.Description,
                IsRead = Book.IsRead,
                DateRead = Book.DateRead,
                Rate = Book.Rate,
                Genre = Book.Genre,
                CoverUrl = Book.CoverUrl,
                publisherName = Book.Publisher.publisherName,
                AutherNames = Book.Book_Author.Select(m => m.Author.authorName).ToList()
            }).FirstOrDefault();
            
        }

    }
}
