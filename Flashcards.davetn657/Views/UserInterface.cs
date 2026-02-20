using Spectre.Console;

namespace Flashcards.davetn657.Views;
public class UserInterface
{
    internal string ChooseName(IEnumerable<string> namesInUse)
    {
        TitleCard("Choose a Name");

        var input = string.Empty;
        
        while (true)
        {
            input = AnsiConsole.Ask<string>("Type a Name (type: r to return):");
            if (input.ToLower() == "r")
            {
                return string.Empty;
            }

            if (!namesInUse.Contains(input))
            {
                return input;
            }
            
            AnsiConsole.Markup("[red]Name Already in Use or Not Available Choose a Different Name![/]\n");
        }
    }

    internal void TitleCard(string title)
    {
        var titleFiglet = new FigletText(title);

        var centered = Align.Center(titleFiglet);

        AnsiConsole.Clear();
        AnsiConsole.Write(centered);
    }
}

