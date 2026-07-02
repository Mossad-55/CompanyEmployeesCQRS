using Application.Queries.Employees;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using MediatR;
using Shared.DataTransferObjects;

namespace Application.Handlers.Employees;

internal sealed class GetEmployeeHandler : IRequestHandler<GetEmployeeQuery, EmployeeDto>
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public GetEmployeeHandler(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    async Task<EmployeeDto> IRequestHandler<GetEmployeeQuery, EmployeeDto>.Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        var company = await _repository.Company.GetCompanyAsync(request.CompanyId, request.CompTrackChanges);
        if (company is null)
            throw new CompanyNotFoundException(request.CompanyId);

        var employeeEntity = await _repository.Employee.GetEmployeeAsync(request.CompanyId, request.Id, request.EmpTrackChanges);
        if (employeeEntity is null)
            throw new EmployeeNotFoundException(request.Id);

        var employeeDto = _mapper.Map<EmployeeDto>(employeeEntity);

        return employeeDto;
    }
}
