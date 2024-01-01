
using PublisherData;
using PublisherDomain;

PubContext _context = new(); //existing database

InsertNewAuthorWithNewBook();

void InsertNewAuthorWithNewBook()
{
    var author = new Author { FirstName = "Lynda", LastName = "Rutledge" };
    author.Books.Add(new Book
    {
        Title = "West With Giraffes",
        PublishDate = new DateOnly(2021, 2, 1)
    });
    _context.Authors.Add(author);
    _context.SaveChanges();
}