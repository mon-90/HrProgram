using AutoMapper;
using HrProgram.Authorization;
using HrProgram.Entities;
using HrProgram.Exceptions;
using HrProgram.Models;
using HrProgram.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace HrProgram.Services
{
    public class AccountService : IAccountService
    {
        private readonly HrProgramDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public AccountService(HrProgramDbContext dbContext, 
                              IMapper mapper,
                              IPasswordHasher<User> passwordHasher, 
                              AuthenticationSettings authenticationSettings,
                              IAuthorizationService authorizationService,
                              IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public void RegisteUser(RegisterUserDto dto)
        {
            var user = _mapper.Map<User>(dto);

            var hashedPassword = _passwordHasher.HashPassword(user, dto.Password);

            user.PasswordHash = hashedPassword;

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public string GenerateJwt(LoginUserDto dto)
        {
            var user = _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Login == dto.Login.ToLower());

            if(user is null)
                throw new BadRequestException("Invalid login or password");

            if (user.Role.Name == "Inactive")
                throw new ForbidException("Inactive user");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if(result == PasswordVerificationResult.Failed)
                throw new BadRequestException("Invalid login or password");

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
                new Claim("DateOfBirth", user.DateOfBirth.ToString("yyyy-MM-dd"))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, 
                                             _authenticationSettings.JwtIssuer, 
                                             claims, 
                                             expires: expires,
                                             signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

        public void EditPassword(EditPasswordUserDto dto)
        {
            var user = _dbContext
               .Users
               .FirstOrDefault(u => u.Login == dto.Login.ToLower());

            if (user is null)
                throw new NotFoundException("User not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, user, new EditPasswordRequirement(PasswordEnum.EditPassword)).Result;

            if (!authorizationResult.Succeeded)
                throw new ForbidException("Access forbidden");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
                throw new BadRequestException("Wrong password");

            var hashedPassword = _passwordHasher.HashPassword(user, dto.NewPassword);

            user.PasswordHash = hashedPassword;

            _dbContext.SaveChanges();
        }
    }
}
