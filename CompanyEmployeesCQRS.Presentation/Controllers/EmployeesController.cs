using Application.Commands.Employees;
using Application.Notifications.Employees;
using Application.Queries.Employees;
using CompanyEmployeesCQRS.Presentation.ActionFilters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Text.Json;

namespace CompanyEmployeesCQRS.Presentation.Controllers;

[Route("api/companies/{companyId}/employees")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IPublisher _publisher;

    public EmployeesController(ISender sender, IPublisher publisher)
    {
        _sender = sender;
        _publisher = publisher;
    }

    [HttpGet(Name = "GetEmployeeForCompany")]
    public async Task<IActionResult> GetEmployeesForCompany(Guid companyId, [FromQuery] EmployeeParameters employeeParameters)
    {
        var employeesWithMetaData = await _sender.Send(new GetEmployeesQuery(companyId, employeeParameters, false, false));

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(employeesWithMetaData.MetaData));

        return Ok(employeesWithMetaData.Employees);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEmployeeForCompany(Guid companyId, Guid id)
    {
        var employee = await _sender.Send(new GetEmployeeQuery(companyId, id, false, false));

        return Ok(employee);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDto employeeForCreationDto)
    {
        var employeeToReturn = await _sender.Send(new CreateEmployeeCommand(companyId, employeeForCreationDto, false, false));

        return CreatedAtRoute("GetEmployeeForCompany", new { companyId, id = employeeToReturn.Id }, employeeToReturn);
    }

    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateEmployeeForCompany(Guid companyId, Guid id, [FromBody] EmployeeForUpdateDto employeeForUpdateDto)
    {
        await _sender.Send(new UpdateEmployeeCommand(companyId, id, employeeForUpdateDto, false, true));

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid id)
    {
        await _publisher.Publish(new EmployeeDeletedNotification(companyId, id, false, false));

        return NoContent();
    }
}
