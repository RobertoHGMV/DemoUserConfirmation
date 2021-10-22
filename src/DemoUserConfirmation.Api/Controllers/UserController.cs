using System.Collections.Generic;
using System.Threading.Tasks;
using DemoUserConfirmation.Api.Models;
using DemoUserConfirmation.Api.Services;
using DemoUserConfirmation.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DemoUserConfirmation.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class UserController : Controller
    {
        UserService _userService;
        UserManagerService _userManagerService;
        EmailService _emailService;
        private readonly ILogger<UserController> _logger;

        public UserController(UserService userService, UserManagerService userManagerService, EmailService emailService, ILogger<UserController> logger)
        {
            _userService = userService;
            _userManagerService = userManagerService;
            _emailService = emailService;
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Add([FromBody]RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User(model.Name, model.Email, model.Password);
                await _userManagerService.CreateAsync(user);
                var token = await _userManagerService.GenerateEmailConfirmationTokenAsync(user);

                var confirmationLink = Url.Action("ConfirmEmail", "User",
                    new { userId = user.Id, token = token }, Request.Scheme);

                //send email

                _logger.Log(LogLevel.Warning, confirmationLink);

                return Ok();
            }

            return BadRequest();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(token))
            {
                var user = await _userManagerService.FindByIdAsync(userId);

                if (await _userManagerService.ConfirmEmailAsync(user, token))
                    await _userService.AddAsync(user);

                return Ok();
            }

            return BadRequest();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManagerService.FindByEmailAsync(model.Email);

                if (user != null && user.EmailConfirmed)
                {
                    var token = _userManagerService.GeneratePasswordResetTokenAsync(user);
                    
                    var passwordResetLink = Url.Action("ResetPassword", "User",
                    new { email = user.Email, token = token }, Request.Scheme);

                    _logger.Log(LogLevel.Warning, passwordResetLink);

                    return Ok();
                }

                return BadRequest();
            }

            return BadRequest();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManagerService.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var token = _userManagerService.ResetPasswordAsync(user, model.PasswordResetToken, model.Password);

                    return Ok();
                }

                return BadRequest();
            }

            return BadRequest();
        }
    }
}