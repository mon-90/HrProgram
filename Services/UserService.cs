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
    public class UserService : IUserService
    {
        private readonly HrProgramDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(HrProgramDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<UserDto> GetAll()
        {
            var users = _dbContext
                .Users
                .Include(u => u.Address)
                .ThenInclude(a => a.ContactPerson)
                .ToList();

            var usersDto = _mapper.Map<List<UserDto>>(users);

            return usersDto;
        }

        public UserDto GetById(int id)
        {
            var user = _dbContext
                .Users
                .Include(u => u.Address)
                .ThenInclude(a => a.ContactPerson)
                .FirstOrDefault(u => u.Id == id);

            if (user is null)
                throw new NotFoundException("User not found");

            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public void UpdateData(int id, UpdateDataUserDto dto)
        {
            var user = _dbContext
                .Users
                .Include(u => u.Address)
                .ThenInclude(a => a.ContactPerson)
                .FirstOrDefault(u => u.Id == id);

            if (user is null)
                throw new NotFoundException("User not found");

            UpdateEntity(user, dto);

            _dbContext.SaveChanges();
        }

        private void UpdateEntity(User entity, UpdateDataUserDto dto)
        {
            foreach (var prop in dto.GetType().GetProperties().Where(x => x.GetValue(dto) is not null))
            {
                entity.GetType().GetProperty(prop.Name).SetValue(entity, prop.GetValue(dto));
            }
        }
    }
}
