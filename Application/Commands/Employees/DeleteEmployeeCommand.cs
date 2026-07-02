using MediatR;

namespace Application.Commands.Employees;

public sealed record DeleteEmployeeCommand(Guid CompanyId, Guid Id, bool CompTrackChanges, bool EmpTrackChanges) : IRequest;
