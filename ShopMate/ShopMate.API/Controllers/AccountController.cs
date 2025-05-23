﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopMate.BLL.DTO.AccountDto;
using ShopMate.BLL.Service.Abstraction;
using System.Security.Claims;

namespace ShopMate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        #region Login,Register
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var res = await _accountService.Login(loginDto);
            if (!res.Succeeded)
            {
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError("", error);
                }
                return BadRequest(ModelState);
            }
            return Ok(res.Token);
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromForm] RegisterDto registerDto)
        {
            var res = await _accountService.Register(registerDto);
            if (!res.Succeeded)
            {
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError("", error);
                }
                return BadRequest(ModelState);
            }
            return Ok("Registration Succeeded");
        }
        #endregion


        #region Confirm email
        [HttpPost("SendConfirmEmailCode")]
        public async Task<IActionResult> SendConfirmEmailCode(string email)
        {
            var res = await _accountService.SendConfirmEmailCode(email);
            if (!res)
            {
                return BadRequest("Email didn't send");
            }
            return Ok("Email sent successfully");
        }


        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string email, string code)
        {
            var res = await _accountService.ConfirmEmail(email, code);
            if (!res)
            {
                return BadRequest("Invalid or expired code");
            }
            return Ok("Email confirmed successfully");
        }


        #endregion

        #region reset password

        [HttpPost("SendResetPasswordLink")]
        public async Task<IActionResult> SendResetPasswordLink(string email)
        {
            var res = await _accountService.SendResetPasswordToken(email);
            if (!res)
            {
                return BadRequest("Email didn't send");
            }
            return Ok("Email sent successfully");
        }


        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var res = await _accountService.ResetPassword(resetPasswordDto);
            if (!res.Succeeded)
            {
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError("", error);
                }
                return BadRequest(ModelState);
            }
            return Ok("Password reseted successfully");
        }


        #endregion

        #region Profile

        [HttpPut("Profile")]
        [Authorize]
        public async Task<ActionResult> Profile([FromForm] UpdateProfileDto profileDto)
        {
            var res = await _accountService.UpdateProfileAsync(profileDto);
            if (!res.Succeeded)
            {
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError("", error);
                }
                return BadRequest(ModelState);
            }
            return Ok(profileDto);
        }

        [HttpGet("Profile")]
        [Authorize]
        public async Task<ActionResult> Profile(string userId)
        {
            var profile = await _accountService.GetProfileAsync(userId);
            if (profile == null)
            {
                return NotFound();
            }
            return Ok(profile);
        }

        #endregion


        #region Delete Account

        [HttpDelete("{id}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Account(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest("User ID is required.");


            var res = await _accountService.DeleteAccount(id);
            if (!res.Succeeded)
            {
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError("", error);
                }
                return BadRequest(ModelState);
            }
            return Ok("Account deleted successfully");
        }
        #endregion

        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (String.IsNullOrWhiteSpace(userId))
            {
                return BadRequest("User not found");
            }

            var res = await _accountService.ChangePasswordAsync(userId, changePasswordDto);

            if (!res.Succeeded)
            {
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError("", error);
                }
                return BadRequest(ModelState);
            }

            return Ok("Password changed successfully");

        }

    }
}

