using System;
using System.Threading.Tasks;

namespace BookStore.Core.ReadModels
{
    public static class Extensions
    {
        public static async Task<TReadModel> GetOrAddAsync<TReadModel>(this IDao<TReadModel> dao, Guid aggregateId)
            where TReadModel : IReadModel, new()
        {
            if (dao == null) throw new ArgumentNullException(nameof(dao));

            var readModel = await dao.GetAsync(aggregateId).ConfigureAwait(false);

            if (readModel == null)
            {
                readModel = new TReadModel
                {
                    AggregateId = aggregateId
                };
                dao.Add(aggregateId, readModel);
            }

            return readModel;
        }
    }
}
