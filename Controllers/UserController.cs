using HrProgram.Models;
using HrProgram.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;

namespace HrProgram.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Hr")]
        public ActionResult<IEnumerable<UserDto>> GetAll()
        {
            var usersDto = _userService.GetAll();

            return Ok(usersDto);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Hr")]
        public ActionResult<UserDto> Get([FromRoute] int id)
        {
            var userDto = _userService.GetById(id);

            return Ok(userDto);
        }

        [HttpPut("{id}/data")]
        [Authorize(Roles = "Hr")]
        public ActionResult UpdateData([FromRoute] int id, [FromBody] UpdateDataUserDto dto)
        {
            _userService.UpdateData(id, dto);

            return Ok();
        }
    }
}
