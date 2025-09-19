using Application.Commands.Companies;
using Application.Queries.Companies;
using CompanyEmployeesCQRS.Presentation.ActionFilters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;
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

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDto dto)
    {
        var company = await _sender.Send(new CreateCompanyCommand(dto));

        return CreatedAtRoute("CompanyById", new { id = company.Id }, company);
    }

    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] CompanyForUpdateDto dto)
    {
        await _sender.Send(new UpdateCompanyCommand(id, dto, true));

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCompany(Guid id)
    {
        await _sender.Send(new DeleteCompanyCommand(id, false));

        return NoContent();
    }
}
