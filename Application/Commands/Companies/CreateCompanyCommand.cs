using MediatR;
using Shared.DataTransferObjects;

namespace Application.Commands.Companies;

public sealed record CreateCompanyCommand(CompanyForCreationDto Company) : IRequest<CompanyDto>;
