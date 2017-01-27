using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Core.Messaging
{
    public interface IEventBus
    {
        Task PublishAsync(Envelope<IEvent> envelope);
        Task PublishAsync(IEnumerable<Envelope<IEvent>> envelopes);
    }
}