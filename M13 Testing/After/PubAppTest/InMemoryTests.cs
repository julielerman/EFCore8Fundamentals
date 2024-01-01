using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PublisherConsole;
using PublisherData;
using PublisherDomain;
using SQLitePCL;
using System.ComponentModel.DataAnnotations;


namespace PubAppTest;

[TestClass]
public class InMemoryTests
{
    [TestMethod]
    public void CanInsertAuthorIntoDatabase()
    {
        var builder = new DbContextOptionsBuilder<PubContext>();
        //builder.UseSqlServer(
        //    "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = PubTestData");
        var _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        builder.UseSqlite(_connection);
        using (var context = new PubContext(builder.Options))
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var author = new Author { FirstName = "a", LastName = "b" };
            context.Authors.Add(author);
            //Debug.WriteLine($"Before save: {author.AuthorId}");
            context.SaveChanges();
            //Debug.WriteLine($"After save: {author.AuthorId}");
            Assert.AreNotEqual(0, author.AuthorId);
        }
    }

    [TestMethod]
    public void ChangeTrackerIdentifiesAddedAuthor()
    {
        var builder = new DbContextOptionsBuilder<PubContext>().UseSqlite("Filename=:memory:");
        using var context = new PubContext(builder.Options);
        var author = new Author { FirstName = "a", LastName = "b" };
        context.Authors.Add(author);
        Assert.AreEqual(EntityState.Added, context.Entry(author).State);
    }

    [TestMethod]
    public void InsertAuthorsReturnsCorrectResultNumber()
    {
        using PubContext context = SetUpSQLiteMemoryContextWithOpenConnection();
        var authorList = new Dictionary<string, string>
                { { "a" , "b" },
                  { "c" , "d" },
                  { "d" , "e" }
                };

        var dl = new DataLogic(context);
        Assert.AreEqual(authorList.Count, dl.ImportAuthors(authorList));
    }

    private static PubContext SetUpSQLiteMemoryContextWithOpenConnection()
    {
        var builder = new DbContextOptionsBuilder<PubContext>().UseSqlite("Filename=:memory:");
        var context = new PubContext(builder.Options);
        context.Database.OpenConnection();
        context.Database.EnsureCreated();
        return context;
    }
}

