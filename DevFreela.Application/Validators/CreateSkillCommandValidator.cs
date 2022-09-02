using DevFreela.Application.Commands.CreateSkill;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class CreateSkillCommandValidator : AbstractValidator<CreateSkillCommand>
    {
        public CreateSkillCommandValidator()
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