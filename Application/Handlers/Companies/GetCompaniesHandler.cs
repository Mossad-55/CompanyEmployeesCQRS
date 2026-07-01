using Application.Queries.Companies;
using AutoMapper;
using Contracts;
using MediatR;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Application.Handlers.Companies;

internal sealed class GetCompaniesHandler : IRequestHandler<GetCompaniesQuery, (IEnumerable<CompanyDto>, MetaData)>
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public GetCompaniesHandler(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<(IEnumerable<CompanyDto>, MetaData)> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
    {
        var companiesWithMetaData = await _repository.Company.GetAllCompaniesAsync(request.companyParameters, request.trackChanges);

        var companiesDtos = _mapper.Map<IEnumerable<CompanyDto>>(companiesWithMetaData);

        return (companiesDtos, companiesWithMetaData.MetaData);
    }
}
