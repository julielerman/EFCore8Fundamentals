using PublisherData;
using PublisherDomain;
using Microsoft.EntityFrameworkCore;

PubContext _context = new PubContext(); 
_context.Database.EnsureDeleted();
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


AFewQueries();
void AFewQueries()
{
    var authors=_context.Authors.TagWith("RobustQueryHint").ToList();
    var books=_context.Books.ToList();  
}