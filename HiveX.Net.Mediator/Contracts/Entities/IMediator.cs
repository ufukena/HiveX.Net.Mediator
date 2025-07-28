using HiveX.Net.Mediator.Contracts.Markers;


namespace HiveX.Net.Mediator.Contracts.Entities
{
    /// <summary>
    /// The mediator interface through which requests (command/query) are sent.
    /// </summary>
    public interface IMediator
    {

        /// <summary>
        /// Asynchronously sends the specified request to the appropriate handler and returns the result.
        /// </summary>
        /// <typeparam name="TResponse">The type of the result</typeparam>
        /// <param name="request">The request object</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The result of the operation</returns>
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);


        /// <summary>
        /// Asynchronously sends the specified notification to the appropriate handlers.
        /// </summary>
        /// <typeparam name="TNotification">The type of the notification</typeparam>
        /// <param name="notification">The notification object</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task</returns>
        Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification;



        /// <summary>
        /// Triggers the specified event and notifies all subscribers listening to that event.
        /// </summary>
        /// <typeparam name="TEvent">The type of the triggered event.</typeparam>
        /// <param name="event">The event object to be triggered.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>Asynchronous task.</returns>
        Task Trigger<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : ITriggerEvent;


        /// <summary>
        /// Subscribes a handler for the specified event type.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event to subscribe to.</typeparam>
        /// <param name="handler">The action to be invoked when the event is triggered.</param>
        void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : ITriggerEvent;

        /// <summary>
        /// Unsubscribes a previously subscribed handler for the specified event type.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event to unsubscribe from.</typeparam>
        /// <param name="handler">The handler to be removed.</param>
        void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : ITriggerEvent;


        /// <summary>
        /// Returns an instance of the specified helper type.
        /// </summary>
        /// <typeparam name="T">The type of the helper to return. Must implement the IHelper interface.</typeparam>
        /// <returns>An instance of the specified helper type.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the helper cannot be found or the type conversion fails.</exception>
        T GetHelper<T>() where T : class, IHelper;


        /// <summary>
        /// Registers externally provided helpers to the Mediator.
        /// </summary>
        /// <param name="services">The collection of helpers to be added. Each must implement the IHelper interface.</param>
        void AddHelpers(IEnumerable<IHelper> services);


    }

}
