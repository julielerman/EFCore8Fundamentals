namespace PublisherDomain;

public class Author
{
    public Guid AuthorId { get; set; }=Guid.NewGuid();
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Book> Books { get; set; } = new();
}
