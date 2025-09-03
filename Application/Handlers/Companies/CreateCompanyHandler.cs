using Application.Commands.Companies;
using AutoMapper;
using Contracts;
using Entities.Models;
using MediatR;
using Shared.DataTransferObjects;

namespace Application.Handlers.Companies;

internal sealed class CreateCompanyHandler : IRequestHandler<CreateCompanyCommand, CompanyDto>
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public CreateCompanyHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repository = repositoryManager;
        _mapper = mapper;
    }

    public async Task<CompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var companyEntity = _mapper.Map<Company>(request.Company);

        _repository.Company.CreateCompany(companyEntity);

        await _repository.SaveAsync();

        var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);

        return companyToReturn;
    }
}
