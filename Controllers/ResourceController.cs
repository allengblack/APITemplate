using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MiddlewareExperiments.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResourcesController : ControllerBase
    {
        private readonly ILogger<ResourcesController> _logger;

        public ResourcesController(ILogger<ResourcesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public JSend<IEnumerable<Resource>> Get()
        {
            var rng = new Random();
            var res = Enumerable.Range(1, 5).Select(index => new Resource
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now.AddDays(index)
            })
            .ToArray();

            // _logger.LogInformation(1, JsonConvert.SerializeObject(new {
            //     Response = res
            // }));
            return new JSend<IEnumerable<Resource>>
            {
                Message = "All is in Order",
                Data = res
            };
        }
    }
}

public class JSend<T>
{
    public string Message { get; set; }
    public T Data { get; set; }
}