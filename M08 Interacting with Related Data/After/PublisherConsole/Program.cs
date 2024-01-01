using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

PubContext _context = new(); //existing database

//InsertNewAuthorWithBook();
void InsertNewAuthorWithBook()
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

//InsertNewAuthorWith2NewBooks();
void InsertNewAuthorWith2NewBooks()
{
    var author = new Author { FirstName = "Don", LastName = "Jones" };
    author.Books.AddRange(new List<Book> {
        new Book {Title = "The Never", PublishDate = new DateOnly(2019, 12, 1) },
        new Book {Title = "Alabaster", PublishDate = new DateOnly(2019,4,1)}
    });
    _context.Authors.Add(author);
    _context.SaveChanges();
}

//AddNewBookToExistingAuthorInMemory();
void AddNewBookToExistingAuthorInMemory()
{
    var author = _context.Authors.FirstOrDefault(a => a.LastName == "Howey");
    if (author != null)
    {
        author.Books.Add(
          new Book { Title = "Wool", PublishDate = new DateOnly(2012, 1, 1) }
          );
        //_context.Authors.Add(author); //this will cause a duplicate key error
    }
    _context.SaveChanges();
}

//AddNewBookToExistingAuthorInMemoryViaBook();
void AddNewBookToExistingAuthorInMemoryViaBook()
{
    var book = new Book
    {
        Title = "Shift",
        PublishDate = new DateOnly(2012, 1, 1),
        AuthorId=5 
    };
    //book.Author = _context.Authors.Find(5); //known id for Hugh Howey
    _context.Books.Add(book);
    _context.SaveChanges();
}

//EagerLoadBooksWithAuthors();
void EagerLoadBooksWithAuthors()
{
    //var authors=_context.Authors.Include(a=>a.Books).ToList();
    var pubDateStart = new DateOnly(2010, 1, 1);
    var authors = _context.Authors
        .Include(a => a.Books
                       .Where(b => b.PublishDate >= pubDateStart)
                       .OrderBy(b => b.Title))
        .ToList();

    authors.ForEach(a =>
    {
        Console.WriteLine($"{a.LastName} ({a.Books.Count})");
        a.Books.ForEach(b => Console.WriteLine($"     {b.Title}"));
    });

    
}

//Projections();
void Projections()
{
    var unknownTypes = _context.Authors
        .Select(a => new
        {
            a.AuthorId,
            Name = a.FirstName.First() + "" + a.LastName,
            a.Books  //.Where(b => b.PublishDate.Year < 2000).Count()
        })
        .ToList();
    var debugview=_context.ChangeTracker.DebugView.ShortView;
}

//ExplicitLoadCollection();
void ExplicitLoadCollection()
{
    var author = _context.Authors.FirstOrDefault(a => a.LastName == "Howey");
    if (author != null)
    { 
        _context.Entry(author).Collection(a => a.Books).Load(); 
    }
}

void LazyLoadBooksFromAnAuthor()
{
    //requires lazy loading to be set up in your app
    var author = _context.Authors.FirstOrDefault(a => a.LastName == "Howey");
    if (author != null)
    {
        foreach (var book in author.Books)
        {
            Console.WriteLine(book.Title);
        }
    }
    
}

//FilterUsingRelatedData();
void FilterUsingRelatedData()
{
    var recentAuthors = _context.Authors
        .Where(a => a.Books.Any(b => b.PublishDate.Year >= 2015))
        .ToList();
}

//ModifyingRelatedDataWhenTracked();
void ModifyingRelatedDataWhenTracked()
{
    var author = _context.Authors.Include(a => a.Books)
        .FirstOrDefault(a => a.AuthorId == 5);
    //author.Books[0].BasePrice = (decimal)10.00;
    author.Books.Remove(author.Books[1]);
    _context.ChangeTracker.DetectChanges();
    var state = _context.ChangeTracker.DebugView.ShortView;
}

//ModifyingRelatedDataWhenNotTracked();
void ModifyingRelatedDataWhenNotTracked()
{
    var author = _context.Authors.Include(a => a.Books)
        .FirstOrDefault(a => a.AuthorId == 5);
    author.Books[0].BasePrice = (decimal)12.00;

    var newContext = new PubContext();
    //newContext.Books.Update(author.Books[0]);
    newContext.Entry(author.Books[0]).State=EntityState.Modified;
    var state = newContext.ChangeTracker.DebugView.ShortView;
    newContext.SaveChanges();
}

//CascadeDeleteInActionWhenTracked();
void CascadeDeleteInActionWhenTracked()
{
    //note : I knew that author with id 9 had books in my sample database
    var author = _context.Authors.Include(a => a.Books)
     .FirstOrDefault(a => a.AuthorId == 9);
    _context.Authors.Remove(author);
    var state = _context.ChangeTracker.DebugView.ShortView;
    _context.SaveChanges();
}