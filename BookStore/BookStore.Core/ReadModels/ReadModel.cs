using System;

namespace BookStore.Core.ReadModels
{
    public abstract class ReadModel : IReadModel
    {
        public Guid AggregateId { get; set; }
    }
}