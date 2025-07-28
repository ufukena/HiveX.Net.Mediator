using Autofac;
using AutoMapper;
using HiveX.Net.Mediator.Contracts.Behaviors;
using HiveX.Net.Mediator.Delegates;
using HiveX.Net.Mediator.DependencyInjection;
using HiveX.Net.TestConsole.Abstractions.Contracts.Markers;
using HiveX.Net.TestConsole.Behaviors;
using HiveX.Net.TestConsole.Mappers;
using Microsoft.Extensions.Logging.Abstractions;
using System.Reflection;


namespace HiveX.Net.TestConsole.Boostrapper.DISetting
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            #region Your application assemblies go here
            var assemblies = new[]
            {
                Assembly.Load("HiveX.Net.TestConsole"),
                //Assembly.Load("Your other assembly name"),
                //Assembly.Load("Your other assembly name"),
            };

            builder.RegisterAssemblyTypes(assemblies)
                    .Where(t => t.GetInterfaces().Contains(typeof(IInjectable)))
                    .Where(t => t.IsClass && !t.IsAbstract)
                    .FindConstructorsWith(new AllConstructorFinder())
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();

            builder.AddHiveXNetMediator(assemblies);
            #endregion


            //Behaviors
            builder.RegisterGeneric(typeof(LogExceptionBehavior<,>)).As(typeof(IPipelineBehavior<,>)).InstancePerLifetimeScope();
            

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });




            //AutoMapper (Optional)

            var loggerFactory = NullLoggerFactory.Instance;

            builder.Register(ctx => new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>(), loggerFactory))
                    .AsSelf()
                    .SingleInstance();
            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper())
                    .As<IMapper>();

        }

    }


}
