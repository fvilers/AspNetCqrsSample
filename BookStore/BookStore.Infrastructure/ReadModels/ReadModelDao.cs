using BookStore.Core.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.ReadModels
{
    public class ReadModelDao<TReadModel> : IDao<TReadModel>
        where TReadModel : IReadModel
    {
        private static readonly IDictionary<Guid, TReadModel> Store = new Dictionary<Guid, TReadModel>();

        public Task<IEnumerable<TReadModel>> FindAsync()
        {
            return Task.FromResult(Store.Values.AsEnumerable());
        }

        public Task<TReadModel> GetAsync(Guid aggregateId)
        {
            TReadModel value;

            if (!Store.TryGetValue(aggregateId, out value))
            {
                value = default(TReadModel);
            }

            return Task.FromResult(value);
        }

        public void Add(Guid aggregateId, TReadModel value)
        {
            Store.Add(aggregateId, value);
        }

        public void Remove(Guid aggregateId)
        {
            Store.Remove(aggregateId);
        }
    }
}
