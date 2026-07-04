using MediatR;

namespace Application.Notifications.Employees;

public sealed record EmployeeDeletedNotification(Guid CompanyId, Guid Id, bool CompTrackChanges, bool EmpTrackChanges) : INotification;
