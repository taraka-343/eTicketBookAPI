using eBooksAPI.Exceptions;
using eBooksAPI.Models;
using eBooksAPI.Services;
using eBooksAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private publisherService _service;
        private readonly ILogger<PublisherController> _logger;
        customReturn<object> cr = new customReturn<object>();
        public PublisherController(publisherService service, ILogger<PublisherController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpPost("add-new-publisher")]
        public IActionResult addPublisher([FromBody] publisherVM publisher)
        {
            try
            {
                _logger.LogInformation("Add Publishers() is called ...");
                if (_service.stringStarts(publisher.publisherName))
                {
                    throw new UserCreationException("Publisher Name cannot starts with digit");
                }
                if (_service.alreadyExists(publisher.publisherName))
                {
                    throw new UserCreationException("Publisher Name Already Exists");
                }
                else
                {
                    var data = _service.addThePublisher(publisher);
                    return Created(nameof(addPublisher), data);
                }
            }
            catch(UserCreationException ex)
            {
                return BadRequest(new { statusCode = 400, Message = ex.Message });
            }
            catch(Exception ex)
            {
                return StatusCode(500,"Interna; SERVER Error..");
            }
        }
        [HttpGet("Get-Pubisher-with-books/{id}")]
        public IActionResult getPublisherWithBooks(int id)
        {
            try
            {
                var result = _service.getPublisherWithBooks(id);
                if (result == null)
                {
                    throw new UserCreationException($"User id :{id} is not exist..");
                }
                return Ok(result);
            }
            catch(UserCreationException ex)
            {
                return NotFound(new { statusCode = 400, Message = ex.Message });
            }
            catch(Exception ex)
            {
                return StatusCode(500, "INTERNAL SERVER error..");
            }
           
        }
        [HttpGet("Get-All-Publishers")]
        public IActionResult getAllPublishers(string orderBy,string filterName,int pageNumber)
        {
            _logger.LogInformation("getAllPublishers() is called ...");
            var result=_service.getAllPublishers(orderBy, filterName, pageNumber);
            return Ok(result);
        }

    }
}
