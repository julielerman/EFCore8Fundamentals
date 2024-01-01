namespace PublisherDomain;

public class PersonName
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string ReverseName => $"{LastName}, {FirstName}";    

}
