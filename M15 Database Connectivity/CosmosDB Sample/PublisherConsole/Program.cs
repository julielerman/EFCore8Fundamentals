using PublisherData;
using PublisherDomain;

PubContext _context = new();
_context.Database.EnsureDeleted();
_context.Database.EnsureCreated();

var author = new Author { FirstName = "Rhoda", LastName = "Lerman" };
author.Books.Add(
    new Book
    {
        Title = "Solimeos",
        PublishDate = new DateTime(2023, 6, 1)  //final novel, published posthumously
    });
author.Books.Add(new Book
{
    Title = "In the Company of Newfies",
    PublishDate = new DateTime(1996, 3, 1)
});
_context.Authors.Add(author);
_context.SaveChanges();
