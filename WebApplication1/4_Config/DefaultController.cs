using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4_Config
{
    [Route("[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        public DefaultController(IStarship starship)
        {
        }

        [HttpGet]
        public string Get()
        {
            return "";
        }
    }
}
