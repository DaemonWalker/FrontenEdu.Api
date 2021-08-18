using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontenEdu.Api.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok("alive!");
        }
    }
}
