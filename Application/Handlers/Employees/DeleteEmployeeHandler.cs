using Application.Notifications.Employees;
using Contracts;
using Entities.Exceptions;
using MediatR;

namespace Application.Handlers.Employees;

internal sealed class DeleteEmployeeHandler : INotificationHandler<EmployeeDeletedNotification>
{
    private readonly IRepositoryManager _repository;

    public DeleteEmployeeHandler(IRepositoryManager repository) => _repository = repository;

    public async Task Handle(EmployeeDeletedNotification notification, CancellationToken cancellationToken)
    {
        var company = await _repository.Company.GetCompanyAsync(notification.CompanyId, notification.CompTrackChanges);
        if (company is null)
            throw new CompanyNotFoundException(notification.CompanyId);

        var employeeEntity = await _repository.Employee.GetEmployeeAsync(notification.CompanyId, notification.Id, notification.EmpTrackChanges);
        if (employeeEntity is null)
            throw new EmployeeNotFoundException(notification.Id);

        _repository.Employee.DeleteEmployee(employeeEntity);

        await _repository.SaveAsync();
    }
}
