using MediatR;

namespace Application.Commands.Companies;

public sealed record DeleteCompanyCommand(Guid Id, bool TrackChanges) : IRequest<Unit>;
