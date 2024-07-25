using Core.Models;
using InnovaHackathon.Dtos;
using InnovaHackathon.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InnovaHackathon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            var user = new User { UserName = userRegisterDto.UserName, Email = userRegisterDto.Email };
            var result = await _userManager.CreateAsync(user, userRegisterDto.Password);

            if (result.Succeeded)
            {
                return Ok(new { Result = "User registered successfully" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(userLoginDto.UserName, userLoginDto.Password, userLoginDto.RememberMe, false);

            if (result.Succeeded)
            {
                return Ok(new { Result = "User logged in successfully" });
            }

            if (result.IsLockedOut)
            {
                return Unauthorized(new { Result = "User account locked out" });
            }

            return Unauthorized(new { Result = "Invalid login attempt" });
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound(new { Result = "User not found" });
            }

            var userProfile = new UserProfileDto
            {
                UserName = user.UserName,
                Email = user.Email
                
            };

            return Ok(userProfile);
        }
    }

}
