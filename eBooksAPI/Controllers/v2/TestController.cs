using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBooksAPI.Controllers.v2
{
    //[ApiVersion("2.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("get-test-data")]
        public string getData()
        {
            return "Iam from test v2 controller";
        }
    }
}
