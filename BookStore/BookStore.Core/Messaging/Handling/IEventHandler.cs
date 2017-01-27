using System.Threading.Tasks;

namespace BookStore.Core.Messaging.Handling
{
    public interface IEventHandler
    {
    }

    public interface IEventHandler<in T> : IEventHandler
        where T : IEvent
    {
        Task HandleAsync(T @event);
    }
}