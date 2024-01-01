namespace PublisherDomain;

public class Author
{
    public int AuthorId { get; set; }
    //public string FirstName { get; set; }
    //public string LastName { get; set; }
    public List<Book> Books { get; set; } =new();
    public ContactDetails Contact { get; set; } = new();
    public ICollection<string> Nicknames { get; set; }
    public Addresses Addresses { get; set; }
    public PersonName Name { get; set; }

}


