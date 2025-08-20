namespace Entities.Exceptions;

public sealed class CompanyNotFoundException : NotFoundException
{
	public CompanyNotFoundException(Guid id) : 
		base ($"The company with id: {id} doesn't exist in the database.")
	{
	}
}
