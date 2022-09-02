using DevFreela.Application.Commands.UpdateSkill;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class UpdateSkillCommandValidator : AbstractValidator<UpdateSkillCommand>
    {
        public UpdateSkillCommandValidator()
        {
            RuleFor(s => s.Description)
                        .MaximumLength(255)
                        .WithMessage("A descrição da Skill não pode ultrapassar 255 caracteres.")
                        .NotEmpty()
                        .WithMessage("Descrição da Skill não pode ser vazia.")
                        .NotNull()
                        .WithMessage("Descrição da Skill não pode ser nula.");     
        }
    }
}