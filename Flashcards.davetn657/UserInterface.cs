using Spectre.Console;
using Flashcards.davetn657;
using Flashcards.davetn657.Enums;
using Flashcards.davetn657.Controllers;
using Flashcards.davetn657.DTOs;

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

            var menuOptions = OptionUtils.GetAllStringValues(typeof(MainMenuOptions));

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

        var menuOptions = OptionUtils.GetAllStringValues(typeof(ManageStackOptions));

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

        var input = string.Empty;

        while (true)
        {
            input = AnsiConsole.Ask<string>("Name your Stack:");

            if (Validation.IsValidStackName(input, stackController.ReadAllStacks()))
            {
                stackController.AddStack(input);
                break;
            }
            else
            {
                AnsiConsole.Markup("[red]Stack Name Already in Use Choose a Different Name![/]\n");
            }
        }

        AnsiConsole.WriteLine($"Stack named {input} created!\n");
        AnsiConsole.Prompt<string>(new SelectionPrompt<string>().AddChoices("Press Enter to Return"));
    }

    private void ChooseStack()
    {
        TitlePanel("Choose a Stack to Edit");

        var stacks = stackController.ReadAllStacks();

        var input = AnsiConsole.Prompt<string>(new SelectionPrompt<string>().AddChoices(stacks.Keys) );

        switch (input)
        {
            case "Return":
                break;
            default:
                EditStack(stacks[input]);
                break;
        }
    }

    private void EditStack(StackDTO stack)
    {
        TitlePanel($"Edit : {stack}");

        var menuOptions = OptionUtils.GetAllStringValues(typeof(EditStackOptions));

        var input = AnsiConsole.Prompt<string>(new SelectionPrompt<string>().AddChoices(menuOptions));
        var optionSelected = OptionUtils.GetEnumValue(input, typeof(EditStackOptions));

        switch (optionSelected)
        {
            case EditStackOptions.RenameStack:
                stackController.EditStack(stack);
                break;
            case EditStackOptions.AddCard:
                break;
            case EditStackOptions.RemoveCard:
                break;
            case EditStackOptions.DeleteStack:
                stackController.RemoveStack(stack);
                break;
            case EditStackOptions.Return:
                break;
        }
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

