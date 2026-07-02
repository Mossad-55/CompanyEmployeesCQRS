using Application.Commands.Employees;
using Contracts;
using Entities.Exceptions;
using MediatR;

namespace Application.Handlers.Employees;

internal sealed class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand>
{
    private readonly IRepositoryManager _repository;

    public DeleteEmployeeHandler(IRepositoryManager repository) => _repository = repository;

    public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var company = await _repository.Company.GetCompanyAsync(request.CompanyId, request.CompTrackChanges);
        if (company is null)
            throw new CompanyNotFoundException(request.CompanyId);

        var employeeEntity = await _repository.Employee.GetEmployeeAsync(request.CompanyId, request.Id, request.EmpTrackChanges);
        if (employeeEntity is null)
            throw new EmployeeNotFoundException(request.Id);

        _repository.Employee.DeleteEmployee(employeeEntity);

        await _repository.SaveAsync();
    }
}
