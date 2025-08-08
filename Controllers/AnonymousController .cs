using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using RUM.DTOs;
using RUM.Interfaces;
using RUM.Models;

namespace RUM.Controllers
{
    [Route("Anonymous")]
    [EnableRateLimiting("fixed")]
    public class AnonymousController : Controller
    {
        private readonly IUserRepo _userRepo;
        private readonly IMessageRepo _messageRepo;
        private readonly ILogger<AnonymousController> _logger;

        public AnonymousController(IUserRepo userRepo, IMessageRepo messageRepo, ILogger<AnonymousController> logger)
        {
            _userRepo = userRepo;
            _messageRepo = messageRepo;
            _logger = logger;
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [HttpGet("Inbox")]
        public async Task<IActionResult> Inbox()
        {
            var (userId, fname, lname) = UserInfo();

            var publicLink = $"{DomainName()}/Anonymous/Send/{fname}{lname}{userId}";

            var messages = await _messageRepo.GetMessagesForUser(userId);

            _logger.LogInformation("User {UserId} opened inbox. Public link: {Link}", userId, publicLink);

            ViewBag.PublicLink = publicLink;
            ViewBag.Messages = messages!;

            return View();
        }

        [HttpGet("Send/{userName}")]
        public async Task<IActionResult> Send([FromRoute] string userName)
        {
            _logger.LogInformation("Anonymous send page requested for username: {UserName}", userName);

            if (string.IsNullOrWhiteSpace(userName))
            {
                _logger.LogWarning("Send failed: username was empty or whitespace.");
                return NotFound();
            }

            var match = Regex.Match(userName, @"^(?<name>[a-zA-Z]+)(?<id>\d+)$");
            if (!match.Success)
            {
                _logger.LogWarning("Send failed: username format invalid: {UserName}", userName);
                return NotFound();
            }

            int userId = int.Parse(match.Groups["id"].Value);
            var dbUser = await _userRepo.Find(userId);

            if (dbUser == null)
            {
                _logger.LogWarning("Send failed: No user found with ID: {UserId}", userId);
                return NotFound();
            }

            _logger.LogInformation("Anonymous message form opened for user {UserId} ({FullName})", dbUser.Id, $"{dbUser.Fname} {dbUser.Lname}");

            ViewBag.UserId = dbUser.Id;
            ViewBag.UserName = $"{dbUser.Fname} {dbUser.Lname}";
            return View();
        }

        [HttpPost("PostMsg")]
        public async Task<IActionResult> PostMsg([FromForm] AnonymousMessageDTO dTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("PostMsg failed: Invalid model state: {@Dto}", dTO);
                return BadRequest();
            }

            var dbUser = await _userRepo.Find(dTO.ReceiverId);
            if (dbUser == null)
            {
                _logger.LogWarning("PostMsg failed: No user found with ID: {UserId}", dTO.ReceiverId);
                return NotFound();
            }

            var username = $"{dbUser.Fname}{dbUser.Lname}{dbUser.Id}";
            TempData["ReceiverUsername"] = username;

            var msg = new Message
            {
                UserId = dTO.ReceiverId,
                Content = dTO.Content,
                CreatedAt = DateTime.UtcNow
            };

            await _messageRepo.Creat(msg);

            _logger.LogInformation("Anonymous message posted to user {UserId} at {Time}", msg.UserId, msg.CreatedAt);

            return RedirectToAction("MessageSent");
        }

        [HttpGet("MessageSent")]
        public IActionResult MessageSent()
        {
            _logger.LogInformation("Anonymous message sent confirmation page displayed.");
            return View();
        }

        private (int userId, string fname, string lname) UserInfo()
        {
            var userId = int.Parse(User.FindFirst("UserId")!.Value);
            var fname = User.FindFirst("Fname")?.Value ?? string.Empty;
            var lname = User.FindFirst("Lname")?.Value ?? string.Empty;

            return (userId, fname, lname);
        }

        private string DomainName()
        {
            var scheme = HttpContext.Request.Scheme;
            var host = HttpContext.Request.Host.Value;
            return $"{scheme}://{host}";
        }
    }
}
