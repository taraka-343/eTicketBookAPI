using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eBooksAPI.Models
{
    public class refreshTokens
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public string JwtId { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateExpires { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
    }
}
