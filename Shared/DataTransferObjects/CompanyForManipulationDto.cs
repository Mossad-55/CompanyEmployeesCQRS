using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public abstract record CompanyForManipulationDto
{
    public string? Name { get; init; }
    public string? Address { get; init; }
    public string? Country { get; init; }
}
