using Spectre.Console;
using Flashcards.davetn657;
using Flashcards.davetn657.Enums;

namespace Flashcards.davetn657;
public class UserInterface
{
    public void MainMenu()
    {
        TitlePanel("Main Menu");

        string[] menuOptions = OptionUtils.GetAllStringValues(typeof(MainMenuOptions));

        var input = AnsiConsole.Prompt<string>(new SelectionPrompt<string>().AddChoices(menuOptions));
        var inputValue = OptionUtils.GetEnumValue(input, typeof(MainMenuOptions));


        switch (inputValue)
        {
            case MainMenuOptions.ManageStack:
                ManageStack();
                break;
            case MainMenuOptions.StudySession:
                break;
        }
    }

    private void ManageStack()
    {
        TitlePanel("Manage Stacks");

        string[] menuOptions = OptionUtils.GetAllStringValues(typeof(ManageStackOptions));

        var input = AnsiConsole.Prompt<string>(new SelectionPrompt<string>().AddChoices(menuOptions));
        var inputValue = OptionUtils.GetEnumValue(input, typeof(ManageStackOptions));

        switch (inputValue)
        {
            case ManageStackOptions.CreateStack:
                CreateStack();
                break;
            case ManageStackOptions.ChooseStack:
                ChooseStack();
                break;
        }
    }

    private void CreateStack()
    {
        TitlePanel("Create a New Stack");

        var input = AnsiConsole.Ask<string>("Name your Stack:");
    }

    private void ChooseStack()
    {
        TitlePanel("Choose a Stack to Edit");
    }

    private void StudySession()
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

