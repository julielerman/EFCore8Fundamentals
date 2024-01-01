using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

using PubContext _context = new();
//this assumes you are working with the populated
//database created in previous module

//FindIt();
void FindIt()
{
    var authorIdTwo = _context.Authors.Find(2);
}

//QueryFilters();
void QueryFilters()
{
    //var firstname = "Josie";
    //var authors = _context.Authors.Where(a => a.FirstName == firstname).ToList();
    var filter = "L%";
    var authors = _context.Authors
        .Where(a => EF.Functions.Like(a.LastName, filter)).ToList();
}
//AddSomeMoreAuthors();
void AddSomeMoreAuthors()
{
    _context.Authors.Add(new Author { FirstName = "Rhoda", LastName = "Lerman" });
    _context.Authors.Add(new Author { FirstName = "Don", LastName = "Jones" });
    _context.Authors.Add(new Author { FirstName = "Jim", LastName = "Christopher" });
    _context.Authors.Add(new Author { FirstName = "Stephen", LastName = "Haunts" });
    _context.SaveChanges();
}

//SkipAndTakeAuthors();
void SkipAndTakeAuthors()
{
    var groupSize = 2;
    for (int i = 0; i < 5; i++)
    {
        var authors = _context.Authors.Skip(groupSize * i).Take(groupSize).ToList();
        Console.WriteLine($"Group {i}:");
        foreach (var author in authors)
        {
            Console.WriteLine($" {author.FirstName} {author.LastName}");
        }
    }
}

//SortAuthors();
void SortAuthors()
{
    //var authorsByLastName = _context.Authors
    //    .OrderBy(a => a.LastName)
    //    .ThenBy(a=>a.FirstName).ToList();  
    //authorsByLastName.ForEach(a => Console.WriteLine(a.LastName + "," + a.FirstName));

    var authorsDescending = _context.Authors
    .OrderByDescending(a => a.LastName)
    .ThenByDescending(a => a.FirstName).ToList();
    Console.WriteLine("**Descending Last and First**");
    authorsDescending.ForEach(a => Console.WriteLine(a.LastName + "," + a.FirstName));
}

QueryAggregate();
void QueryAggregate()
{
    var author = _context.Authors.Where(a => a.LastName == "Lerman").FirstOrDefault();
}