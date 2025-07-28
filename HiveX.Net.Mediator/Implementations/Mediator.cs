using HiveX.Net.Mediator.Contracts.Behaviors;
using HiveX.Net.Mediator.Contracts.Entities;
using HiveX.Net.Mediator.Contracts.Handlers;
using HiveX.Net.Mediator.Contracts.Markers;
using HiveX.Net.Mediator.Delegates;
using System.Reflection;


namespace HiveX.Net.Mediator.Implementations
{

    public class Mediator : IMediator
    {
        private readonly ServiceFactory _serviceFactory;
        private static readonly Dictionary<Type, List<Delegate>> _subscriptions = new();
        private Dictionary<Type, IHelper> _helpers = new();

        public Mediator(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
        }


        //Commads and Queries        
        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var requestType = request.GetType();
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));

            var handlerObj = _serviceFactory(handlerType)
                ?? throw new InvalidOperationException($"Handler not found: {handlerType.Name}");

            var method = handlerType.GetMethod("Handle", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (method == null)
                throw new InvalidOperationException($"No 'Handle' method found in '{handlerType.Name}'.");

            var pipelineType = typeof(IPipelineBehavior<,>).MakeGenericType(requestType, typeof(TResponse));
            var pipelinesObj = _serviceFactory(typeof(IEnumerable<>).MakeGenericType(pipelineType));
            var behaviors = pipelinesObj as IEnumerable<object> ?? Enumerable.Empty<object>();

            RequestHandler<TResponse> handlerDelegate = () =>
            {
                var task = (Task<TResponse>)method.Invoke(handlerObj, new object[] { request, cancellationToken })!;
                return task;
            };

            foreach (var behavior in behaviors.Reverse())
            {
                var behaviorType = typeof(IPipelineBehavior<,>).MakeGenericType(requestType, typeof(TResponse));
                var behaviorMethod = behaviorType.GetMethod("Handle", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                if (behaviorMethod == null)
                    throw new InvalidOperationException($"The type '{behaviorType.Name}' does not implement a 'Handle' method.");

                var next = handlerDelegate;

                handlerDelegate = () =>
                {
                    var task = (Task<TResponse>)behaviorMethod.Invoke(behavior, new object[] { request, cancellationToken, next })!;
                    return task;
                };
            }

            return await handlerDelegate();
        }



        //Notifications        
        public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification
        {
            if (notification == null)
                throw new ArgumentNullException(nameof(notification));

            var notificationType = notification.GetType();
            var handlerType = typeof(INotificationHandler<>).MakeGenericType(notificationType);

            var handlersObj = _serviceFactory(typeof(IEnumerable<>).MakeGenericType(handlerType));
            var handlers = handlersObj as IEnumerable<object> ?? Enumerable.Empty<object>();

            var handleMethod = handlerType.GetMethod("Handle", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (handleMethod == null)
                throw new InvalidOperationException($"The type '{handlerType.Name}' does not implement a 'Handle' method.");

            foreach (var handler in handlers)
            {
                var task = (Task)handleMethod.Invoke(handler, new object[] { notification, cancellationToken })!;
                await task;
            }
        }



        //Triggers (Events)
        public Task Trigger<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : ITriggerEvent
        {
            var eventType = typeof(TEvent);
            if (_subscriptions.TryGetValue(eventType, out var handlers))
            {                
                var handlersCopy = handlers.Cast<Action<TEvent>>().ToList();
                foreach (var handler in handlersCopy)
                {
                    handler(@event);
                }
            }
            return Task.CompletedTask;
        }

        public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : ITriggerEvent
        {
            var eventType = typeof(TEvent);
            if (!_subscriptions.ContainsKey(eventType))
                _subscriptions[eventType] = new List<Delegate>();

            _subscriptions[eventType].Add(handler);
        }

        public void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : ITriggerEvent
        {
            var eventType = typeof(TEvent);
            if (_subscriptions.TryGetValue(eventType, out var handlers))
            {
                handlers.Remove(handler);
                if (handlers.Count == 0)
                    _subscriptions.Remove(eventType);
            }
        }



        // Helpers
        public void AddHelpers(IEnumerable<IHelper> helpers)
        {
            foreach (var helper in helpers)
            {
                var interfaceType = helper.GetType()
                    .GetInterfaces()
                    .FirstOrDefault(i => typeof(IHelper).IsAssignableFrom(i) && i != typeof(IHelper));

                if (interfaceType != null)
                {
                    _helpers[interfaceType] = helper;
                }
            }
        }

        public TInterface GetHelper<TInterface>() where TInterface : class, IHelper
        {
            // Only interfaces are allowed.
            if (!typeof(TInterface).IsInterface)
                throw new InvalidOperationException($"Only interfaces are allowed, but '{typeof(TInterface).Name}' is a class.");

            if (_helpers.TryGetValue(typeof(TInterface), out var service))
            {
                return (TInterface)service;
            }

            throw new InvalidOperationException($"Helper not found: {typeof(TInterface).Name}");
        }



    }

}
