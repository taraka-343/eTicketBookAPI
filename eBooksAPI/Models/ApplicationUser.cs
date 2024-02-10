using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBooksAPI.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string custom { get; set; }
    }
}
