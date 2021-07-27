using FluentValidation;

namespace HrProgram.Models.Validators
{
    public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(x => x.Login)
               .MaximumLength(30);

            RuleFor(x => x.Password)
                .MinimumLength(6)
                .MaximumLength(20);
        }
    }
}
