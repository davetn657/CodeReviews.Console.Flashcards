using Spectre.Console;
using Flashcards.davetn657;
using Flashcards.davetn657.Enums;

namespace Flashcards.davetn657;
public class UserInterface
{
    public void MainMenu()
    {
        TitlePanel("Main Menu");

        string[] menuOptions = new string[]
        {
            OptionUtils.GetStringValue(MainMenuOptions.CreateStack),
            OptionUtils.GetStringValue(MainMenuOptions.ManageStack),
            OptionUtils.GetStringValue(MainMenuOptions.StudySession)
        };

        var input = AnsiConsole.Prompt<string>(new SelectionPrompt<string>().AddChoices(menuOptions));
        var menuOption = OptionUtils.GetEnumValue(input, typeof(MainMenuOptions));


        switch (menuOption)
        {
            case MainMenuOptions.CreateStack:
                CreateStack();
                break;
            case MainMenuOptions.ManageStack:
                break;
            case MainMenuOptions.StudySession:
                break;
        }
    }

    internal void CreateStack()
    {
        TitlePanel("Create a New Stack");
    }

    internal void ManageStack()
    {
        TitlePanel("Manage Stacks");
    }

    internal void StudySession()
    {
        TitlePanel("Study Session");
    }

    private void TitlePanel(string title)
    {
        var titlePanel = new Panel(title)
        {
            Border = BoxBorder.Double,
            Padding = new Padding(2, 0)
        };

        AnsiConsole.Clear();
        AnsiConsole.Write(titlePanel);
    }

    
}

