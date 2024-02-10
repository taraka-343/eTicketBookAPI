using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBooksAPI.ViewModels
{
    public class AuthorVM
    {
        public string authorName { get; set; }
    }
    public class AuthorVMWithBookNames
    {
        public string authorName { get; set; }
        public List<string> bookNames { get; set; }
    }
}
