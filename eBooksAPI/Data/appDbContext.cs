using eBooksAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBooksAPI.Data
{
    public class appDbContext:IdentityDbContext<ApplicationUser>
    {
        public appDbContext(DbContextOptions<appDbContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<book_author>()
                            .HasOne(b => b.book)
                            .WithMany(ba => ba.Book_Author)
                            .HasForeignKey(bi => bi.bookId);
            modelBuilder.Entity<book_author>()
                            .HasOne(b => b.Author)
                            .WithMany(ba => ba.Book_Author)
                            .HasForeignKey(bi => bi.authorId);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<book> books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<book_author> Book_Authors { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<refreshTokens> RefreshTokens { get; set; }
    }
}
