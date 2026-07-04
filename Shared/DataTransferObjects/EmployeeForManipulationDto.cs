namespace Shared.DataTransferObjects;

public abstract record EmployeeForManipulationDto
{
    public string? Name { get; init; }
    public int Age { get; init; }
    public string? Position { get; init; }
}
