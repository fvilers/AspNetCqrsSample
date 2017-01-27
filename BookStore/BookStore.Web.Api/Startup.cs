using BookStore.Core.Messaging.Handling;
using BookStore.Core.Processors;
using BookStore.Web.Api;
using Microsoft.Owin;
using Microsoft.Practices.Unity;
using Owin;
using System;
using System.Web.Hosting;

[assembly: OwinStartup(typeof(Startup))]

namespace BookStore.Web.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            ConfigureWebApi(app);

            var container = UnityConfig.GetConfiguredContainer();
            var commandHandlerRegistry = container.Resolve<ICommandHandlerRegistry>();

            if (commandHandlerRegistry != null)
            {
                foreach (var handler in container.ResolveAll<ICommandHandler>())
                {
                    commandHandlerRegistry.Register(handler);
                }
            }

            foreach (var processor in container.ResolveAll<IProcessor>())
            {
                HostingEnvironment.QueueBackgroundWorkItem(cancellationToken => processor.StartAsync(cancellationToken));
            }
        }
    }
}