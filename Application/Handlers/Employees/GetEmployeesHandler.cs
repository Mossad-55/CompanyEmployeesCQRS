using Application.Queries.Employees;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using MediatR;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Application.Handlers.Employees;

internal sealed class GetEmployeesHandler : IRequestHandler<GetEmployeesQuery, (IEnumerable<EmployeeDto>, MetaData)>
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public GetEmployeesHandler(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<(IEnumerable<EmployeeDto>, MetaData)> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        var company = await _repository.Company.GetCompanyAsync(request.CompanyId, request.CompTrackChanges);
        if(company is null)
            throw new CompanyNotFoundException(request.CompanyId);

        var employeesWithMetaData = await _repository.Employee.GetAllEmployeesAsync(request.CompanyId, request.EmployeeParameters, request.EmpTrackChanges);

        var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesWithMetaData);

        return (employeesDto, employeesWithMetaData.MetaData);
    }
}
