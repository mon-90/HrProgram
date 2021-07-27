using AutoMapper;
using HrProgram.Entities;
using HrProgram.Models;

namespace HrProgram
{
    public class HrProgramMappingProfile : Profile
    {
        public HrProgramMappingProfile()
        {
            CreateMap<Workplace, WorkplaceDto>()
                .ReverseMap();

            CreateMap<User, UserDto>()
                .ForMember(m => m.Country, c => c.MapFrom(s => s.Address.Country))
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(m => m.HouseNumber, c => c.MapFrom(s => s.Address.HouseNumber))
                .ForMember(m => m.ApartmentNumber, c => c.MapFrom(s => s.Address.ApartmentNumber))
                .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.Address.PostalCode))
                .ForMember(m => m.PhoneNumber, c => c.MapFrom(s => s.Address.PhoneNumber))
                .ForMember(m => m.ContactPersonFirstName, c => c.MapFrom(s => s.Address.ContactPerson.FirstName))
                .ForMember(m => m.ContactPersonLastName, c => c.MapFrom(s => s.Address.ContactPerson.LastName))
                .ForMember(m => m.ContactPersonPhoneNumber, c => c.MapFrom(s => s.Address.ContactPerson.PhoneNumber));

            CreateMap<RegisterUserDto, User>()
                .ForMember(r => r.Address, c => c.MapFrom(dto => new Address()
                {
                    Country = dto.Country,
                    City = dto.City,
                    Street = dto.Street,
                    HouseNumber = dto.HouseNumber,
                    ApartmentNumber = dto.ApartmentNumber,
                    PostalCode = dto.PostalCode,
                    PhoneNumber = dto.PhoneNumber,
                    ContactPerson = new ContactPerson()
                    {
                        FirstName = dto.ContactPersonFirstName,
                        LastName = dto.ContactPersonLastName,
                        PhoneNumber = dto.ContactPersonPhoneNumber
                    }
                }));

            CreateMap<Vacation, VacationDto>();

            CreateMap<CreateAndUpdateVacationDto, Vacation>();
        }
    }
}
