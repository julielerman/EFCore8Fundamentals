using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

PubContext _context = new(); //existing database

//GetAllBooksWithTheirCovers();
void GetAllBooksWithTheirCovers()
{
    var booksandcovers = _context.Books.Include(b => b.Cover).ToList();
    booksandcovers.ForEach(book =>
     Console.WriteLine(
         book.Title +
         (book.Cover == null ? "--- No cover yet" : $"---  {book.Cover.DesignIdeas}")));
}

//GetAllBooksThatHaveCovers();
void GetAllBooksThatHaveCovers()
{
    var booksandcovers = _context.Books.Include(b => b.Cover).Where(b => b.Cover != null).ToList();
    booksandcovers.ForEach(book =>
       Console.WriteLine(book.Title + ":" + book.Cover.DesignIdeas));
}

//ProjectBooksThatHaveCovers();
void ProjectBooksThatHaveCovers()
{
    var anon = _context.Books.Where(b => b.Cover != null)
      .Select(b => new { b.Title, b.Cover.DesignIdeas })
      .ToList();
    anon.ForEach(b =>
      Console.WriteLine(b.Title + ": " + b.DesignIdeas));

}

//MultiLevelInclude();
void MultiLevelInclude()
{
    var authorGraph = _context.Authors.AsNoTracking()
        .Include(a => a.Books)
        .ThenInclude(b => b.Cover)
        .ThenInclude(c => c.Artists)
        .FirstOrDefault(a => a.AuthorId == 4);

    Console.WriteLine();
    Console.WriteLine($"{authorGraph?.FirstName} {authorGraph?.LastName}");
    foreach (var book in authorGraph.Books)
    {
        Console.WriteLine($"Book: {book.Title}");
        if (book.Cover != null)
        {
            Console.WriteLine($"Design Ideas: {book.Cover.DesignIdeas}");
            Console.Write("Artist(s):");
            book.Cover.Artists.ForEach(a => Console.Write($"{a.LastName} "));
        }
    }
};

//NewBookAndCover();
void NewBookAndCover()
{
    var book = new Book
    {
        AuthorId = 1,
        Title = "Call Me Ishtar",
        PublishDate = new DateOnly(1973, 1, 1)
    };
    book.Cover = new Cover { DesignIdeas = "Image of Ishtar?" };
    _context.Books.Add(book);
    _context.SaveChanges();
}

//AddCoverToExistingBook();
void AddCoverToExistingBook()
{
    var book = _context.Books.Find(8); //Shift
    book.Cover = new Cover { DesignIdeas = "Rows and rows of freezers" };
    _context.SaveChanges();
}

//AddCoverToExistingBookThatHasAnUntrackedCover();
void AddCoverToExistingBookThatHasAnUntrackedCover()
{
    //this will fail because theres a cover already in the database
    var book = _context.Books.Find(5); //The Never
    book.Cover = new Cover { DesignIdeas = "A spiral" };
    _context.SaveChanges();
}

//AddCoverToExistingBookWithTrackedCover();
void AddCoverToExistingBookWithTrackedCover()
{
    var book = _context.Books.Include(b => b.Cover)
                             .FirstOrDefault(b => b.BookId == 5); //The Never
    book.Cover = new Cover { DesignIdeas = "A spiral" };
    _context.ChangeTracker.DetectChanges();
    var debugview = _context.ChangeTracker.DebugView.ShortView;
}

ProtectingFromUniqueFKSideEffects();
void ProtectingFromUniqueFKSideEffects()
{
    var TheNeverDesignIdeas = "A spirally spiral";
    var book = _context.Books.Include(b => b.Cover)
                             .FirstOrDefault(b => b.BookId == 5); //The Never
    if (book.Cover != null)
    {
        book.Cover.DesignIdeas = TheNeverDesignIdeas;
    }
    else
    {
        book.Cover = new Cover { DesignIdeas = TheNeverDesignIdeas };
    }
    _context.SaveChanges();
}

