using MediatR;

namespace Application.Notifications.Companies;

public sealed record CompanyDeletedNotification(Guid Id, bool TrackChanges) : INotification;
