using BookStore.Core.ReadModels;

namespace BookStore.Api.ReadModels
{
    public class BookReadModel : ReadModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal? Price { get; set; }
    }
}
