using System.Threading;
using System.Threading.Tasks;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetSkillById
{
    public class GetSkillByIdQueryHandler : IRequestHandler<GetSkillByIdQuery, SkillViewModel>
    {
        private readonly ISkillRepository _skillRepository;

        public GetSkillByIdQueryHandler(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public async Task<SkillViewModel> Handle(GetSkillByIdQuery request, CancellationToken cancellationToken)
        {
            var skill = await _skillRepository.GetByIdAsync(request.Id);

            if(skill is null) return null;

            var skillViewModel = new SkillViewModel(skill.Id, skill.Description);

            return skillViewModel;
        }
    }
}