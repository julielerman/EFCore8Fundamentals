using Microsoft.EntityFrameworkCore;
using PublisherData;

PubContext _context = new(); 
_context.Database.EnsureDeleted();
_context.Database.EnsureCreated();
//assign a book cover to an artist
var cover = _context.Covers.Find(1); //for book 3 :)
cover.Artists.Add(_context.Artists.Find(1));
_context.SaveChanges();


//CancelBookWithRawSQL(3);
void CancelBookWithRawSQL(int bookid)
{
    //get a list of artists working on book covers for this book
    var artists = _context.Artists
        .Where(a => a.Covers.Any(cover => cover.BookId == bookid)).ToList();
    foreach (var artist in artists)
        artist.Notes +=
            Environment.NewLine +
            $"Assigned book { bookid} was cancelled on { DateTime.Today.Date} ";
    //by default, raw sql methods are not in transactions
    _context.Books.Where(b => b.BookId == bookid).ExecuteDelete();
    _context.SaveChanges();
}




CancelBookWithCustomTransaction(3);
void CancelBookWithCustomTransaction(int bookid)
{
    using var transaction = _context.Database.BeginTransaction();
    try
    {
        //get a list of artists working on book covers for this book
        var artists = _context.Artists
            .Where(a => a.Covers.Any(cover => cover.BookId == bookid)).ToList();
        foreach (var artist in artists)
            artist.Notes +=
                Environment.NewLine +
                $"Assigned book { bookid} was cancelled on { DateTime.Today.Date} ";                                                                                                                         //by default, raw sql methods execute in their own transaction immediately

        _context.Books.Where(b => b.BookId == bookid).ExecuteDelete();
        _context.SaveChanges();
        transaction.Commit();
    }
    catch (Exception)
    {
        // TODO: Handle failure
    }
}

//CancelBookBatched(3);
void CancelBookBatched(int bookid)
{
    //get a list of artists working on bo   ok covers for this book
    var artists = _context.Artists.Include(a => a.Covers).ThenInclude(c => c.Book)
          .Where(a => a.Covers.Any(cover => cover.BookId == bookid)).ToList();
    foreach (var artist in artists)
    { artist.Notes +=
            Environment.NewLine +
            $"Assigned book {bookid} was cancelled on {DateTime.Today.ToShortDateString()} ";

        artist.Covers.Remove(artist.Covers.First(c => c.BookId == bookid));
    }
    _context.SaveChanges(); 
}