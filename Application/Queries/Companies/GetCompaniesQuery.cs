using MediatR;
using Shared.DataTransferObjects;

namespace Application.Queries.Companies;

public sealed record GetCompaniesQuery(bool trackChanges) : IRequest<IEnumerable<CompanyDto>>;
