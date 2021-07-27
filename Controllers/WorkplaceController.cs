using HrProgram.Models;
using HrProgram.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HrProgram.Controllers
{
    [ApiController]
    [Route("api/workplace")]
    public class WorkplaceController : ControllerBase
    {
        private readonly IWorkplaceService _workplaceService;

        public WorkplaceController(IWorkplaceService workplaceService)
        {
            _workplaceService = workplaceService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Hr")]
        public ActionResult<IEnumerable<WorkplaceDto>> GetAll()
        {
            var workplacesDto = _workplaceService.GetAll();

            return Ok(workplacesDto);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Hr")]
        public ActionResult<WorkplaceDto> Get([FromRoute] int id)
        {
            var workplaceDto = _workplaceService.GetById(id);

            return Ok(workplaceDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([FromBody] WorkplaceDto dto)
        {
            var id = _workplaceService.Create(dto);

            return Created($"/api/workplace/{id}", null);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete([FromRoute] int id)
        {
            _workplaceService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Update([FromRoute] int id, [FromBody] WorkplaceDto dto)
        {          
            _workplaceService.Update(id, dto);

            return Ok();
        }
    }
}
