namespace PubAPI;

public class AuthorDTO
{
   public AuthorDTO(int authorId, string firstName, string lastName)
    {
        AuthorId = authorId;
        FirstName = firstName;
        LastName = lastName;
    }
    public int AuthorId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
}
