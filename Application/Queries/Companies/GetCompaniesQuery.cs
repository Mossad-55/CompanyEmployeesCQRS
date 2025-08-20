using MediatR;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Application.Queries.Companies;

public sealed record GetCompaniesQuery(CompanyParameters companyParameters, bool trackChanges) : IRequest<IEnumerable<CompanyDto>>;
