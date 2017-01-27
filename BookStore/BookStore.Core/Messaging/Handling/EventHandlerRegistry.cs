using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Core.Messaging.Handling
{
    public class EventHandlerRegistry : IEventHandlerRegistry
    {
        private readonly Dictionary<Type, List<IEventHandler>> _handlers = new Dictionary<Type, List<IEventHandler>>();

        public void Register(IEventHandler handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            var supportedEventTypes = handler.GetType()
                .GetInterfaces()
                .Where(iface => iface.IsGenericType && iface.GetGenericTypeDefinition() == typeof(IEventHandler<>))
                .Select(iface => iface.GetGenericArguments()[0])
                .ToList();

            foreach (var eventType in supportedEventTypes)
            {
                List<IEventHandler> entry;
                if (!_handlers.TryGetValue(eventType, out entry))
                {
                    entry = new List<IEventHandler>();
                    _handlers.Add(eventType, entry);
                }

                _handlers[eventType].Add(handler);
            }
        }

        public bool TryGetHandlers(Type eventType, out IEnumerable<IEventHandler> handlers)
        {
            List<IEventHandler> entry;
            if (_handlers.TryGetValue(eventType, out entry))
            {
                handlers = entry;
                return true;
            }

            handlers = null;
            return false;
        }
    }
}