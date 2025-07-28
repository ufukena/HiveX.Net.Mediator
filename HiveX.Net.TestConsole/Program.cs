using HiveX.Net.Mediator.Contracts.Entities;
using HiveX.Net.Mediator.Implementations;
using HiveX.Net.TestConsole.Abstractions.Contracts.Helpers;
using HiveX.Net.TestConsole.Boostrapper.Configurator;
using HiveX.Net.TestConsole.Commands;
using HiveX.Net.TestConsole.Events;
using HiveX.Net.TestConsole.Models;
using HiveX.Net.TestConsole.Notifications;
using HiveX.Net.TestConsole.Queries;


// Configure and get the IMediator instance
ServiceConfigurator serviceConfigurator = new ServiceConfigurator();
IMediator _mediator = serviceConfigurator.ConfigureAndGetMediator();

///////////////////////////////////////////////////////////////////////////////////////////

var customerModel = new CustomerModel { 
    FullName = "Lebron James", Phone="0123456798", Address = "Istanbul", Email = "lebronjames@gmail.com"  
};

///////////////////////////////////////////////////////////////////////////////////////////


// Sending a command to create a customer
//await _mediator.Send(new CustomerCreateCommand(customerModel));


///////////////////////////////////////////////////////////////////////////////////////////


// Sending a command to update a customer
//await _mediator.Send(new CustomerUpdateCommand(customerModel));


///////////////////////////////////////////////////////////////////////////////////////////


// Sending a command to delete a customer  
/* 
 * NOTE : To test exception handling: An exception has been added to CustomerDeleteCommandHandler (in CustomerHandlers)
          to trigger LogExceptionBehavior (in Behaviors) via IPipelineBehavior.
*/
//await _mediator.Send(new CustomerDeleteCommand(customerModel));


///////////////////////////////////////////////////////////////////////////////////////////


// Sending a query to get a customer by ID
//var customer = await _mediator.Send(new CustomerGetQuery(1));


///////////////////////////////////////////////////////////////////////////////////////////


// Sending a query to get all customers
//var customers = await _mediator.Send(new CustomerGetAllQuery());


///////////////////////////////////////////////////////////////////////////////////////////


// Example usage of helper classes: You should use interfaces marked with IHelper.
//bool result = _mediator.GetHelper<ICommonWebHelper>().CheckInternetConnection();


///////////////////////////////////////////////////////////////////////////////////////////


// Publishing a notification to notify subscribers about the customer creation
//await _mediator.Publish(new CustomerCreatedNotification(customerModel));


///////////////////////////////////////////////////////////////////////////////////////////


// Triggering an event to notify subscribers about the customer creation

void OnCustomerCreated(CustomerCreatedEvent ev)
{    
    Console.WriteLine($"Customer inserted: {ev.CustomerModel.FullName}");

    // Optionally unsubscribe if you only want to handle this event once
    _mediator.Unsubscribe<CustomerCreatedEvent>(OnCustomerCreated);
}

// Subscribe to the CustomerCreatedEvent and bind it to the OnCustomerCreated handler
_mediator.Subscribe<CustomerCreatedEvent>(OnCustomerCreated);

// Trigger the event, which will notify all subscribed handlers
await _mediator.Trigger(new CustomerCreatedEvent(customerModel));


///////////////////////////////////////////////////////////////////////////////////////////






Console.ReadLine();
