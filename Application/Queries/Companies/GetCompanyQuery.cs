using MediatR;
using Shared.DataTransferObjects;

namespace Application.Queries.Companies;

public sealed record GetCompanyQuery(Guid Id, bool TrackChanges) : IRequest<CompanyDto>;
