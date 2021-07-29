using HrProgram.Models;
using System.Collections.Generic;

namespace HrProgram.Services.Interfaces
{
    public interface IVacationService
    {
        IEnumerable<VacationDto> GetAll(int userId);
        VacationDto GetById(int vacationId, int userId);
        int Create(CreateAndUpdateVacationDto dto);
        void Delete(int id);
        void Update(int id, CreateAndUpdateVacationDto dto);
        void Accept(int id, AcceptVacationDto dto);
    }
}
