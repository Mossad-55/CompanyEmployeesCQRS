namespace Entities.Exceptions;

public sealed class UserRoleNotFoundException : NotFoundException
{
    public UserRoleNotFoundException(string message) 
        : base($"The provided role: {message} can't be found")
    {
    }
}
