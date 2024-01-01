 using PublisherData;
using PublisherDomain;

PubContext _context = new(); //existing database

//RetrieveAndUpdateOneBook();
void RetrieveAndUpdateOneBook()
{
    var thebook = _context.Books.FirstOrDefault();
    thebook.BasePrice = 15.00M;
    thebook.Title += "!";
    _context.SaveChanges();
}
//AddAuthorWithContactDetails();
void AddAuthorWithContactDetails()
{
    var author = new Author
    {
        Name=new PersonName { FirstName = "William", LastName = "Shakespeare" },
        //FirstName = "William",
        //LastName = "Shakespeare",
        Contact = new ContactDetails
        {
            EmailAddress = "will@upstart.crow",
            Phone = "555-555-1234"
        }
    };
    _context.Authors.Add(author);
    _context.SaveChanges();
}

AddAuthorWithNicknames();
void AddAuthorWithNicknames()
{
    var author = new Author
    {
        Name = new PersonName { FirstName = "William", LastName = "Shakespeare" },
        //FirstName = "William",
        //LastName = "Shakespeare",
        Nicknames = new List<string> { "The Bard", "The Bard of Avon","Swan of Avon"  }
    };
    _context.Authors.Add(author);
    _context.SaveChanges();
}
