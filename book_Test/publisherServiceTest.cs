using eBooksAPI.Data;
using eBooksAPI.Models;
using eBooksAPI.Services;
using eBooksAPI.ViewModels;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using eBooksAPI.Controllers;

namespace book_Test
{
    public class publisherServiceTest
    {
        private static DbContextOptions<appDbContext> DbContextOption = new DbContextOptionsBuilder<appDbContext>().UseInMemoryDatabase(databaseName: "BookDbTest").Options;
        appDbContext context;
        publisherService PublisheService;
        [OneTimeSetUp]
        public void Setup()
        {
            context = new appDbContext(DbContextOption);
            context.Database.EnsureCreated();
            seedData();
            PublisheService = new publisherService(context);
        }

        [Test]
        public void getAllPublishers_withoutSort_withoutSearch_withoutPageNumber()
        {
            var result=PublisheService.getAllPublishers("","",null);
            Assert.That(result.Count, Is.EqualTo(6));
        }
        [Test]
        public void getAllPublishers_withoutSort_withoutSearch_withPageNumber()
        {
            var result = PublisheService.getAllPublishers("", "", 1);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.FirstOrDefault().publisherName, Is.EqualTo("Publisher 6"));
        }
        [Test]
        public void getAllPublishers_withoutSort_withSearch_withoutPageNumber()
        {
            var result = PublisheService.getAllPublishers("", "6", null);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.FirstOrDefault().publisherName, Is.EqualTo("Publisher 6"));
        }
        [Test]
        public void getAllPublishers_withSort_withoutSearch_withoutPageNumber()
        {
            var result = PublisheService.getAllPublishers("DESC", "", null);
            //Assert.That(result.Count, Is.EqualTo(5));
            Assert.That(result.FirstOrDefault().publisherName, Is.EqualTo("Publisher 6"));
        }
        [Test]
        public void getAllPublishers_By_id_withResponse()
        {
            var result = PublisheService.getPublisherWithBooks(1);
            Assert.That(result.publisherName,Is.EqualTo("Publisher 1"));
            //Assert.That(result.bookNames, Is.EqualTo("Book 1 Title"));
        }
        [Test]
        public void getAllPublishers_By_id_withOutResponse()
        {
            var result = PublisheService.getPublisherWithBooks(12);
            Assert.That(result,Is.Null);
            //Assert.That(result.bookNames, Is.EqualTo("Book 1 Title"));
        }
        [Test]
        public void AddPublisher_WithException_Test()
        {
            var newPublisher = new publisherVM()
            {
                publisherName = "123 With Exception"
            };

            Assert.That(() => PublisheService.addThePublisher(newPublisher), Throws.Exception.TypeOf<UserCreationException>().With.Message.EqualTo("Publisher Name cannot starts with digit"));
        }



        [OneTimeTearDown]
        public void cleanUp()
        {
            context.Database.EnsureDeleted();
        }
        public void seedData()
        {
            var publishers = new List<Publisher>
            {
                    new Publisher() {
                        Id = 1,
                        publisherName = "Publisher 1"
                    },
                    new Publisher() {
                        Id = 2,
                        publisherName = "Publisher 2"
                    },
                    new Publisher() {
                        Id = 3,
                        publisherName = "Publisher 3"
                    },
                    new Publisher() {
                        Id = 4,
                        publisherName = "Publisher 4"
                    },
                    new Publisher() {
                        Id = 5,
                        publisherName = "Publisher 5"
                    },
                    new Publisher() {
                        Id = 6,
                        publisherName = "Publisher 6"
                    },
            };
            context.Publishers.AddRange(publishers);

            var authors = new List<Author>()
            {
                new Author()
                {
                    id = 1,
                    authorName = "Author 1"
                },
                new Author()
                {
                    id = 2,
                    authorName = "Author 2"
                }
            };
            context.Authors.AddRange(authors);


            var books = new List<book>()
            {
                new book()
                {
                    Id = 1,
                    Title = "Book 1 Title",
                    Description = "Book 1 Description",
                    IsRead = false,
                    Genre = "Genre",
                    CoverUrl = "https://...",
                    DateAdded = DateTime.Now.AddDays(-10),
                    publisherId = 1
                },
                new book()
                {
                    Id = 2,
                    Title = "Book 2 Title",
                    Description = "Book 2 Description",
                    IsRead = false,
                    Genre = "Genre",
                    CoverUrl = "https://...",
                    DateAdded = DateTime.Now.AddDays(-10),
                    publisherId = 1
                }
            };
            context.books.AddRange(books);

            var books_authors = new List<book_author>()
            {
                new book_author()
                {
                    id = 1,
                    bookId = 1,
                    authorId = 1
                },
                new book_author()
                {
                    id = 2,
                    bookId = 1,
                    authorId = 2
                },
                new book_author()
                {
                    id = 3,
                    bookId = 2,
                    authorId = 2
                },
            };
            context.Book_Authors.AddRange(books_authors);


            context.SaveChanges();
        }

    }
    //[Test]
    //public void Test1()
    //{
    //    Assert.Pass();
    //}
}
