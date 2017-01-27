using BookStore.Core;
using BookStore.Core.EventSourcing;
using BookStore.Core.Messaging;
using BookStore.Core.Messaging.Handling;
using BookStore.Core.Processors;
using BookStore.Core.ReadModels;
using BookStore.Core.Serialization;
using BookStore.Domain.CommandHandlers;
using BookStore.Infrastructure.EventSourcing;
using BookStore.Infrastructure.Messaging;
using BookStore.Infrastructure.Processors;
using BookStore.Infrastructure.ReadModels;
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

            // Event bus
            var eventQueue = new ConcurrentQueue<Envelope<IEvent>>();
            container.RegisterInstance<IProducerConsumerCollection<Envelope<IEvent>>>(eventQueue);
            container.RegisterType<IEventBus, EventBus>();

            // Processors
            container.RegisterType<ICommandHandlerRegistry, CommandHandlerRegistry>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICommandExecuter, CommandExecuter>();
            container.RegisterType<IProcessor, CommandProcessor>("CommandProcessor");

            container.RegisterType<IEventHandlerRegistry, EventHandlerRegistry>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEventDispatcher, EventDispatcher>();

            // Event sourcing
            container.RegisterType<IEventStore, EventStore>(new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(IAggregateRepository<>), typeof(AggregateRepository<>));

            // Read models
            container.RegisterType(typeof(IDao<>), typeof(ReadModelDao<>));

            // Misc
            container.RegisterType<ITextSerializer, JsonTextSerializer>();
        }
    }
}
