using AutoMapper;
using HrProgram.Entities;
using HrProgram.Exceptions;
using HrProgram.Models;
using HrProgram.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HrProgram.Services
{
    public class WorkplaceService : IWorkplaceService
    {
        private readonly HrProgramDbContext _dbContext;
        private readonly IMapper _mapper;

        public WorkplaceService(HrProgramDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<WorkplaceDto> GetAll()
        {
            var workplaces = _dbContext
                .Workplaces
                .ToList();

            var workplacesDto = _mapper.Map<List<WorkplaceDto>>(workplaces);

            return workplacesDto;
        }

        public WorkplaceDto GetById(int id)
        {
            var workplace = _dbContext
                .Workplaces
                .FirstOrDefault(w => w.Id == id);

            if (workplace is null)
                throw new NotFoundException("Workplace not found");

            var workplaceDto = _mapper.Map<WorkplaceDto>(workplace);

            return workplaceDto;
        }

        public int Create(WorkplaceDto dto)
        {
            var workplace = _mapper.Map<Workplace>(dto);

            _dbContext.Workplaces.Add(workplace);
            _dbContext.SaveChanges();

            return workplace.Id;
        }

        public void Delete(int id)
        {
            var workplace = _dbContext
                .Workplaces
                .Include(w => w.Users)
                .FirstOrDefault(w => w.Id == id);

            if (workplace is null || workplace.Users.Any())
                throw new NotFoundException("Workplace not found");

            _dbContext.Workplaces.Remove(workplace);
            _dbContext.SaveChanges();
        }

        public void Update(int id, WorkplaceDto dto)
        {
            var workplace = _dbContext
                .Workplaces
                .FirstOrDefault(w => w.Id == id);

            if (workplace is null)
                throw new NotFoundException("Workplace not found");

            workplace.Name = dto.Name;

            _dbContext.SaveChanges();
        }
    }
}
