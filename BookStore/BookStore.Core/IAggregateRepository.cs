using System;
using System.Threading.Tasks;

namespace BookStore.Core
{
    public interface IAggregateRepository<TAggregate>
        where TAggregate : IAggregateRoot
    {
        Task<TAggregate> GetAsync(Guid aggregateId);
        Task StoreAsync(TAggregate aggregate, string correlationId = null);
    }
}