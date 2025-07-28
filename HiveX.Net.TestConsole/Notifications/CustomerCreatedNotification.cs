using HiveX.Net.Mediator.Contracts.Markers;
using HiveX.Net.TestConsole.Models;


namespace HiveX.Net.TestConsole.Notifications
{
    public class CustomerCreatedNotification : INotification
    {
        public CustomerModel CustomerModel { get; }
        public CustomerCreatedNotification(CustomerModel customerModel) => CustomerModel = customerModel;

    }

}
