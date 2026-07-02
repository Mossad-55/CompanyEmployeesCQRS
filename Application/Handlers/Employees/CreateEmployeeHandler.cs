using Application.Commands.Employees;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using MediatR;
using Shared.DataTransferObjects;

namespace Application.Handlers.Employees;

internal sealed class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, EmployeeDto>
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public CreateEmployeeHandler(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<EmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var company = await _repository.Company.GetCompanyAsync(request.CompanyId, request.CompTrackChanges);
        if (company is null)
            throw new CompanyNotFoundException(request.CompanyId);

        var employeeEntity = _mapper.Map<Employee>(request.Employee);

        _repository.Employee.CreateEmployeeForCompany(request.CompanyId, employeeEntity);

        await _repository.SaveAsync();

        var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

        return employeeToReturn;
    }
}
