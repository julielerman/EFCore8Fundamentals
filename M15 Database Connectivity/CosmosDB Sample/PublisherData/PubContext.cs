using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PublisherDomain;

namespace PublisherData
{
    public class PubContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionstring =
              "AccountEndpoint = https://someresource.documents.azure.com:443;AccountKey=yoursecretkey";

            optionsBuilder.UseCosmos(connectionstring, "EFCore8")
            .LogTo(Console.WriteLine,
                new[] { DbLoggerCategory.Database.Command.Name },
                LogLevel.Information)
           .EnableSensitiveDataLogging();
        }
    }
}
