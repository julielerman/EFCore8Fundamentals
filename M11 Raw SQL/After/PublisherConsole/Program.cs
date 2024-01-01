using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PublisherData;
using PublisherDomain;

PubContext _context = new(); //existing database

//SimpleRawSQL();
void SimpleRawSQL()
{
    var authors = _context.Authors.FromSqlRaw("select * from authors")
        .Include(a => a.Books).ToList();
}

//ConcatenatedRawSql_Unsafe();   //There is no safe query with contatenated strings!
void ConcatenatedRawSql_Unsafe()
{
    var lastnameStart = "L";
    var authors = _context.Authors
        .FromSqlRaw("SELECT * FROM authors WHERE lastname LIKE '" + lastnameStart + "%'")
        .OrderBy(a => a.LastName).TagWith("Concatenated_Unsafe").ToList();
}

//FormattedRawSql_Unsafe();
void FormattedRawSql_Unsafe()
{
    var lastnameStart = "L";
    var sql = String.Format("SELECT * FROM authors WHERE lastname LIKE '{0}%'", lastnameStart);
    var authors = _context.Authors.FromSqlRaw(sql)
        .OrderBy(a => a.LastName).TagWith("Formatted_Unsafe").ToList();
}

//FormattedRawSql_Safe();
void FormattedRawSql_Safe()
{
    var lastnameStart = "L";
    var authors = _context.Authors
        .FromSqlRaw("SELECT * FROM authors WHERE lastname LIKE '{0}%'", lastnameStart)
        .OrderBy(a => a.LastName).TagWith("Formatted_Safe").ToList();
}

//StringFromInterpolated_Unsafe();
void StringFromInterpolated_Unsafe()
{
    var lastnameStart = "L";
    string sql = $"SELECT * FROM authors WHERE lastname LIKE '{lastnameStart}%'";
    var authors = _context.Authors.FromSqlRaw(sql)
        .OrderBy(a => a.LastName).TagWith("Interpolated_Unsafe").ToList();
}

//StringFromInterpolated_StillUnsafe();
void StringFromInterpolated_StillUnsafe()
{
    var lastnameStart = "L";
    var authors = _context.Authors
        .FromSqlRaw($"SELECT * FROM authors WHERE lastname LIKE '{lastnameStart}%'")
        .OrderBy(a => a.LastName).TagWith("Interpolated_StillUnsafe").ToList();
}

//StringFromInterpolated_Safe();
void StringFromInterpolated_Safe()
{
    var lastnameStart = "L";
    var authors = _context.Authors
        .FromSql($"SELECT * FROM authors WHERE lastname LIKE '{lastnameStart}%'")
    .OrderBy(a => a.LastName).TagWith("Interpolated_Safe").ToList();
}

#if false
StringFromInterpolated_SoSafeItWontCompile();
void StringFromInterpolated_SoSafeItWontCompile()
{
    var lastnameStart = "L";
    var sql = $"SELECT * FROM authors WHERE lastname LIKE '{lastnameStart}%'";
    var authors = _context.Authors.FromSql(sql)
    .OrderBy(a => a.LastName).TagWith("Interpolated_WontCompile").ToList();
}

FormattedWithInterpolated_SoSafeItWontCompile();
void FormattedWithInterpolated_SoSafeItWontCompile()
{
    var lastnameStart = "L";
    var authors = _context.Authors
        .FromSql
            ("SELECT * FROM authors WHERE lastname LIKE '{0}%'", lastnameStart)
        .OrderBy(a => a.LastName).TagWith("Interpolated_WontCompile").ToList();
}

#endif

//RawSqlStoredProc();
void RawSqlStoredProc()
{
    var authors = _context.Authors
        .FromSqlRaw("AuthorsPublishedinYearRange {0}, {1}", 2010, 2015)
        .ToList();
}

//InterpolatedSqlStoredProc();
void InterpolatedSqlStoredProc()
{
    int start = 2010;
    int end = 2015;
    var authors = _context.Authors
    .FromSql($"AuthorsPublishedinYearRange {start}, {end}")
    .ToList();
}

//RunSqlQueryScalarMethods(); 
void RunSqlQueryScalarMethods()
{
    var ids = _context.Database
    .SqlQuery<int>($"SELECT AuthorId FROM Authors").ToList();

    var titles = _context.Database
    .SqlQuery<string>($"SELECT Title FROM Books").ToList();

    var sometitles = _context.Database
     .SqlQuery<string>($"SELECT Title as VALUE FROM Books")
     .Where(t => t.Contains("The")).ToList();

    //var longtitles=_context.Database
    //.SqlQuery<string>($"SELECT Title as VALUE FROM Books")
    //.Where(t => t.Length > 10).ToList();//EF can't evalueate t.Length into SQL

    var longtitles = _context.Database
    .SqlQuery<string>($"SELECT Title FROM Books WHERE LEN(title)>{10}").ToList();

    var rawLongTitles = _context.Database
    .SqlQueryRaw<string>($"SELECT Title FROM Books WHERE LEN(title)>{0}", 10).ToList();

}

//RunSqlQueryNonEntityMethods();
void RunSqlQueryNonEntityMethods()
{
    var xyz = _context.Database
        .SqlQuery<AuthorName>($"select lastname, firstname from authors").ToList();

    var xyz2 = _context.Database
        .SqlQuery<AuthorName>($"GetAuthorNames").ToList();

}

//GetAuthorsByArtist();
void GetAuthorsByArtist()
{
    var authorartists = _context.AuthorsByArtist.ToList();
    var oneauthorartists = _context.AuthorsByArtist.FirstOrDefault();
    var Kauthorartists = _context.AuthorsByArtist
                                 .Where(a => a.Artist.StartsWith("K")).ToList();
    var debugView = _context.ChangeTracker.DebugView.ShortView;
}

//DeleteCover(9);
void DeleteCover(int coverId)
{
    var rowCount = _context.Database.ExecuteSqlRaw("DeleteCover {0}",coverId);
    Console.WriteLine(rowCount);
}

//InsertNewAuthor();
void InsertNewAuthor()
{
    var author = new Author { FirstName = "Madeline", LastName = "Miller" };
    _context.Authors.Add(author);
    _context.SaveChanges();
}

InsertNewAuthorWithNewBook();
void InsertNewAuthorWithNewBook()
{
    var author = new Author { FirstName = "Anne", LastName = "Enright" };
    author.Books.Add(new Book { Title = "The Green Road" });
    _context.Authors.Add(author);
    _context.SaveChanges();
}

class AuthorName
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
}
