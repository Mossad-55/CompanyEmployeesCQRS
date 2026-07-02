using MediatR;
using Shared.DataTransferObjects;

namespace Application.Commands.Employees;

public sealed record CreateEmployeeCommand(Guid CompanyId, EmployeeForCreationDto Employee, bool CompTrackChanges, bool EmpTrackChanges) : IRequest;
