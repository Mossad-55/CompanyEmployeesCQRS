using MediatR;
using Shared.DataTransferObjects;

namespace Application.Commands.Companies;

public sealed record UpdateCompanyCommand(Guid Id, CompanyForUpdateDto Company, bool TrackChanges) : IRequest<Unit>;
