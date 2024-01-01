using PublisherData;

PubContext _context = new PubContext();
//this assumes you are working with the populated
//database created in previous module

//_context.Authors.ToList();

var name = "Ozeki";
var authors=_context.Authors.Where(a => a.LastName == name).ToList();