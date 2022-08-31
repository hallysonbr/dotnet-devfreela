using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
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
    }
}
