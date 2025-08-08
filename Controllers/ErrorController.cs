using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
[EnableRateLimiting("fixed")]
public class ErrorController : Controller
{
    private readonly ILogger<ErrorController> _logger;

    public ErrorController(ILogger<ErrorController> logger)
    {
        _logger = logger;
    }

    [Route("Error/Status")]
     [EnableRateLimiting("fixed")]
    public IActionResult Status(int code)
    {
        var model = new ErrorViewModel
        {
            StatusCode = code,
            Title = "An error occurred",
            Messages = code switch
            {
                404 => new List<string> { "The page you requested was not found.", "Check the URL and try again." },
                403 => new List<string> { "You are not authorized to access this resource." },
                500 => new List<string> { "An internal server error occurred.", "Please try again later." },
                400 => new List<string> { "The request was invalid.", "Please check your data and try again." },
                503 => new List<string> { "Too Many Requests." },
                _ => new List<string> { "An unknown error occurred." }
            }
        };

        _logger.LogWarning("Status error occurred. Code: {StatusCode}, Messages: {@Messages}", model.StatusCode, model.Messages);

        return View("Error", model);
    }

    [Route("Error/Exception")]
    public IActionResult Exception()
    {
        var model = new ErrorViewModel
        {
            StatusCode = 500,
            Title = "Unexpected Error",
            Messages = new List<string>
            {
                "An unhandled exception occurred while processing your request.",
                "Please contact support if the issue persists."
            }
        };

        _logger.LogError("Unhandled exception triggered. Showing exception page with messages: {@Messages}", model.Messages);

        return View("Error", model);
    }
}
