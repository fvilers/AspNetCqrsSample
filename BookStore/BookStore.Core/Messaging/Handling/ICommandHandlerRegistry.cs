using System;

namespace BookStore.Core.Messaging.Handling
{
    public interface ICommandHandlerRegistry : IRegistry<ICommandHandler>
    {
        bool TryGetHandler(Type commandType, out ICommandHandler handler);
    }
}