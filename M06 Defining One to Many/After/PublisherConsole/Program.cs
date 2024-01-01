using PublisherData;
using PublisherDomain;

PubContext _context = new ();
//this assumes you are working with the populated
//database created in previous module
 
var newBook = new Book{Title = "How to crash your app"};
_context.Books.Add(newBook);
_context.SaveChanges();