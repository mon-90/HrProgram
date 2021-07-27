using HrProgram.Models;
using System.Collections.Generic;

namespace HrProgram.Services.Interfaces
{
    public interface IWorkplaceService
    {
        WorkplaceDto GetById(int id);
        IEnumerable<WorkplaceDto> GetAll();
        int Create(WorkplaceDto dto);
        void Delete(int id);
        void Update(int id, WorkplaceDto dto);
    }
}
