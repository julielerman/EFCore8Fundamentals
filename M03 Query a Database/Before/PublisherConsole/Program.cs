using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

GetAuthors();

void GetAuthors()
{
    using var context = new PubContext();
    var authors = context.Authors.ToList();
    foreach (var author in authors)
    {
        Console.WriteLine(author.FirstName + " " + author.LastName);
    }
}
