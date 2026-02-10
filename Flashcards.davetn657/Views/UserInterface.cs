using Spectre.Console;
using Flashcards.davetn657.Controllers;
using Flashcards.davetn657.Models;
using Flashcards.davetn657.Models.Enums;
using Flashcards.davetn657.Models.DTOs;

namespace Flashcards.davetn657.Views;
public class UserInterface
{
    private StackController stackController;
    private CardController cardController;
    
    public UserInterface()
    {
        stackController = new StackController();
        cardController = new CardController();
    }

    public void StartApp()
    {
        var endApp = false;

        while (!endApp)
        {
            TitlePanel("Main Menu");

            var menuOptions = OptionUtils.GetAllStringValues(typeof(MainMenuOptions));

            var input = AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(menuOptions));
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

        var input = AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(menuOptions));
        var optionSelected = OptionUtils.GetEnumValue(input, typeof(ManageStackOptions));

        switch (optionSelected)
        {
            case ManageStackOptions.CreateStack:
                stackController.AddStack(NameStack());
                AnsiConsole.WriteLine($"Stack named {input} created!\n");
                AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices("Press Enter to Return"));
                break;
            case ManageStackOptions.ChooseStack:
                ChooseStack();
                break;
        }
    }

    

    private void ChooseStack()
    {
        TitlePanel("Choose a stack to edit");

        var stacks = stackController.ReadAllStacks();

        var input = AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(stacks.Keys).AddChoices("Return") );

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
        var endEdit = false;

        while (!endEdit)
        {
            TitlePanel($"Edit : {stack}");

            var menuOptions = OptionUtils.GetAllStringValues(typeof(EditStackOptions));

            var input = AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(menuOptions));
            var optionSelected = OptionUtils.GetEnumValue(input, typeof(EditStackOptions));

            switch (optionSelected)
            {
                case EditStackOptions.RenameStack:
                    stackController.EditStack(stack, NameStack());
                    break;
                case EditStackOptions.CreateCard:
                    CreateCard(stack);
                    break;
                case EditStackOptions.ChooseCard:
                    ChooseCard(stack);
                    break;
                case EditStackOptions.DeleteStack:
                    stackController.RemoveStack(stack);
                    break;
                case EditStackOptions.Return:
                    endEdit = true;
                    break;
            }
        }
        
    }

    private string NameStack()
    {
        TitlePanel("Name your Stack");

        var input = string.Empty;
        var cards = stackController.ReadAllStacks();

        while (true)
        {
            input = AnsiConsole.Ask<string>("Name your stack:");

            if (!cards.ContainsKey(input) && input != "Return")
            {
                return input;
            }
            else
            {
                AnsiConsole.Markup("[red]Stack Name Already in Use Choose a Different Name![/]\n");
            }
        }
    }

    private void CreateCard(StackDTO stack)
    {
        TitlePanel("Create a new Card");

        var input = string.Empty;
        var cards = cardController.ReadAllCards();
        var card = new CardDTO();

        input = AnsiConsole.Ask<string>("Input question details:");
        card.Question = input;

        input = AnsiConsole.Ask<string>("Input answer details:");
        card.Answer = input;

        cardController.AddCard(card, stack);
    }

    private void ChooseCard(StackDTO stack)
    {
        TitlePanel($"{stack.Name} cards");

        var allCards = cardController.ReadAllCards();

        var input = AnsiConsole.Prompt<string>(new SelectionPrompt<string>().AddChoices(allCards.Keys).AddChoices("Return"));


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

