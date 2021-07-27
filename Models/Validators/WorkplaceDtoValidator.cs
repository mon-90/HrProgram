using FluentValidation;

namespace HrProgram.Models.Validators
{
    public class WorkplaceDtoValidator : AbstractValidator<WorkplaceDto>
    {
        public WorkplaceDtoValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty();
        }
    }
}
