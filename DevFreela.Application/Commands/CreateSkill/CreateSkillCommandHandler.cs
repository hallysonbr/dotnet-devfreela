using System.Threading;
using System.Threading.Tasks;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.CreateSkill
{
    public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, int>
    {
        private readonly ISkillRepository _skillService;

        public CreateSkillCommandHandler(ISkillRepository skillService)
        {
            _skillService = skillService;
        }
        public async Task<int> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = new Skill(request.Description);
            await _skillService.AddAsync(skill);

            return skill.Id;
        }
    }
}