using PublisherConsole;

AddSomeAuthors();

void AddSomeAuthors()
{
    var authorList = new Dictionary<string, string>
    {
       { "Beth","Cato" },
       { "Alix E.", "Harrow" },
       { "Bernardine", "Evarista" },
       { "Laline",  "Paul" },
       { "Sarah", "Penner" }
    };
    var dl = new DataLogic();
    dl.ImportAuthors(authorList);
}
