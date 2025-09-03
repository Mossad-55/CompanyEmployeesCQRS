using Application.Queries.Companies;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.RequestFeatures;

namespace CompanyEmployeesCQRS.Presentation.Controllers;

[Route("api/companies")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly ISender _sender;

    public CompaniesController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetCompanies([FromQuery] CompanyParameters companyParameters)
    {
        var companies = await _sender.Send(new GetCompaniesQuery(companyParameters, false));

        return Ok(companies);
    }

    [HttpGet("{id:guid}", Name = "CompanyById")]
    public async Task<IActionResult> GetCompany(Guid id)
    {
        var company = await _sender.Send(new GetCompanyQuery(id, false));

        return Ok(company);
    }
}
