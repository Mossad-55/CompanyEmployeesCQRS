using Application.Notifications.Employees;
using Contracts;
using MediatR;

namespace Application.Handlers.Employees;

internal sealed class EmailForEmployeeHandler : INotificationHandler<EmployeeDeletedNotification>
{
    private readonly ILoggerManager _logger;

    public EmailForEmployeeHandler(ILoggerManager logger) => _logger = logger;

    public async Task Handle(EmployeeDeletedNotification notification, CancellationToken cancellationToken)
    {
        _logger.LogWarn($"Employee Deleted Notification: CompanyId: {notification.CompanyId}, EmployeeId: {notification.Id}.");

        await Task.CompletedTask;
    }
}
