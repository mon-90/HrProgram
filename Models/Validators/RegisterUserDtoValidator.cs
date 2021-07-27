using FluentValidation;
using System.Linq;

namespace HrProgram.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(HrProgramDbContext dbContext)
        {
            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();

            RuleFor(x => x.Login)
                .MaximumLength(30);

            RuleFor(x => x.Login)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(u => u.Login == value);
                    if (emailInUse)
                        context.AddFailure("Login", "That login is taken");
                });

            RuleFor(x => x.Password)
                .MinimumLength(6)
                .MaximumLength(20);

            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password);

            RuleFor(x => x.IdNumber)
                .NotEmpty();

            RuleFor(x => x.DateOfBirth)
                .NotEmpty();

            RuleFor(x => x.DateOfEmployment)
                .NotEmpty();

            RuleFor(x => x.RoleId)
                .NotEmpty();

            RuleFor(x => x.City)
                .NotEmpty();

            RuleFor(x => x.Street)
                .NotEmpty();

            RuleFor(x => x.HouseNumber)
                .NotEmpty();

            RuleFor(x => x.PostalCode)
                .NotEmpty();

            RuleFor(x => x.ContactPersonFirstName)
                .NotEmpty();

            RuleFor(x => x.ContactPersonLastName)
                .NotEmpty();
        }
    }
}
