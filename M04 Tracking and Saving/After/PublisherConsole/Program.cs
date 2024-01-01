using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;
using System.Globalization;


PubContext _context = new ();
//this assumes you are working with the populated
//database created in previous module

//InsertAuthor();
void InsertAuthor()
{   
    var author = new Author { FirstName = "Frank", LastName = "Herbert" };
    _context.Authors.Add(author);
    _context.SaveChanges();
}

//RetrieveAndUpdateAuthor();
void RetrieveAndUpdateAuthor()
{
    var author = _context.Authors
        .FirstOrDefault(a => a.FirstName == "Julie" && a.LastName == "Lerman");
    if (author != null)
    {
        author.FirstName = "Julia";
        _context.SaveChanges();
    }
}

//RetrieveAndUpdateMultipleAuthors();
void RetrieveAndUpdateMultipleAuthors()
{
    var LermanAuthors = _context.Authors.Where(a => a.LastName == "Lehrman").ToList();
    foreach (var la in LermanAuthors)
    {
        la.LastName = "Lerman";
    }
    //note: \r\n is unicode to get a new line instead of the long Environment.NewLine
    Console.WriteLine($"Before:\r\n{_context.ChangeTracker.DebugView.ShortView}");
    _context.ChangeTracker.DetectChanges();
    Console.WriteLine($"After:\r\n{_context.ChangeTracker.DebugView.ShortView}");
    _context.SaveChanges();
}

//VariousOperations();
void VariousOperations()
{
    var author = _context.Authors.Find(2); //this is currently Josie Newf
    author.LastName = "Newfoundland";
    var newauthor = new Author { LastName = "Appleman", FirstName = "Dan" };
    _context.Authors.Add(newauthor);
    _context.SaveChanges();
}

//CoordinatedRetrieveAndUpdateAuthor();
void CoordinatedRetrieveAndUpdateAuthor()
{
    var author = FindThatAuthor(3);
    if (author?.FirstName == "Julie")
    {
        author.FirstName = "Julia";
        SaveThatAuthor(author);
    }
}

Author FindThatAuthor(int authorId)
{
    using var shortLivedContext = new PubContext();
    return shortLivedContext.Authors.Find(authorId);
}

void SaveThatAuthor(Author author)
{
    using var anotherShortLivedContext = new PubContext();
    anotherShortLivedContext.Authors.Update(author);
    anotherShortLivedContext.SaveChanges();
}

//DeleteAnAuthor();
void DeleteAnAuthor()
{
    var extraJL = _context.Authors.Find(1);
    if (extraJL != null)
    {
        _context.Authors.Remove(extraJL);
        _context.SaveChanges();
    }
}

//InsertMultipleAuthors();
void InsertMultipleAuthors()
{
    var newAuthors = new Author[]{
       new Author { FirstName = "Ruth", LastName = "Ozeki" },
       new Author { FirstName = "Sofia", LastName = "Segovia" },
       new Author { FirstName = "Ursula K.", LastName = "LeGuin" },
       new Author { FirstName = "Hugh", LastName = "Howey" },
       new Author { FirstName = "Isabelle", LastName = "Allende" }
    };
    _context.AddRange(newAuthors);
    _context.SaveChanges();
}

void InsertMultipleAuthorsPassedIn(List<Author> listOfAuthors)
{
    _context.Authors.AddRange(listOfAuthors);
    _context.SaveChanges();
}

//ExecuteDelete();
void ExecuteDelete()
{
    var deleteId = 9;
    _context.Authors.Where(a=>a.Id== deleteId).ExecuteDelete();
    var startswith = "H";
    var count=_context.Authors.Where(a => a.LastName.StartsWith(startswith)).ExecuteDelete();
}

ExecuteUpdate();
void ExecuteUpdate()
{
    var tenYearsAgo = DateOnly.FromDateTime(DateTime.Now).AddYears(-10);
    ////change price of books older than 10 years to $1.50
    var oldbookprice = 1.50m;
    _context.Books.Where(b => b.PublishDate < tenYearsAgo)
        .ExecuteUpdate(setters => setters.SetProperty(b => b.BasePrice, oldbookprice));

    ////change all last names to lower case
    _context.Authors
        .ExecuteUpdate(setters => setters.SetProperty(a => a.LastName,a => a.LastName.ToLower()));

    //change all last names back to title case
    //(note:May look funky but LINQ can't translate efforts like ToUpperInvariant and TextInfo)
    _context.Authors
        .ExecuteUpdate(setters => setters.SetProperty(
            a => a.LastName,
            a => a.LastName.Substring(0, 1).ToUpper() + a.LastName.Substring(1).ToLower()));
}
