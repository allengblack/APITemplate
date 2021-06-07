using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APITemplate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourcesController: ControllerBase
    {
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