using Microsoft.EntityFrameworkCore;
using PubAPI;
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

    public async Task<AuthorDTO> GetAuthorById(int authorid)
    {
        return await _context.Authors.AsNoTracking()
          .Where(a => a.AuthorId == authorid)
          .Select(a => new AuthorDTO(a.AuthorId, a.FirstName, a.LastName))
          .FirstOrDefaultAsync();

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