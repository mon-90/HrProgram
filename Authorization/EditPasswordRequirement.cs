using Microsoft.AspNetCore.Authorization;

namespace HrProgram.Authorization
{
    public enum PasswordEnum
    {
        EditPassword
    }
    public class EditPasswordRequirement : IAuthorizationRequirement
    {
        public EditPasswordRequirement(PasswordEnum passwordEnum)
        {
            PasswordEnum = passwordEnum;
        }

        public PasswordEnum PasswordEnum { get; }
    }
}
