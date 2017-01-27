using System.Threading.Tasks;

namespace BookStore.Core.Messaging.Handling
{
    public interface IEventDispatcher
    {
        Task DispatchEventAsync(IEvent @event, string correlationId = null);
    }
}