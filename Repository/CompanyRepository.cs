using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository;

internal sealed class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
{
    public CompanyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<PagedList<Company>> GetAllCompaniesAsync(CompanyParameters companyParameters, bool trackChanges)
    {
        /* First solution for small amount of data.
         * 
         * var companies = await FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToListAsync();

        return PagedList<Company>.ToPagedList(companies, companyParameters.PageNumber, companyParameters.PageSize);*/

        // Second Solution for large amount of data ..
        var companies = await FindAll(trackChanges)
            .FilterCompanies(companyParameters.Country)
            .Search(companyParameters.SearchTerm)
            .Sort(companyParameters.OrderBy)
            .Skip((companyParameters.PageNumber - 1) * companyParameters.PageSize)
            .Take(companyParameters.PageSize)
            .ToListAsync();

        var totalCount = await FindAll(trackChanges)
            .CountAsync();

        return new PagedList<Company>(companies, totalCount, companyParameters.PageNumber, companyParameters.PageSize);
    }

    public async Task<Company> GetCompanyAsync(Guid id, bool trachChanges) =>
        await FindByCondition(c => c.Id == id, trachChanges)
        .SingleOrDefaultAsync();

    public void CreateCompany(Company company) => Create(company);

    public async Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
        await FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToListAsync();

    public void DeleteCompany(Company company) => Delete(company);
}
