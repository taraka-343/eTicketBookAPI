using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBooksAPI.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        public string publisherName { get; set; }
        //navigation properties
        public List<book> books { get; set; }
    }
    public class PublisherWithBooks
    {
        public string publisherName { get; set; }
        public List<string> bookNames { get; set; }
    }
}
