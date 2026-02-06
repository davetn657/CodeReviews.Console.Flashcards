using Microsoft.Extensions.Configuration;

namespace Flashcards.davetn657.Controllers;
public class StackController
{
    private static readonly IConfiguration _configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appseting.json")
        .Build();
    private string? _connection = _configuration.GetConnectionString("DatabaseConnection");

    public void AddStack()
    {
        
    }

    public void RemoveStack()
    {

    }

    public void EditStack() 
    {

    }

    public void ReadAllStacks()
    {

    }
}
