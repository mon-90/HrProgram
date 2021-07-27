using HrProgram.Models;
using HrProgram.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HrProgram.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        [Authorize(Roles = "Admin,Hr")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            _accountService.RegisteUser(dto);

            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginUserDto dto)
        {
            string token = _accountService.GenerateJwt(dto);

            return Ok(token);
        }

        [HttpPut("password")]
        [Authorize]
        public ActionResult UpdatePassword([FromBody] EditPasswordUserDto dto)
        {
            _accountService.EditPassword(dto);

            return Ok();
        }
    }
}
