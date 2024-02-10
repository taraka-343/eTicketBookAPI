using eBooksAPI.Models;
using eBooksAPI.Services;
using eBooksAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace eBooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private bookService _service;
        public BookController(bookService service)
        {
            _service = service;
        }
        customReturn<Object> cr = new customReturn<Object>();
        [HttpPost("add-new-book")]
        public IActionResult addBook([FromBody] BookVM book)
        {
            _service.addTheBook(book);
            var customData = new { message = "User Created Successfully", value = 200 };
            var response = new customReturn<Object>
            {
                statusCode = 204,
                Message = "Created",
                Data = customData
            };
            return Created(nameof(addBook), response);
        }
        [HttpGet("get-all-books")]
        public IActionResult getAllBooks()
        {
            var result=_service.getAllBooks();
            return Ok(result);
        }
        [HttpGet("get-bookby-id/{id}")]
        public IActionResult getBookById(int id)
        {
            var result = _service.getBookById(id);
            if (result!=null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete("Delete-book/{id}")]
        public IActionResult delete(int id)
        {
            _service.deleteBook(id);
            return Ok();
        }
        [HttpGet("get-with-publisher-name/{id}")]
        public IActionResult getWithPublisherName(int id)
        {
            var result = _service.getBookByIdWithPublisherName(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
