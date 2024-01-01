using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PubAPI;
using PublisherData;
using System.Net;
using System.Net.Http.Json;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace TestAPIMethods;

[TestClass]
public partial class APITests
{
 
    [TestMethod]
    public async Task ApiIsRunning()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();
        var response = await client.GetStringAsync("/weatherforecast");
        Assert.AreEqual("[{\"date", response.Substring(0, 7));
    }  

    [TestMethod]
    public async Task CanRetrieveAnAuthorDTO()
    {   
        await using var application = new CustomWebApplicationFactory<Program>();
         CreateAndSeedDatabase(application);
        using var client = application.CreateClient();
        var authorDTO = await client.GetFromJsonAsync<AuthorDTO>("/api/author/1");
        Assert.IsInstanceOfType(authorDTO, typeof(AuthorDTO));
    }

    [TestMethod]
    public async Task CanInsertAnAuthor()
    {
        var authorDTO = new AuthorDTO(0, "John", "Doe");
        await using var application = new CustomWebApplicationFactory<Program>();
        CreateAndSeedDatabase(application);
        using var client = application.CreateClient();
        var response = await client.PostAsJsonAsync("/api/author/", authorDTO);
        var author = await response.Content.ReadFromJsonAsync<AuthorDTO>();
        Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        Assert.AreNotEqual(0, author.AuthorId);
    }

    private static void CreateAndSeedDatabase(WebApplicationFactory<Program> appFactory)
    {
        using (var scope = appFactory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<PubContext>();
            db.Database.EnsureCreated();

         }


    }

}
