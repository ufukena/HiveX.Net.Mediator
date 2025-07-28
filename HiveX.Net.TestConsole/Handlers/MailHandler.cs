using HiveX.Net.Mediator.Contracts.Handlers;
using HiveX.Net.Mediator.Contracts.Markers;
using HiveX.Net.TestConsole.Notifications;


namespace HiveX.Net.TestConsole.Handlers
{
    public class MailHandler : INotificationHandler<CustomerCreatedNotification>        
    {
        public Task Handle(CustomerCreatedNotification notification, CancellationToken cancellationToken)
                           => HandleInternal(notification);

        // You can extend this class with additional Handle methods for other notifications, allowing centralized management of email delivery
        // Like this:

        #region Examples
        /*
            public Task Handle(ProductCreatedNotification notification, CancellationToken cancellationToken)
                    => HandleInternal(notification);

            public Task Handle(UserDeletedNotification notification, CancellationToken cancellationToken)
                    => HandleInternal(notification);
        */
        #endregion


        private Task HandleInternal(INotification notification)
        {
            if (notification is CustomerCreatedNotification customerNotification)
            {
                Console.WriteLine($"An email has been sent to the manager for {customerNotification.CustomerModel.FullName}.");
            }

            return Task.CompletedTask;
        }

    }
}
