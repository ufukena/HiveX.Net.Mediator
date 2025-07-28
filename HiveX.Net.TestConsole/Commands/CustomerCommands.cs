using HiveX.Net.Mediator.Contracts.Markers;
using HiveX.Net.TestConsole.Models;


namespace HiveX.Net.TestConsole.Commands
{
    
    public class CustomerCreateCommand : IRequest<bool>
    {
        public CustomerModel CustomerModel { get; }
        public CustomerCreateCommand(CustomerModel customerModel) => CustomerModel = customerModel;

    }

    public class CustomerUpdateCommand : IRequest<bool>
    {
        public CustomerModel CustomerModel { get; }
        public CustomerUpdateCommand(CustomerModel customerModel) => CustomerModel = customerModel;

    }

    public class CustomerDeleteCommand : IRequest<bool>
    {
        public CustomerModel CustomerModel { get; }
        public CustomerDeleteCommand(CustomerModel customerModel) => CustomerModel = customerModel;

    }
}
