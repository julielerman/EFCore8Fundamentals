using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PublisherDomain;

namespace PublisherData;

public class PubContextStreamWriter : DbContext
{
    private StreamWriter _writer = new StreamWriter("EFCoreLog.txt", append: true);
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
          "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = PubDatabase"
        ).LogTo(_writer.WriteLine, 
                new[] {DbLoggerCategory.Database.Command.Name},
                LogLevel.Information)
         .EnableSensitiveDataLogging();
    }
    public override void Dispose()
    {
        _writer.Dispose();  
        base.Dispose();
    }

}

