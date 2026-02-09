using Flashcards.davetn657.Controllers;
using Flashcards.davetn657.DTOs;

namespace Flashcards.davetn657;
public class Validation
{
    public static bool IsValidStackName(string name, Dictionary<string, StackDTO> stacks)
    {
        if (stacks.ContainsKey(name))
        {
            return false;
        }
        return true;
    }
}
