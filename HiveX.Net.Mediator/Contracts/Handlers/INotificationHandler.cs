using HiveX.Net.Mediator.Contracts.Markers;

namespace HiveX.Net.Mediator.Contracts.Handlers
{
    /// <summary>
    /// Interface for a handler that processes a notification.
    /// </summary>
    /// <typeparam name="TNotification">The type of the notification.</typeparam>
    public interface INotificationHandler<TNotification> where TNotification : INotification
    {
        Task Handle(TNotification notification, CancellationToken cancellationToken);
    }

}
