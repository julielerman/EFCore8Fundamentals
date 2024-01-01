namespace PublisherDomain
{
    public class Book
    {
        public Book()
        {
            BookId= Guid.NewGuid();
        }
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public decimal BasePrice { get; set; }
        public Author Author { get; set; }
        public Guid AuthorId { get; set; }

    }
}
