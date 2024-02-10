using eBooksAPI.Services;
using eBooksAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private authorService _service;
        public AuthorController(authorService service)
        {
            _service = service;
        }
        [HttpPost("add-new-author")]
        public IActionResult addAuthor([FromBody] AuthorVM book)
        {
            _service.addTheAuthor(book);
            return Ok();
        }
        [HttpGet("get-author-with-books/{id}")]
        public IActionResult getAuthorWithBook(int id)
        {
            var result=_service.getAuthorWithBookname(id);
            return Ok(result);
        }
    }
}
