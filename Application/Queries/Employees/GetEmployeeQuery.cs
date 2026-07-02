using MediatR;
using Shared.DataTransferObjects;

namespace Application.Queries.Employees;

public sealed record GetEmployeeQuery(Guid CompanyId, Guid Id, bool CompTrackChanges, bool EmpTrackChanges) : IRequest<EmployeeDto>;
