using MediatR;
using Shared.DataTransferObjects;

namespace Application.Commands.Employees;

public sealed record UpdateEmployeeCommand(Guid CompanyId, Guid Id, EmployeeForUpdateDto Employee, bool CompTrackChanges, bool EmpTrackChanges) : IRequest;
