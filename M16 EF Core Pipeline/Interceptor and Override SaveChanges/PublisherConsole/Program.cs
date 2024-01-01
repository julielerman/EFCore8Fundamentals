using PublisherData;
using PublisherDomain;

PubContext _context = new(); 
_context.Database.EnsureDeleted(); // Delete and recreate PubPipeline database, not PubData!
_context.Database.EnsureCreated();

NewAuthorAndBook();
void NewAuthorAndBook()
{
    var author = new Author { FirstName = "Deirdre", LastName = "Sinnott" };
    author.Books.Add(new Book
    {
        Title = "The Third Mrs. Galway",
        PublishDate = new DateTime(2021, 1, 1)
    }
    );
    _context.Authors.Add(author);
    _context.SaveChanges();
}
