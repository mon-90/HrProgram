using FluentValidation;

namespace HrProgram.Models.Validators
{
    public class EditPasswordUserDtoValidator : AbstractValidator<EditPasswordUserDto>
    {
        public EditPasswordUserDtoValidator()
        {
            RuleFor(x => x.NewPassword)
                .MinimumLength(6)
                .MaximumLength(20);

            RuleFor(x => x.ConfirmNewPassword)
                .Equal(e => e.NewPassword);
        }
    }
}
