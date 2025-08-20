namespace Entities.Exceptions;

public class EmployeeNotFoundException : NotFoundException
{
	public EmployeeNotFoundException(Guid id) :
		base($"The employee with id: {id} doens't exist in the database.")
	{

	}
}
