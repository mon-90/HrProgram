using HrProgram.Models;
using System.Collections.Generic;

namespace HrProgram.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetAll();
        UserDto GetById(int id);
        void UpdateData(int id, UpdateDataUserDto dto);
    }
}
