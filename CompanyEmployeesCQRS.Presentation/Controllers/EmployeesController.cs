using Application.Queries.Employees;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.RequestFeatures;
using System.Text.Json;

namespace CompanyEmployeesCQRS.Presentation.Controllers;

[Route("api/{companyId}/employees")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly ISender _sender;

    public EmployeesController(ISender sender) => _sender = sender;

    [HttpGet]
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
}
