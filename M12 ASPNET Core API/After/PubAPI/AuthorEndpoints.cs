using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;
namespace PubAPI;

public static class AuthorEndpoints
{
    public static void MapAuthorEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Author").WithTags(nameof(Author));

        group.MapGet("/WithBooks/", async (PubContext db) =>
        {
            return await db.Authors.Include(a => a.Books).AsNoTracking().ToListAsync();
        })
        .WithName("GetAllAuthorsWithBooks")
        .WithOpenApi();

        group.MapGet("/{authorid}", async Task<Results<Ok<AuthorDTO>, NotFound>> (int authorid, PubContext db) =>
        {
            return await db.Authors.AsNoTracking()
                .Where(model => model.AuthorId == authorid)
                .Select(a => new AuthorDTO(a.AuthorId, a.FirstName, a.LastName))
                .FirstOrDefaultAsync()
                is AuthorDTO model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetAuthorById")
        .WithOpenApi();


        group.MapPut("/{authorid}", async Task<Results<Ok, NotFound>> (int authorid, AuthorDTO authorDTO, PubContext db) =>
      {
          var affected = await db.Authors
              .Where(model => model.AuthorId == authorid)
              .ExecuteUpdateAsync(setters => setters
                  //.SetProperty(m => m.AuthorId, authorDTO.AuthorId)
                  .SetProperty(m => m.FirstName, authorDTO.FirstName)
                  .SetProperty(m => m.LastName, authorDTO.LastName)
                  );
          return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
      })
      .WithName("UpdateAuthor")
      .WithOpenApi();

        group.MapPost("/", async (AuthorDTO authorDTO, PubContext db) =>
        {
            var author = new Author { FirstName = authorDTO.FirstName, LastName = authorDTO.LastName };
            db.Authors.Add(author);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Author/{author.AuthorId}",
                new AuthorDTO(author.AuthorId, author.FirstName, author.LastName));
        })
        .WithName("CreateAuthor")
        .WithOpenApi();

        group.MapDelete("/{authorid}", async Task<Results<Ok, NotFound>> (int authorid, PubContext db) =>
        {
            var affected = await db.Authors
                .Where(model => model.AuthorId == authorid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteAuthor")
        .WithOpenApi();
    }
}

