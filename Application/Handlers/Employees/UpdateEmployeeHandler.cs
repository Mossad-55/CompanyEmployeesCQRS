using Application.Commands.Employees;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using MediatR;

namespace Application.Handlers.Employees;

internal sealed class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand>
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public UpdateEmployeeHandler(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var company = await _repository.Company.GetCompanyAsync(request.CompanyId, request.CompTrackChanges);
        if(company is null)
            throw new CompanyNotFoundException(request.CompanyId);

        var employeeEntity = await _repository.Employee.GetEmployeeAsync(request.CompanyId, request.Id, request.EmpTrackChanges);
        if(employeeEntity is null)
            throw new EmployeeNotFoundException(request.Id);

        _mapper.Map(request.Employee, employeeEntity);

        await _repository.SaveAsync();
    }
}
