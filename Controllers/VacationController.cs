using HrProgram.Models;
using HrProgram.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HrProgram.Controllers
{
    [ApiController]
    [Route("api/vacation")]
    public class VacationController : ControllerBase
    {
        private readonly IVacationService _vacationService;

        public VacationController(IVacationService vacationService)
        {
            _vacationService = vacationService;
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Hr,Employee")]
        public ActionResult<IEnumerable<VacationDto>> GetAll([FromRoute] int userId)
        {
            var vacationsDto = _vacationService.GetAll(userId);

            return Ok(vacationsDto);
        }

        [HttpGet("user/{userId}/{vacationId}")]
        [Authorize(Roles = "Hr,Employee")]
        public ActionResult<VacationDto> Get([FromRoute] int vacationId, [FromRoute] int userId)
        {
            var vacationDto = _vacationService.GetById(vacationId, userId);

            return Ok(vacationDto);
        }

        [HttpPost]
        [Authorize(Roles = "Hr,Employee")]
        public ActionResult Create([FromBody] CreateAndUpdateVacationDto dto)
        {
            var id = _vacationService.Create(dto);

            return Created($"/api/vacation/{id}", null);
        }

        [HttpDelete("{vacationId}")]
        [Authorize(Roles = "Hr,Employee")]
        public ActionResult Delete([FromRoute] int vacationId)
        {
            _vacationService.Delete(vacationId);

            return NoContent();
        }

        [HttpPut("update/{vacationId}")]
        [Authorize(Roles = "Hr,Employee")]
        public ActionResult Update([FromRoute] int vacationId, [FromBody] CreateAndUpdateVacationDto dto)
        {
            _vacationService.Update(vacationId, dto);

            return Ok();
        }

        [HttpPut("accept/{vacationId}")]
        [Authorize(Roles = "Hr")]
        public ActionResult Accept([FromRoute] int vacationId, [FromBody] AcceptVacationDto dto)
        {
            _vacationService.Accept(vacationId, dto);

            return Ok();
        }
    }
}
