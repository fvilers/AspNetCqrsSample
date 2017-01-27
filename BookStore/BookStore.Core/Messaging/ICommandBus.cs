using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Core.Messaging
{
    public interface ICommandBus
    {
        Task SendAsync(Envelope<ICommand> envelope);
        Task SendAsync(IEnumerable<Envelope<ICommand>> envelopes);
    }
}