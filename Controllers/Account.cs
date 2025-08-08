using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Logging;
using RUM.DTOs;
using RUM.Interfaces;
using RUM.Models;

namespace RUM.Controllers
{
    [Route("Account")]
    [EnableRateLimiting("fixed")]
    public class AccountController : Controller
    {
        private readonly IUserRepo _userRepo;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserRepo userRepo, ILogger<AccountController> logger)
        {
            _userRepo = userRepo;
            _logger = logger;
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            _logger.LogInformation("GET /Account/Create called");
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreatUserDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state on Create user: {@Model}", dto);
                return BadRequest();
            }

            var user = new User
            {
                Fname = dto.Fname,
                Lname = dto.Lname,
                Email = dto.Email,
                Password = dto.Password
            };

            await _userRepo.Creat(user);
            _logger.LogInformation("User created successfully: {@User}", new { user.Id, user.Email });

            await Auth.AuthManager.Auth(HttpContext, user);
            _logger.LogInformation("User authenticated after creation: {UserId}", user.Id);

            return Redirect("/Anonymous/Inbox");
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            _logger.LogInformation("GET /Account/Login called");
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDTO dTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state on Login: {@Model}", dTO);
                return BadRequest();
            }

            var dbUser = await _userRepo.Find(dTO.Email!);
            if (dbUser == null)
            {
                _logger.LogWarning("Login failed. User not found: {Email}", dTO.Email);
                return Unauthorized();
            }

            if (dbUser.Password != dTO.Password)
            {
                _logger.LogWarning("Login failed. Incorrect password for user: {Email}", dTO.Email);
                return Unauthorized();
            }

            await Auth.AuthManager.Auth(HttpContext, dbUser);
            _logger.LogInformation("User logged in successfully: {UserId}, {Email}", dbUser.Id, dbUser.Email);

            return Redirect("/Anonymous/Inbox");
        }
    }
}
