using Autofac;
using HiveX.Net.Mediator.Contracts.Behaviors;
using HiveX.Net.Mediator.Contracts.Entities;
using HiveX.Net.Mediator.Contracts.Handlers;
using System.Reflection;


namespace HiveX.Net.Mediator.DependencyInjection
{
    public static class AutofacExtensions
    {
        /// <summary>
        /// Registers the Mediator implementation and related handlers (request handlers, notification handlers, pipeline behaviors)
        /// from the specified assemblies into the Autofac container builder.
        /// </summary>
        /// <param name="builder">The Autofac ContainerBuilder to register components into.</param>
        /// <param name="assemblies">Assemblies to scan for handler implementations.</param>
        /// <returns>The updated ContainerBuilder instance.</returns>
        public static ContainerBuilder AddHiveXNetMediator(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            builder.RegisterType<Implementations.Mediator>()
                   .As<IMediator>()
                   .SingleInstance();

            //Send 
            var handlerType = typeof(IRequestHandler<,>);
            builder.RegisterAssemblyTypes(assemblies)
                   .Where(t => !t.IsAbstract && !t.IsInterface)
                   .Where(t => t.GetInterfaces()
                   .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerType))
                   .FindConstructorsWith(new AllConstructorFinder())
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            //Publish 
            var notificationHandlerType = typeof(INotificationHandler<>);
            builder.RegisterAssemblyTypes(assemblies)
                   .Where(t => !t.IsAbstract && !t.IsInterface)
                   .Where(t => t.GetInterfaces()
                   .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == notificationHandlerType))
                   .FindConstructorsWith(new AllConstructorFinder())
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            //PipelineBehavior
            var behaviorType = typeof(IPipelineBehavior<,>);
            builder.RegisterAssemblyTypes(assemblies)
                   .Where(t => !t.IsAbstract && !t.IsInterface)
                   .Where(t => t.GetInterfaces()
                   .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == behaviorType))
                   .FindConstructorsWith(new AllConstructorFinder())
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            return builder;
        }

    }
}
