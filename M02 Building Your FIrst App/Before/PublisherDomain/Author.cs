namespace PublisherDomain
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Book> Books 
          { get; set; } = new List<Book>();
    }
}
