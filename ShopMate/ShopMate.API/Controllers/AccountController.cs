using Microsoft.AspNetCore.Mvc;
using ShopMate.BLL.DTO.AccountDto;
using ShopMate.BLL.Service.Abstraction;

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
            return Ok($"Login Succeeded\nYour Token: {res.Token}");
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
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


        [HttpPost("SendConfirmEmailCode")]
        public async Task<IActionResult> SendConfirmEmailCode(string email)
        {
            var res = await _accountService.SendConfirmEmailCode(email);
            if (!res)
            {
                return BadRequest("Confirm not success");
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


        [HttpPost("SendResetPasswordLink")]
        public async Task<IActionResult> SendResetPasswordLink(string email)
        {
            var res = await _accountService.SendResetPasswordToken(email);
            if (!res)
            {
                return BadRequest("Email not sent successfully");
            }
            return Ok("Email sent successfully");
        }


        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
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




    }
}

