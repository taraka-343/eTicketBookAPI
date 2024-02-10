using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBooksAPI.ViewModels
{
    public class BookVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string Genre { get; set; }
        public string CoverUrl { get; set; }
        public int publisherId { get; set; }
        public List<int> authorId { get; set; }
    }
    public class BookVMWithPublisherName
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string Genre { get; set; }
        public string CoverUrl { get; set; }
        public string publisherName { get; set; }
        public List<string> AutherNames { get; set; }
    }
}
