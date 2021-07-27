using AutoMapper;
using HrProgram.Authorization;
using HrProgram.Entities;
using HrProgram.Exceptions;
using HrProgram.Models;
using HrProgram.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;

namespace HrProgram.Services
{
    public class VacationService : IVacationService
    {
        private readonly HrProgramDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public VacationService(HrProgramDbContext dbContext, 
                               IMapper mapper, 
                               IAuthorizationService authorizationService,
                               IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public IEnumerable<VacationDto> GetAll(int userId)
        {
            var vacations = _dbContext
                .Vacations
                .Where(v => v.UserId == userId);

            if (vacations is null)
                throw new NotFoundException("Vacation not found");

            var vacation = vacations
                .FirstOrDefault();

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, vacation, new ResourceOperationRequirement(ResourceOperation.GetAll)).Result;

            if (!authorizationResult.Succeeded)
                throw new ForbidException("Access forbiden");

            var vacationsDto = _mapper.Map<List<VacationDto>>(vacations);

            return vacationsDto;
        }

        public VacationDto GetById(int vacationId)
        {
            var vacation = _dbContext
                .Vacations
                .Where(v => v.UserId == _userContextService.GetUserId)
                .FirstOrDefault(v => v.Id == vacationId);

            if (vacation is null)
                throw new NotFoundException("Vacation not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, vacation, new ResourceOperationRequirement(ResourceOperation.GetById)).Result;

            if (!authorizationResult.Succeeded)
                throw new ForbidException("Access forbiden");

            var vacationDto = _mapper.Map<VacationDto>(vacation);

            return vacationDto;
        }

        public int Create(CreateAndUpdateVacationDto dto)
        {
            var vacation = _mapper.Map<Vacation>(dto);
            vacation.UserId = _userContextService.GetUserId;

            _dbContext.Vacations.Add(vacation);
            _dbContext.SaveChanges();

            return vacation.Id;
        }

        public void Delete(int id)
        {
            var vacation = _dbContext
                .Vacations
                .FirstOrDefault(v => v.Id == id);

            if (vacation is null)
                throw new NotFoundException("Vacation not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, vacation, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
                throw new ForbidException("Access forbiden");

            _dbContext.Remove(vacation);
            _dbContext.SaveChanges();
        }

        public void Update(int id, CreateAndUpdateVacationDto dto)
        {
            var vacation = _dbContext
                .Vacations
                .FirstOrDefault(v => v.Id == id);

            if (vacation is null)
                throw new NotFoundException("Vacation not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, vacation, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
                throw new ForbidException("Access forbiden");

            vacation.DayOfStart = dto.DayOfStart;
            vacation.DayOfEnd = dto.DayOfEnd;

            _dbContext.SaveChanges();
        }

        public void Accept(int id, AcceptVacationDto dto)
        {
            var vacation = _dbContext
                .Vacations
                .FirstOrDefault(v => v.Id == id);

            if (vacation is null)
                throw new NotFoundException("Vacation not found");

            vacation.IsAccepted = dto.IsAccepted;

            _dbContext.SaveChanges();
        }
    }
}
