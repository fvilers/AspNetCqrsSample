using BookStore.Core.Messaging;
using BookStore.Core.Messaging.Handling;
using BookStore.Core.Processors;
using BookStore.Core.Serialization;
using BookStore.Domain.CommandHandlers;
using BookStore.Infrastructure.Messaging;
using BookStore.Infrastructure.Processors;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Concurrent;

namespace BookStore.Web.Api
{
    public class UnityConfig
    {
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // Command handlers
            container.RegisterType<ICommandHandler, BookCommandHandler>("BookCommandHandler");

            // Command bus
            var commandQueue = new ConcurrentQueue<Envelope<ICommand>>();
            container.RegisterInstance<IProducerConsumerCollection<Envelope<ICommand>>>(commandQueue);
            container.RegisterType<ICommandBus, CommandBus>();

            // Processors
            container.RegisterType<ICommandHandlerRegistry, CommandHandlerRegistry>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICommandExecuter, CommandExecuter>();
            container.RegisterType<IProcessor, CommandProcessor>();

            // Misc
            container.RegisterType<ITextSerializer, JsonTextSerializer>();
        }
    }
}
