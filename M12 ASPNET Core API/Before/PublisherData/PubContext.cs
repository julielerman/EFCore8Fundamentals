using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PublisherDomain;

namespace PublisherData;

public class PubContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Cover> Covers { get; set; }
    public DbSet<AuthorByArtist> AuthorsByArtist { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
          "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = PubDatabase"
        ).LogTo(Console.WriteLine,
                new[] { DbLoggerCategory.Database.Command.Name },
                LogLevel.Information)
        .EnableSensitiveDataLogging();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthorByArtist>().HasNoKey()
            .ToView(nameof(AuthorsByArtist));

        modelBuilder.Entity<Author>().HasData(
            new Author { AuthorId = 1, FirstName = "Rhoda", LastName = "Lerman" });
        var authorList = new Author[]{
            new Author {AuthorId = 2, FirstName = "Ruth", LastName = "Ozeki" },
            new Author {AuthorId = 3, FirstName = "Sofia", LastName = "Segovia" },
            new Author {AuthorId = 4, FirstName = "Ursula K.", LastName = "LeGuin" },
            new Author {AuthorId = 5, FirstName = "Hugh", LastName = "Howey" },
            new Author {AuthorId = 6, FirstName = "Isabelle", LastName = "Allende" }
        };
        modelBuilder.Entity<Author>().HasData(authorList);

        var someBooks = new Book[]{
            new Book {BookId = 1, AuthorId=1, Title = "In God's Ear",
                PublishDate= new DateOnly(1989,3,1) },
            new Book {BookId = 2, AuthorId=2, Title = "A Tale For the Time Being",
            PublishDate = new DateOnly(2013,12,31) },
            new Book {BookId = 3, AuthorId=3, Title = "The Left Hand of Darkness",
            PublishDate=new DateOnly(1969,3,1)} };
        modelBuilder.Entity<Book>().HasData(someBooks);

        var someArtists = new Artist[]{
            new Artist {ArtistId = 1, FirstName = "Pablo", LastName="Picasso"},
            new Artist {ArtistId = 2, FirstName = "Dee", LastName="Bell"},
            new Artist {ArtistId = 3, FirstName ="Katharine", LastName="Kuharic"} };
        modelBuilder.Entity<Artist>().HasData(someArtists);

        var someCovers = new Cover[]{
            new Cover {CoverId = 1, BookId=3,
                DesignIdeas="How about a left hand in the dark?", DigitalOnly=false},
            new Cover {CoverId = 2, BookId=2,
                DesignIdeas= "Should we put a clock?", DigitalOnly=true},
            new Cover {CoverId = 3, BookId=1,
                DesignIdeas="A big ear in the clouds?", DigitalOnly = false}};
        modelBuilder.Entity<Cover>().HasData(someCovers);

        modelBuilder.Entity<Author>()
            .InsertUsingStoredProcedure("AuthorInsert", spbuilder => 
               spbuilder.HasParameter(a => a.FirstName)
                        .HasParameter(a => a.LastName)
                        .HasResultColumn(a => a.AuthorId)
            );
       }
}