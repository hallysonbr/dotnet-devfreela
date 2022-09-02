using DevFreela.Application.Commands.CreateSkill;
using DevFreela.Application.Commands.UpdateSkill;
using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Application.Queries.GetSkillById;
using DevFreela.Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _skillService;
        private readonly IMediator _mediator;

        public SkillsController(ISkillService skillService, IMediator mediator)
        {
            _skillService = skillService;
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "freelancer")]
        public async Task<IActionResult> Get()
        {
            //var skills = _skillService.GetAll();

            var query = new GetAllSkillsQuery();
            var skills = await _mediator.Send(query);
            return Ok(skills);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "freelancer")]
        public async Task<IActionResult> GetById(int id)
        {
            var skillQuery = new GetSkillByIdQuery(id);

            var skill = await _mediator.Send(skillQuery);
            if(skill is null) return NotFound();

            return Ok(skill);
        }

        [HttpPost]
        [Authorize(Roles = "freelancer")]
        public async Task<IActionResult> Post([FromBody] CreateSkillCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = id }, command);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "freelancer")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateSkillCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "freelancer")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }
    }
}
