namespace PublisherDomain;

public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
}

public class Addresses
{
    public Address HomeAddress { get; set; }
    public Address ShippingAddress { get; set; }
}
