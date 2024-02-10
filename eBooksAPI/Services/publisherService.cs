using eBooksAPI.Data;
using eBooksAPI.Models;
using eBooksAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace eBooksAPI.Services
{

    public class publisherService
    {
        private appDbContext _context;
        public publisherService(appDbContext context)
        {
            _context = context;
        }
        public Publisher addThePublisher(publisherVM bk)
        {
                var _publisher = new Publisher()
                {
                    publisherName = bk.publisherName
                };
                _context.Publishers.Add(_publisher);
                _context.SaveChanges();
            return _publisher;
        }
        public PublisherWithBooks getPublisherWithBooks(int id)
        {
            return _context.Publishers.Where(m => m.Id == id).Select(m => new PublisherWithBooks()
            {
                publisherName = m.publisherName,
                bookNames = m.books.Select(n => n.Title).ToList()
            }).FirstOrDefault();
        }
        public List<Publisher> getAllPublishers(string orderBy,string filterName,int? pageNumber)
        {
            var _publisher = _context.Publishers.OrderBy(n => n.publisherName).ToList();
            if (!string.IsNullOrEmpty(orderBy))
            {
                switch (orderBy)
                {
                    case "DESC":
                        _publisher = _publisher.OrderByDescending(n => n.publisherName).ToList();
                        break;
                    default:
                        _publisher = _publisher.OrderBy(n => n.publisherName).ToList();
                        break;
                }
            }
            if (!string.IsNullOrEmpty(filterName))
            {
                _publisher = _publisher.Where(n => n.publisherName.Contains(filterName,StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(pageNumber.ToString()))
            {
                int pageSize = 5;
                _publisher = _publisher.Skip((pageNumber?? - 1) * pageSize).Take(pageSize).ToList();
            }
            return _publisher;
        }
        public bool stringStarts(string name)
        {
            return !Regex.IsMatch(name, "^[^0-9].*");
        }
        public bool alreadyExists(string data)
        {
            var result= _context.Publishers.Where(n => n.publisherName == data).FirstOrDefault();
            if (result.publisherName==null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


    }
}
