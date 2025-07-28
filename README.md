# HiveX.Net.Mediator

**HiveX.Net.Mediator** is a lightweight mediator infrastructure for .NET Core applications that provides centralized handling for **Commands, Queries, Events, Notifications**, and **Helper classes**.

<br><br>

## Purpose

This library is designed to simplify the implementation of **CQRS (Command Query Responsibility Segregation)** and **Mediator-style message dispatching** in .NET applications with a clean and flexible structure.

<br><br>

## Supported Features

- ✅ Command Handlers  
- ✅ Query Handlers  
- ✅ Event Dispatching  
- ✅ Notification Handlers  
- ✅ Dynamic Helper Resolution (via interfaces marked with `IHelper`)

<br><br>

## What Makes HiveX.Net.Mediator Stand Out

- Enables **loose coupling** between layers in your architecture  
- Improves **testability** through separated handlers  
- Centralized event and exception handling for logging and diagnostics  
- `IHelper`-based resolution for modular and reusable helper classes

<br><br>

## How to use

```csharp
// Configure and get the IMediator instance
ServiceConfigurator serviceConfigurator = new ServiceConfigurator();
IMediator _mediator = serviceConfigurator.ConfigureAndGetMediator();

var customerModel = new CustomerModel { 
    FullName = "xxxx", Phone="0123456798", Address = "Istanbul", Email = "xxxx@yyy.com"  
};

// Sending a command to create a customer
await _mediator.Send(new CustomerCreateCommand(customerModel));

// Sending a command to update a customer
await _mediator.Send(new CustomerUpdateCommand(customerModel));

// Sending a command to delete a customer  
await _mediator.Send(new CustomerDeleteCommand(customerModel));

// Sending a query to get a customer by Id
var customer = await _mediator.Send(new CustomerGetQuery(1));

// Sending a query to get all customers
var customers = await _mediator.Send(new CustomerGetAllQuery());

// Example usage of helper classes: You should use interfaces marked with IHelper.
bool result = _mediator.GetHelper<ICommonWebHelper>().CheckInternetConnection();

// Publishing a notification to notify subscribers about the customer creation
await _mediator.Publish(new CustomerCreatedNotification(customerModel));

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

```
