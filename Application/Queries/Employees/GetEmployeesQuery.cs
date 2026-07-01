using MediatR;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Application.Queries.Employees;

public sealed record GetEmployeesQuery(Guid CompanyId, EmployeeParameters EmployeeParameters, bool CompTrackChanges, bool EmpTrackChanges) 
    : IRequest<(IEnumerable<EmployeeDto> Employees, MetaData MetaData)>;
