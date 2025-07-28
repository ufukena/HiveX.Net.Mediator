using Autofac;
using HiveX.Net.Mediator.Contracts.Entities;
using HiveX.Net.Mediator.Contracts.Markers;
using HiveX.Net.TestConsole.Abstractions.Contracts.Context;
using HiveX.Net.TestConsole.Boostrapper.DISetting;
using HiveX.Net.TestConsole.Context;
using HiveX.Net.TestConsole.Helpers;


namespace HiveX.Net.TestConsole.Boostrapper.Configurator
{

    public class ServiceConfigurator : IDisposable
    {

        private Autofac.IContainer? _container;
        private ILifetimeScope? _scope;

        public IMediator ConfigureAndGetMediator(Action<ContainerBuilder>? dbConfig = null)
        {
            var builder = new ContainerBuilder();

            // DBContext
            if (dbConfig != null)
                dbConfig(builder);
            else
                builder.RegisterType<DatabaseContext>().As<IDatabaseContext>().InstancePerLifetimeScope();



            // AutofacModule register 
            builder.RegisterModule<AutofacModule>();


            // Register the IMediator interface and its implementation
            _container = builder.Build();
            _scope = _container.BeginLifetimeScope();
            IMediator mediator = _scope.Resolve<IMediator>();


            // Adding your custom helpers
            var services = new List<IHelper> { new CommonWebHelper() };
            mediator.AddHelpers(services);


            return mediator;

        }

        public void Dispose() => _scope?.Dispose();

    }

}
