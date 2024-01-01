namespace PublisherDomain;

public class Artist
{
    public int ArtistId { get; set; }
    public string FirstName { get; set;}
    public string LastName { get; set;}
    public List<Cover> Covers
      { get; set; } = new();
}
