using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;

namespace APITemplate.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResourcesController: BaseController
{
    public ResourcesController(ILogger logger) : base(logger)
    {}
    
    [HttpGet]
    public ActionResult Get()
    {
        var rng = new Random();
        var data = Enumerable.Range(1, 5).Select(index => new Resource
        {
            Id = Guid.NewGuid(),
            Date = DateTime.Now.AddDays(index)
        }).ToArray();
        
        Logger.Information("Returning data {@Data}", data);
        return Ok(data);
    }
}