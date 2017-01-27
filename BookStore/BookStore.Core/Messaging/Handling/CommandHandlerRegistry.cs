using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Core.Messaging.Handling
{
    public class CommandHandlerRegistry : ICommandHandlerRegistry
    {
        private readonly Dictionary<Type, ICommandHandler> _handlers = new Dictionary<Type, ICommandHandler>();

        public void Register(ICommandHandler handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            var supportedCommandTypes = handler.GetType()
                .GetInterfaces()
                .Where(iface => iface.IsGenericType && iface.GetGenericTypeDefinition() == typeof(ICommandHandler<>))
                .Select(iface => iface.GetGenericArguments()[0])
                .ToList();
            var registeredType = _handlers.Keys.FirstOrDefault(x => supportedCommandTypes.Contains(x));

            if (registeredType != null)
            {
                var commands = String.Join(", ", supportedCommandTypes.Select(x => x.FullName));
                var registeredHandler = _handlers[registeredType];
                var message = $"The command(s) ('{commands}') handled by the received handler ('{handler}') already has a registered handler ('{registeredHandler}').";

                throw new ArgumentException(message);
            }

            foreach (var commandType in supportedCommandTypes)
            {
                _handlers.Add(commandType, handler);
            }
        }

        public bool TryGetHandler(Type commandType, out ICommandHandler handler)
        {
            return _handlers.TryGetValue(commandType, out handler);
        }
    }
}