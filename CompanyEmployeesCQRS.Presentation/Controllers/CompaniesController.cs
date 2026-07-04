using Application.Commands.Companies;
using Application.Notifications.Companies;
using Application.Queries.Companies;
using CompanyEmployeesCQRS.Presentation.ActionFilters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Text.Json;

namespace CompanyEmployeesCQRS.Presentation.Controllers;

[Route("api/companies")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IPublisher _publisher;

    public CompaniesController(ISender sender, IPublisher publisher)
    {
        _sender = sender;
        _publisher = publisher;
    }

    [HttpGet]
    public async Task<IActionResult> GetCompanies([FromQuery] CompanyParameters companyParameters)
    {
        var companiesWithMetaData = await _sender.Send(new GetCompaniesQuery(companyParameters, false));

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(companiesWithMetaData.MetaData));

        return Ok(companiesWithMetaData.Companies);
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
        await _publisher.Publish(new CompanyDeletedNotification(id, false));

        return NoContent();
    }
}
