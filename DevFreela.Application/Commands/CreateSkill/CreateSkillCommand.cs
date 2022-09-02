using MediatR;

namespace DevFreela.Application.Commands.CreateSkill
{
    public class CreateSkillCommand : IRequest<int>
    {
        public string Description { get; set; }
    }
}