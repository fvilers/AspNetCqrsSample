namespace BookStore.Domain.SpecialCases
{
    public sealed class BookNotFound : Book
    {
        public BookNotFound()
            : base("NOT_FOUND", "NOT_FOUND")
        {
        }
    }
}
