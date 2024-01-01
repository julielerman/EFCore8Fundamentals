using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublisherData.Migrations
{
    /// <inheritdoc />
    public partial class addstoredproc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE dbo.AuthorsPublishedinYearRange
                     @yearstart int,
                     @yearend int
                  AS
                  SELECT Distinct Authors.* FROM authors
                  LEFT JOIN Books ON Authors.Authorid = books.authorId
                  WHERE Year(books.PublishDate) >= @yearstart 
                   AND Year(books.PublishDate) <= @yearend");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP PROCEDURE dbo.AuthorsPublishedinYearRange");
        }
    }
}
