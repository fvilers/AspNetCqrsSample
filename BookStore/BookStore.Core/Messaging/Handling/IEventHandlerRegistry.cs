using System;
using System.Collections.Generic;

namespace BookStore.Core.Messaging.Handling
{
    public interface IEventHandlerRegistry : IRegistry<IEventHandler>
    {
        bool TryGetHandlers(Type eventType, out IEnumerable<IEventHandler> handlers);
    }
}