using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace APITemplate.Controllers;

public abstract class BaseController: ControllerBase
{
    protected readonly ILogger Logger;
    
    protected BaseController(ILogger logger)
    {
        Logger = logger;
    }
}

