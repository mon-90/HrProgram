using HrProgram.Models;

namespace HrProgram.Services.Interfaces
{
    public interface IAccountService
    {
        void RegisteUser(RegisterUserDto dto);
        string GenerateJwt(LoginUserDto dto);
        void EditPassword(EditPasswordUserDto dto);
    }
}
