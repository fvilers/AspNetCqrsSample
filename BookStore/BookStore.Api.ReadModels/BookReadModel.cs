using System;

namespace BookStore.Api.ReadModels
{
    public class BookReadModel
    {
        public Guid AggregateId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal? Price { get; set; }
    }
}
