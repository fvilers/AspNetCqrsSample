using BookStore.Core.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.ReadModels
{
    public class ReadModelDao<TReadModel> : IDao<TReadModel>
    {
        private readonly IDictionary<Guid, TReadModel> _store = new Dictionary<Guid, TReadModel>();

        public Task<IEnumerable<TReadModel>> FindAsync()
        {
            return Task.FromResult(_store.Values.AsEnumerable());
        }

        public Task<TReadModel> GetAsync(Guid aggregateId)
        {
            TReadModel value;

            if (!_store.TryGetValue(aggregateId, out value))
            {
                value = default(TReadModel);
            }

            return Task.FromResult(value);
        }

        public void Add(Guid aggregateId, TReadModel value)
        {
            _store.Add(aggregateId, value);
        }
    }
}
