using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APITemplate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourcesController: ControllerBase
    {
        private readonly ILogger<ResourcesController> _logger;

        public ResourcesController(ILogger<ResourcesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Resource>> Get()
        {
            var rng = new Random();
            var res = Enumerable.Range(1, 5).Select(index => new Resource
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now.AddDays(index)
            });

            return Ok(res);
        }
    }
}