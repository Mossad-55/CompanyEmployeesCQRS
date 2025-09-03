using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace CompanyEmployeesCQRS;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // ForCtorParam is used because we are using a record not a class.
        CreateMap<Company, CompanyDto>()
            .ForMember(c => c.FullAddress,
            opts => opts.MapFrom(x => (x.Address ?? "") + " " + (x.Country ?? "")));

        CreateMap<CompanyForCreationDto, Company>();
        CreateMap<CompanyForUpdateDto, Company>().ReverseMap();

        CreateMap<Employee, EmployeeDto>();
        CreateMap<EmployeeForCreationDto, Employee>();
        CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();

        CreateMap<UserForRegistrationDto, User>();
    }
}
