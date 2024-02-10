using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBooksAPI.Models
{
    public class customReturn<T>
    {
        public int statusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
