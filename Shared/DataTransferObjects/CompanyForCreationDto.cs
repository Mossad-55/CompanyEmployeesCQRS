using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record CompanyForCreationDto : CompanyForManipulationDto
{ 
    public IEnumerable<EmployeeForCreationDto>? Employees { get; init; }
}
