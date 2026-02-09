using Spectre.Console;
using Flashcards.davetn657;
using Flashcards.davetn657.Enums;
using Flashcards.davetn657.Controllers;

namespace Flashcards.davetn657;
public class UserInterface
{
    private StackController stackController;
    
    public UserInterface()
    {
        stackController = new StackController();
    }

    public void StartApp()
    {
        var endApp = false;

        while (!endApp)
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
                case MainMenuOptions.ExitApp:
                    endApp = true;
                    break;
            }
        }
        
    }

    private void ManageStack()
    {
        TitlePanel("Manage Stacks");

        string[] menuOptions = OptionUtils.GetAllStringValues(typeof(ManageStackOptions));

        var input = AnsiConsole.Prompt<string>(new SelectionPrompt<string>().AddChoices(menuOptions));
        var optionSelected = OptionUtils.GetEnumValue(input, typeof(ManageStackOptions));

        switch (optionSelected)
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
        stackController.AddStack(input);

        AnsiConsole.WriteLine($"Stack named {input} created!\n");
        AnsiConsole.Ask<string>("Press Enter to Return");
    }

    private void ChooseStack()
    {
        TitlePanel("Choose a Stack to Edit");

        var allStacks = stackController.ReadAllStacks();
        var stacks = new List<string>();

        foreach (var stack in allStacks)
        {
            stacks.Add($"{stack.Name}");
        }
        stacks.Add("Return");

        var input = AnsiConsole.Prompt<string>(new SelectionPrompt<string>().AddChoices(stacks));

        switch (input)
        {
            case "Return":
                break;
            default:
                EditStack(input);
                break;
        }
    }

    private void EditStack(string stackName)
    {

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
            Padding = new Padding(20, 1)
        };

        var centered = Align.Center(titlePanel);

        AnsiConsole.Clear();
        AnsiConsole.Write(centered);
    }

    
}

