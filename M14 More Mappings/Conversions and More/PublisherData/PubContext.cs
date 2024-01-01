using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using PublisherDomain;
using System.Drawing;
using System.Linq.Expressions;

namespace PublisherData;

public class PubContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Cover> Covers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = MappingsDatabase")
            .LogTo(Console.WriteLine,
                new[] { DbLoggerCategory.Database.Command.Name },
                LogLevel.Information)
        .EnableSensitiveDataLogging();
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>().HaveAnnotation("MaxLength", 100);

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().Ignore(p => p.BasePrice);
        modelBuilder.Entity<Author>().OwnsOne(p => p.Contact, navBuilder => { navBuilder.ToJson(); });
        modelBuilder.Entity<Author>().ComplexProperty(p => p.Name);
        modelBuilder.Entity<Artist>().ComplexProperty(p => p.Name);
        //modelBuilder.Entity<Author>().ComplexProperty(p => p.Address);
        modelBuilder.Entity<Author>().ComplexProperty(p => p.Addresses, b =>
        {
            b.ComplexProperty(a => a.HomeAddress);
            b.ComplexProperty(a => a.ShippingAddress);
        });
        modelBuilder.Entity<Artist>().ComplexProperty(p => p.Address);

    }
}
public class ColorToStringConverter : ValueConverter<Color, string>
{
    public ColorToStringConverter() : base(ColorString, ColorStruct) { }

    private static Expression<Func<Color, string>> ColorString = v => new String(v.Name);
    private static Expression<Func<string, Color>> ColorStruct = v => Color.FromName(v);

}
public enum BookGenre
{
    Adventure = 5,
    HistoricalFiction = 6,
    History = 7,
    Memoir = 3,
    Mystery = 2,
    ScienceFiction = 1,
    YoungAdult = 4,
}
