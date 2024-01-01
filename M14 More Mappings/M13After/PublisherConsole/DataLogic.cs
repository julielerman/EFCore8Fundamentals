using PublisherData;
using PublisherDomain;

namespace PublisherConsole;

public class DataLogic
{
    PubContext _context;

    public DataLogic(PubContext context)
    {
        _context = context;
    }
    public DataLogic()
    {
        _context = new PubContext();
    }

    public int ImportAuthors(Dictionary<string, string> authorList)
    {
        foreach (var author in authorList)
        {
            _context.Authors.Add(
                new Author { FirstName = author.Key, LastName = author.Value });
        }
        return _context.SaveChanges();
    }
}