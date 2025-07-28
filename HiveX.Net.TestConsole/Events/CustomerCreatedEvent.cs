using HiveX.Net.Mediator.Contracts.Markers;
using HiveX.Net.TestConsole.Models;


namespace HiveX.Net.TestConsole.Events
{
    public class CustomerCreatedEvent : ITriggerEvent
    {
        public CustomerModel CustomerModel { get; }

        public CustomerCreatedEvent(CustomerModel customer)
        {
            CustomerModel = customer;
        }

    }


}
