using HiveX.Net.Mediator.Contracts.Behaviors;
using HiveX.Net.Mediator.Contracts.Entities;
using HiveX.Net.Mediator.Contracts.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace HiveX.Net.Mediator.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Registers the Mediator implementation, request handlers, and pipeline behaviors
        /// from the specified assemblies into the IServiceCollection for dependency injection.
        /// </summary>
        /// <param name="services">The IServiceCollection to register services into.</param>
        /// <param name="assemblies">Assemblies to scan for handler and behavior implementations.</param>
        /// <returns>The updated IServiceCollection instance.</returns>

        public static IServiceCollection AddHiveXNetMediator(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddSingleton<IMediator, Implementations.Mediator>();

            var handlerType = typeof(IRequestHandler<,>);
            foreach (var assembly in assemblies)
            {
                var handlers = assembly.GetTypes()
                    .Where(t => !t.IsAbstract && !t.IsInterface)
                    .SelectMany(t => t.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerType)
                    .Select(i => new { Handler = t, Interface = i }));

                foreach (var handler in handlers)
                {
                    services.AddScoped(handler.Interface, handler.Handler);
                }
            }

            var behaviorType = typeof(IPipelineBehavior<,>);
            foreach (var assembly in assemblies)
            {
                var behaviors = assembly.GetTypes()
                    .Where(t => !t.IsAbstract && !t.IsInterface)
                    .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == behaviorType));

                foreach (var behavior in behaviors)
                {
                    foreach 
                    (
                        var @interface in behavior.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == behaviorType)
                    )
                    {
                        services.AddScoped(@interface, behavior);
                    }
                }
            }

            return services;
        }

        
        
    }
}
