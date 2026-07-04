using Application.Notifications.Companies;
using Contracts;
using MediatR;

namespace Application.Handlers.Companies;

internal sealed class EmailForCompanyHandler : INotificationHandler<CompanyDeletedNotification>
{
    private readonly ILoggerManager _logger;

    public EmailForCompanyHandler(ILoggerManager logger) => _logger = logger;

    public async Task Handle(CompanyDeletedNotification notification, CancellationToken cancellationToken)
    {
        _logger.LogWarn($"Delete action for the company with id: {notification.Id} has occurred.");

        await Task.CompletedTask;
    }
}
